using System;
using System.Threading;
using System.Windows.Forms;

namespace DynuIpUpdater
{
    internal static class Program
    {
        private static Mutex _mutex;

        [STAThread]
        static void Main(string[] args)
        {
            // Single instance check
            const string mutexName = "DynuIpUpdater_SingleInstance";
            bool createdNew;
            _mutex = new Mutex(true, mutexName, out createdNew);

            if (!createdNew)
            {
                MessageBox.Show("DynuIpUpdater is already running.", "Dynu IP Updater", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Parse command line arguments
                var options = CommandLineParser.Parse(args);

                if (options.ShowHelp)
                {
                    ShowHelp();
                    return;
                }

                if (options.RunSilent)
                {
                    // Run in silent mode (background update)
                    RunSilentMode(options);
                }
                else
                {
                    // Show the configuration form
                    Application.Run(new MainForm());
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Fatal error: " + ex.Message);
                if (!CommandLineParser.Parse(args).RunSilent)
                {
                    MessageBox.Show("Fatal error: " + ex.Message, "Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            finally
            {
                if (_mutex != null)
                {
                    _mutex.ReleaseMutex();
                    _mutex.Dispose();
                }
            }
        }

        private static void RunSilentMode(CommandLineOptions options)
        {
            Logger.Log("Application started in silent mode");

            try
            {
                var config = ConfigManager.Load();

                if (string.IsNullOrEmpty(config.Username) || string.IsNullOrEmpty(config.Password))
                {
                    Logger.LogError("Silent mode failed: No credentials configured. Please run the application in GUI mode first to configure.");
                    return;
                }

                // Perform single update
                var updater = new DynuUpdater(config);
                var result = updater.UpdateIp();

                if (result.Success)
                {
                    Logger.Log("IP update successful: " + result.Message);
                }
                else
                {
                    Logger.LogError("IP update failed: " + result.Message);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError("Silent mode error: " + ex.Message);
            }
        }

        private static void ShowHelp()
        {
            string help = @"
DynuIpUpdater - Dynamic DNS IP Updater for Dynu.com

Usage: DynuIpUpdater.exe [options]

Options:
  -s, --silent     Run in silent mode (perform update and exit)
  -h, --help       Show this help message

Examples:
  DynuIpUpdater.exe              Launch GUI for configuration
  DynuIpUpdater.exe --silent     Perform IP update silently (background)
  DynuIpUpdater.exe -s           Same as --silent

Note: 
  - Run the application without parameters first to configure credentials.
  - Silent mode requires credentials to be configured via the GUI first.
  - Use Windows Task Scheduler or the built-in service installer for automatic updates.
";
            MessageBox.Show(help, "Dynu IP Updater - Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    public class CommandLineOptions
    {
        public bool RunSilent { get; set; }
        public bool ShowHelp { get; set; }
    }

    public static class CommandLineParser
    {
        public static CommandLineOptions Parse(string[] args)
        {
            var options = new CommandLineOptions();

            foreach (var arg in args)
            {
                string lowerArg = arg.ToLowerInvariant();

                switch (lowerArg)
                {
                    case "-s":
                    case "--silent":
                    case "/silent":
                        options.RunSilent = true;
                        break;
                    case "-h":
                    case "--help":
                    case "/help":
                    case "/?":
                        options.ShowHelp = true;
                        break;
                }
            }

            return options;
        }
    }
}
