using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace DynuIpUpdater
{
    public class AppConfig
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Group { get; set; }
        public string Hostnames { get; set; }
        public int UpdateIntervalMinutes { get; set; }
        public bool UpdateIpv4 { get; set; }
        public bool UpdateIpv6 { get; set; }
        public bool UsePasswordHash { get; set; }
        public string LastKnownIp { get; set; }
        public DateTime LastUpdateTime { get; set; }

        public AppConfig()
        {
            Username = string.Empty;
            Password = string.Empty;
            Group = string.Empty;
            Hostnames = string.Empty;
            UpdateIntervalMinutes = 5;
            UpdateIpv4 = true;
            UpdateIpv6 = false;
            UsePasswordHash = true;
            LastKnownIp = string.Empty;
            LastUpdateTime = DateTime.MinValue;
        }
    }

    public static class ConfigManager
    {
        private static readonly string ConfigFolder = GetConfigFolder();
        private static readonly string ConfigFile = Path.Combine(ConfigFolder, "config.json");
        private static readonly string KeyFile = Path.Combine(ConfigFolder, "config.key");

        private static string GetConfigFolder()
        {
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DynuIpUpdater");
            
            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
            
            return appDataFolder;
        }

        private static byte[] GetOrCreateKey()
        {
            if (File.Exists(KeyFile))
            {
                return File.ReadAllBytes(KeyFile);
            }

            // Generate a new 256-bit key
            byte[] key = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
            }

            // Mix with machine-specific data
            string machineData = Environment.MachineName + Environment.UserName + Environment.ProcessorCount;
            byte[] machineBytes = Encoding.UTF8.GetBytes(machineData);
            for (int i = 0; i < key.Length && i < machineBytes.Length; i++)
            {
                key[i] ^= machineBytes[i % machineBytes.Length];
            }

            File.WriteAllBytes(KeyFile, key);
            File.SetAttributes(KeyFile, FileAttributes.Hidden);
            return key;
        }

        private static string Encrypt(string plainText)
        {
            byte[] key = GetOrCreateKey();
            
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream())
                {
                    // Write IV first
                    ms.Write(aes.IV, 0, aes.IV.Length);

                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    using (var sw = new StreamWriter(cs, Encoding.UTF8))
                    {
                        sw.Write(plainText);
                    }

                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static string Decrypt(string cipherText)
        {
            byte[] key = GetOrCreateKey();
            byte[] fullCipher = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = key;

                // Extract IV from beginning
                byte[] iv = new byte[16];
                Array.Copy(fullCipher, 0, iv, 0, iv.Length);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var ms = new MemoryStream(fullCipher, iv.Length, fullCipher.Length - iv.Length))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public static AppConfig Load()
        {
            try
            {
                if (!File.Exists(ConfigFile))
                {
                    return new AppConfig();
                }

                string encrypted = File.ReadAllText(ConfigFile, Encoding.UTF8);
                string json = Decrypt(encrypted);

                var serializer = new JavaScriptSerializer();
                var config = serializer.Deserialize<AppConfig>(json);
                return config ?? new AppConfig();
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to load config: " + ex.Message);
                return new AppConfig();
            }
        }

        public static void Save(AppConfig config)
        {
            try
            {
                var serializer = new JavaScriptSerializer();
                string json = serializer.Serialize(config);
                string encrypted = Encrypt(json);

                File.WriteAllText(ConfigFile, encrypted, Encoding.UTF8);

                Logger.Log("Configuration saved successfully");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to save config: " + ex.Message);
                throw;
            }
        }

        public static void Delete()
        {
            try
            {
                if (File.Exists(ConfigFile))
                    File.Delete(ConfigFile);
                if (File.Exists(KeyFile))
                    File.Delete(KeyFile);

                Logger.Log("Configuration deleted");
            }
            catch (Exception ex)
            {
                Logger.LogError("Failed to delete config: " + ex.Message);
            }
        }

        public static bool ConfigExists()
        {
            return File.Exists(ConfigFile);
        }
    }
}
