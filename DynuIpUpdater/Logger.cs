using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DynuIpUpdater
{
    public static class Logger
    {
        private static readonly object _lock = new object();
        private static readonly string LogFolder = GetLogFolder();
        private static readonly string LogFile = Path.Combine(LogFolder, "log.txt");

        private static string GetLogFolder()
        {
            // Store in %APPDATA%\DynuIpUpdater for proper Windows permissions
            string appDataFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                "DynuIpUpdater");
            
            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
            
            return appDataFolder;
        }
        private const long MaxLogSize = 5 * 1024 * 1024; // 5MB
        private const int MaxBackupFiles = 5;

        public static void Log(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void LogIpChange(string oldIp, string newIp)
        {
            WriteLog("IP_CHANGE", string.Format("IP changed from [{0}] to [{1}]", oldIp, newIp));
        }

        private static void WriteLog(string level, string message)
        {
            lock (_lock)
            {
                try
                {
                    // Check if rotation is needed
                    RotateLogIfNeeded();

                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string logEntry = string.Format("{0} [{1}] {2}", timestamp, level, message);

                    // Append to log file
                    using (var writer = new StreamWriter(LogFile, true, Encoding.UTF8))
                    {
                        writer.WriteLine(logEntry);
                    }
                }
                catch
                {
                    // Silently fail - we don't want logging to crash the app
                }
            }
        }

        private static void RotateLogIfNeeded()
        {
            try
            {
                if (!File.Exists(LogFile))
                    return;

                var fileInfo = new FileInfo(LogFile);
                if (fileInfo.Length < MaxLogSize)
                    return;

                // Need to rotate
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd_HHmmss");
                string backupFileName = Path.Combine(LogFolder, string.Format("log_bak_{0}.txt", timestamp));

                // Close any handles and rename current log
                File.Move(LogFile, backupFileName);

                // Clean up old backup files
                CleanupOldBackups();
            }
            catch
            {
                // Silently fail
            }
        }

        private static void CleanupOldBackups()
        {
            try
            {
                var backupFiles = Directory.GetFiles(LogFolder, "log_bak_*.txt");

                if (backupFiles.Length <= MaxBackupFiles)
                    return;

                // Sort by creation time and delete oldest
                Array.Sort(backupFiles, (a, b) =>
                    File.GetCreationTime(a).CompareTo(File.GetCreationTime(b)));

                int filesToDelete = backupFiles.Length - MaxBackupFiles;
                for (int i = 0; i < filesToDelete; i++)
                {
                    File.Delete(backupFiles[i]);
                }
            }
            catch
            {
                // Silently fail
            }
        }

        public static string GetLogContent(int maxLines = 500)
        {
            lock (_lock)
            {
                try
                {
                    if (!File.Exists(LogFile))
                        return "(No log entries yet)";

                    var lines = File.ReadAllLines(LogFile, Encoding.UTF8);

                    if (lines.Length <= maxLines)
                    {
                        return string.Join(Environment.NewLine, lines);
                    }

                    // Return last N lines
                    var lastLines = new string[maxLines];
                    Array.Copy(lines, lines.Length - maxLines, lastLines, 0, maxLines);
                    return string.Join(Environment.NewLine, lastLines);
                }
                catch (Exception ex)
                {
                    return "(Error reading log: " + ex.Message + ")";
                }
            }
        }

        public static void ClearLog()
        {
            lock (_lock)
            {
                try
                {
                    if (File.Exists(LogFile))
                        File.Delete(LogFile);

                    Log("Log cleared");
                }
                catch
                {
                    // Silently fail
                }
            }
        }
    }
}
