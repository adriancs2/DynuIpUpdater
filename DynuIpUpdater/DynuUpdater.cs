using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace DynuIpUpdater
{
    public class UpdateResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ResponseCode { get; set; }
        public string NewIp { get; set; }

        public UpdateResult()
        {
            Success = false;
            Message = string.Empty;
            ResponseCode = string.Empty;
            NewIp = string.Empty;
        }
    }

    public class DynuUpdater : IDisposable
    {
        private readonly AppConfig _config;
        private readonly WebClient _webClient;

        // IP detection services (try multiple for reliability)
        private static readonly string[] IpDetectionUrls = new[]
        {
            "https://api.ipify.org",
            "https://ipcheck.dynu.com",
            "https://icanhazip.com",
            "https://api.my-ip.io/ip"
        };

        // IPv6 detection services
        private static readonly string[] Ipv6DetectionUrls = new[]
        {
            "https://api64.ipify.org",
            "https://ipv6.icanhazip.com"
        };

        private const string DynuUpdateUrl = "https://api.dynu.com/nic/update";

        public DynuUpdater(AppConfig config)
        {
            _config = config;
            _webClient = new WebClient();
            _webClient.Headers.Add("User-Agent", "DynuIpUpdater/1.0");
            
            // Enable TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        }

        public string GetPublicIp()
        {
            foreach (var url in IpDetectionUrls)
            {
                try
                {
                    string response = _webClient.DownloadString(url);
                    string ip = response.Trim();

                    // Validate IP format
                    IPAddress parsed;
                    if (IPAddress.TryParse(ip, out parsed) &&
                        parsed.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return ip;
                    }
                }
                catch
                {
                    // Try next service
                    continue;
                }
            }

            return null;
        }

        public string GetPublicIpv6()
        {
            foreach (var url in Ipv6DetectionUrls)
            {
                try
                {
                    string response = _webClient.DownloadString(url);
                    string ip = response.Trim();

                    // Validate IPv6 format
                    IPAddress parsed;
                    if (IPAddress.TryParse(ip, out parsed) &&
                        parsed.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        return ip;
                    }
                }
                catch
                {
                    // Try next service
                    continue;
                }
            }

            return null;
        }

        public UpdateResult UpdateIp(bool forceUpdate = false)
        {
            var result = new UpdateResult();

            try
            {
                // Get current public IP
                string currentIp = null;
                string currentIpv6 = null;

                if (_config.UpdateIpv4)
                {
                    currentIp = GetPublicIp();
                    if (currentIp == null)
                    {
                        result.Message = "Failed to detect public IPv4 address";
                        Logger.LogError(result.Message);
                        return result;
                    }
                }

                if (_config.UpdateIpv6)
                {
                    currentIpv6 = GetPublicIpv6();
                    // IPv6 is optional, don't fail if not available
                    if (currentIpv6 == null)
                    {
                        Logger.LogWarning("IPv6 address not detected");
                    }
                }

                // Check if IP has changed
                if (!forceUpdate && currentIp == _config.LastKnownIp)
                {
                    result.Success = true;
                    result.Message = "IP unchanged: " + currentIp;
                    result.NewIp = currentIp ?? string.Empty;
                    return result;
                }

                // Build update URL
                var queryParams = new StringBuilder();

                // Password (use SHA256 hash if configured)
                string password = _config.UsePasswordHash
                    ? ComputeSha256Hash(_config.Password)
                    : _config.Password;

                queryParams.AppendFormat("password={0}", Uri.EscapeDataString(password));

                // Username or Group or Hostname
                if (!string.IsNullOrEmpty(_config.Group))
                {
                    // Group update - requires username
                    queryParams.AppendFormat("&username={0}", Uri.EscapeDataString(_config.Username));
                    queryParams.AppendFormat("&group={0}", Uri.EscapeDataString(_config.Group));
                }
                else if (!string.IsNullOrEmpty(_config.Hostnames))
                {
                    // Specific hostname(s) update
                    queryParams.AppendFormat("&hostname={0}", Uri.EscapeDataString(_config.Hostnames));
                }
                else
                {
                    // Update all hostnames for username
                    queryParams.AppendFormat("&username={0}", Uri.EscapeDataString(_config.Username));
                }

                // IP addresses
                if (_config.UpdateIpv4 && !string.IsNullOrEmpty(currentIp))
                {
                    queryParams.AppendFormat("&myip={0}", currentIp);
                }

                if (_config.UpdateIpv6 && !string.IsNullOrEmpty(currentIpv6))
                {
                    queryParams.AppendFormat("&myipv6={0}", currentIpv6);
                }

                string updateUrl = string.Format("{0}?{1}", DynuUpdateUrl, queryParams);

                // Make the update request
                string response = _webClient.DownloadString(updateUrl);
                result.ResponseCode = response.Trim();

                // Parse response
                result = ParseResponse(result.ResponseCode, currentIp ?? string.Empty, _config.LastKnownIp);

                // Update config if successful
                if (result.Success && !string.IsNullOrEmpty(currentIp))
                {
                    string oldIp = _config.LastKnownIp;
                    _config.LastKnownIp = currentIp;
                    _config.LastUpdateTime = DateTime.Now;
                    ConfigManager.Save(_config);

                    // Log IP change if different
                    if (oldIp != currentIp && !string.IsNullOrEmpty(oldIp))
                    {
                        Logger.LogIpChange(oldIp, currentIp);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                result.Message = "Update failed: " + ex.Message;
                Logger.LogError(result.Message);
                return result;
            }
        }

        private UpdateResult ParseResponse(string responseCode, string newIp, string oldIp)
        {
            var result = new UpdateResult
            {
                ResponseCode = responseCode,
                NewIp = newIp
            };

            // Response may contain additional info after the code
            string code = responseCode.Split(' ')[0].ToLower();

            switch (code)
            {
                case "good":
                    result.Success = true;
                    result.Message = "IP updated successfully to " + newIp;
                    Logger.Log(result.Message);
                    break;

                case "nochg":
                    result.Success = true;
                    result.Message = "IP unchanged: " + newIp;
                    Logger.Log(result.Message);
                    break;

                case "badauth":
                    result.Message = "Authentication failed. Check username/password/group settings.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "notfqdn":
                    result.Message = "Hostname is not a valid fully qualified domain name.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "nohost":
                    result.Message = "Hostname does not exist in your account.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "abuse":
                    result.Message = "Account blocked for abuse. Contact Dynu support.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "dnserr":
                    result.Message = "DNS error on Dynu server. Try again later.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "911":
                    result.Message = "Dynu server error. Try again later.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                case "unknown":
                    result.Message = "Unknown error. Check your parameters.";
                    Logger.LogError("Dynu response: " + responseCode + " - " + result.Message);
                    break;

                default:
                    result.Message = "Unknown response: " + responseCode;
                    Logger.LogWarning("Dynu response: " + responseCode);
                    break;
            }

            return result;
        }

        private static string ComputeSha256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void Dispose()
        {
            if (_webClient != null)
            {
                _webClient.Dispose();
            }
        }
    }
}
