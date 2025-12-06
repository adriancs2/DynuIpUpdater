using System;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.ServiceProcess;
using System.Windows.Forms;

namespace DynuIpUpdater
{
    public static class ServiceManager
    {
        private const string ServiceName = "DynuIPUpdater";
        private const string ServiceDisplayName = "Dynu IP Updater Service";
        private const string ServiceDescription = "Automatically updates your public IP address with Dynu.com Dynamic DNS service.";

        public static bool IsAdministrator()
        {
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        public static bool IsServiceInstalled()
        {
            try
            {
                using (var sc = new ServiceController(ServiceName))
                {
                    var status = sc.Status;
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static ServiceControllerStatus? GetServiceStatus()
        {
            try
            {
                using (var sc = new ServiceController(ServiceName))
                {
                    return sc.Status;
                }
            }
            catch
            {
                return null;
            }
        }

        public static ServiceResult InstallService(int intervalMinutes)
        {
            if (!IsAdministrator())
            {
                return new ServiceResult(false, "Administrator privileges required. Please run as Administrator.");
            }

            try
            {
                string exePath = Application.ExecutablePath;
                string scPath = Path.Combine(Environment.SystemDirectory, "sc.exe");

                // Create the service using sc.exe
                var createProcess = new ProcessStartInfo
                {
                    FileName = scPath,
                    Arguments = string.Format("create {0} binPath= \"\\\"{1}\\\" --silent\" start= auto DisplayName= \"{2}\"", 
                        ServiceName, exePath, ServiceDisplayName),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(createProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        if (process.ExitCode != 0)
                        {
                            string error = process.StandardError.ReadToEnd();
                            return new ServiceResult(false, "Failed to create service: " + error);
                        }
                    }
                }

                // Set description
                var descProcess = new ProcessStartInfo
                {
                    FileName = scPath,
                    Arguments = string.Format("description {0} \"{1}\"", ServiceName, ServiceDescription),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(descProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                    }
                }

                // Create scheduled task instead (more reliable for this use case)
                // Since Windows Service requires different architecture, we'll use Task Scheduler
                return CreateScheduledTask(intervalMinutes);
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "Failed to install service: " + ex.Message);
            }
        }

        public static ServiceResult CreateScheduledTask(int intervalMinutes)
        {
            if (!IsAdministrator())
            {
                return new ServiceResult(false, "Administrator privileges required. Please run as Administrator.");
            }

            try
            {
                string exePath = Application.ExecutablePath;
                string taskName = "DynuIPUpdater";

                // Delete existing task if exists
                var deleteProcess = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Delete /TN \"{0}\" /F", taskName),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(deleteProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                    }
                }

                // Create new scheduled task
                var createProcess = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Create /TN \"{0}\" /TR \"\\\"{1}\\\" --silent\" /SC MINUTE /MO {2} /RU SYSTEM /RL HIGHEST /F",
                        taskName, exePath, intervalMinutes),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(createProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        string output = process.StandardOutput.ReadToEnd();
                        string error = process.StandardError.ReadToEnd();

                        if (process.ExitCode == 0)
                        {
                            Logger.Log(string.Format("Scheduled task created: runs every {0} minutes", intervalMinutes));
                            return new ServiceResult(true, 
                                string.Format("Scheduled task '{0}' created successfully.\nIP will be updated every {1} minutes.", 
                                taskName, intervalMinutes));
                        }
                        else
                        {
                            return new ServiceResult(false, "Failed to create scheduled task: " + error);
                        }
                    }
                }

                return new ServiceResult(false, "Failed to start process");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "Failed to create scheduled task: " + ex.Message);
            }
        }

        public static ServiceResult RemoveScheduledTask()
        {
            if (!IsAdministrator())
            {
                return new ServiceResult(false, "Administrator privileges required. Please run as Administrator.");
            }

            try
            {
                string taskName = "DynuIPUpdater";

                var deleteProcess = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Delete /TN \"{0}\" /F", taskName),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(deleteProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        string error = process.StandardError.ReadToEnd();

                        if (process.ExitCode == 0)
                        {
                            Logger.Log("Scheduled task removed");
                            return new ServiceResult(true, "Scheduled task removed successfully.");
                        }
                        else
                        {
                            return new ServiceResult(false, "Failed to remove scheduled task: " + error);
                        }
                    }
                }

                return new ServiceResult(false, "Failed to start process");
            }
            catch (Exception ex)
            {
                return new ServiceResult(false, "Failed to remove scheduled task: " + ex.Message);
            }
        }

        public static bool IsScheduledTaskInstalled()
        {
            try
            {
                string taskName = "DynuIPUpdater";

                var queryProcess = new ProcessStartInfo
                {
                    FileName = "schtasks.exe",
                    Arguments = string.Format("/Query /TN \"{0}\"", taskName),
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(queryProcess))
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        return process.ExitCode == 0;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public static void RunAsAdministrator(string arguments = "")
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Arguments = arguments,
                    UseShellExecute = true,
                    Verb = "runas"
                };

                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to elevate privileges: " + ex.Message);
            }
        }
    }

    public class ServiceResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ServiceResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
