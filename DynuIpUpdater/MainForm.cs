using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DynuIpUpdater
{
    public partial class MainForm : Form
    {
        private AppConfig _config;
        private DynuUpdater _updater;

        public MainForm()
        {
            InitializeComponent();
            _config = new AppConfig();
            SetupEventHandlers();
            LoadConfiguration();
            RefreshStatus();
            Logger.Log("Application started in GUI mode");
        }

        private void SetupEventHandlers()
        {
            chkShowPassword.CheckedChanged += delegate { txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '●'; };
            rbUpdateAll.CheckedChanged += UpdateTargetChanged;
            rbUpdateGroup.CheckedChanged += UpdateTargetChanged;
            rbUpdateHostnames.CheckedChanged += UpdateTargetChanged;
            btnCheckIp.Click += BtnCheckIp_Click;
            btnTestConnection.Click += BtnTestConnection_Click;
            btnUpdateNow.Click += BtnUpdateNow_Click;
            btnSaveSettings.Click += BtnSaveSettings_Click;
            btnInstallService.Click += BtnInstallService_Click;
            btnRemoveService.Click += BtnRemoveService_Click;
            btnRefreshLog.Click += BtnRefreshLog_Click;
            btnClearLog.Click += BtnClearLog_Click;
            btnOpenLogFile.Click += BtnOpenLogFile_Click;
            tabControl.SelectedIndexChanged += delegate { if (tabControl.SelectedTab == tabLog) RefreshLog(); };
            lnkGitHub.LinkClicked += LnkGitHub_LinkClicked;
            lnkDownload.LinkClicked += LnkDownload_LinkClicked;
            this.Load += MainForm_Load;
            this.FormClosing += MainForm_FormClosing;
        }

        private void LnkGitHub_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/adriancs2/DynuIpUpdater");
        }

        private void LnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/adriancs2/DynuIpUpdater/releases");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            refreshTimer.Start();
            UpdateServiceStatus();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Stop();
            if (_updater != null) _updater.Dispose();
        }

        private void LoadConfiguration()
        {
            try
            {
                _config = ConfigManager.Load();
                txtUsername.Text = _config.Username;
                txtPassword.Text = _config.Password;
                txtGroup.Text = _config.Group;
                txtHostnames.Text = _config.Hostnames;
                numInterval.Value = Math.Max(1, Math.Min(1440, _config.UpdateIntervalMinutes));
                chkUpdateIpv4.Checked = _config.UpdateIpv4;
                chkUpdateIpv6.Checked = _config.UpdateIpv6;
                chkUsePasswordHash.Checked = _config.UsePasswordHash;

                if (!string.IsNullOrEmpty(_config.Group)) rbUpdateGroup.Checked = true;
                else if (!string.IsNullOrEmpty(_config.Hostnames)) rbUpdateHostnames.Checked = true;
                else rbUpdateAll.Checked = true;

                UpdateTargetChanged(this, EventArgs.Empty);
            }
            catch (Exception ex) { Logger.LogError("Failed to load configuration: " + ex.Message); }
        }

        private void SaveConfiguration()
        {
            try
            {
                SaveConfigFromUI();
                ConfigManager.Save(_config);
                SetStatus("Settings saved successfully", false);
            }
            catch (Exception ex)
            {
                SetStatus("Failed to save settings: " + ex.Message, true);
                Logger.LogError("Failed to save configuration: " + ex.Message);
            }
        }

        private void UpdateTargetChanged(object sender, EventArgs e)
        {
            txtGroup.Enabled = rbUpdateGroup.Checked;
            txtHostnames.Enabled = rbUpdateHostnames.Checked;
        }

        private void BtnCheckIp_Click(object sender, EventArgs e)
        {
            btnCheckIp.Enabled = false;
            btnCheckIp.Text = "Checking...";
            SetStatus("Checking public IP...", false);
            try
            {
                using (var updater = new DynuUpdater(new AppConfig()))
                {
                    string ip = updater.GetPublicIp();
                    if (ip != null)
                    {
                        txtCurrentIp.Text = ip;
                        SetStatus("Public IP: " + ip, false);
                    }
                    else
                    {
                        SetStatus("Failed to detect public IP address", true);
                    }
                }
            }
            catch (Exception ex)
            {
                SetStatus("Check IP failed: " + ex.Message, true);
            }
            finally
            {
                btnCheckIp.Enabled = true;
                btnCheckIp.Text = "Check IP";
            }
        }

        private void BtnTestConnection_Click(object sender, EventArgs e)
        {
            SetButtonsEnabled(false);
            SetStatus("Testing connection...", false);
            try
            {
                SaveConfigFromUI();
                _updater = new DynuUpdater(_config);
                string ip = _updater.GetPublicIp();
                if (ip != null)
                {
                    txtCurrentIp.Text = ip;
                    SetStatus("Connection successful. Public IP: " + ip, false);
                    Logger.Log("Test connection successful. IP: " + ip);
                }
                else SetStatus("Failed to detect public IP address", true);
            }
            catch (Exception ex)
            {
                SetStatus("Connection test failed: " + ex.Message, true);
                Logger.LogError("Connection test failed: " + ex.Message);
            }
            finally { SetButtonsEnabled(true); }
        }

        private void BtnUpdateNow_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            SetButtonsEnabled(false);
            SetStatus("Updating IP address...", false);
            try
            {
                SaveConfigFromUI();
                _updater = new DynuUpdater(_config);
                var result = _updater.UpdateIp(true);
                if (result.Success)
                {
                    txtCurrentIp.Text = result.NewIp;
                    _config.LastKnownIp = result.NewIp;
                    _config.LastUpdateTime = DateTime.Now;
                    txtLastUpdate.Text = _config.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    SetStatus(result.Message, false);
                }
                else SetStatus(result.Message, true);
            }
            catch (Exception ex)
            {
                SetStatus("Update failed: " + ex.Message, true);
                Logger.LogError("Manual update failed: " + ex.Message);
            }
            finally { SetButtonsEnabled(true); }
        }

        private void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            SaveConfiguration();
        }

        private void BtnInstallService_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            SaveConfiguration();
            if (!ServiceManager.IsAdministrator())
            {
                if (MessageBox.Show("Installing requires admin privileges.\n\nRestart as Administrator?",
                    "Administrator Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try { ServiceManager.RunAsAdministrator(); Application.Exit(); }
                    catch (Exception ex) { MessageBox.Show("Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                return;
            }
            var result = ServiceManager.CreateScheduledTask((int)numInterval.Value);
            MessageBox.Show(result.Message, result.Success ? "Success" : "Error", MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            UpdateServiceStatus();
        }

        private void BtnRemoveService_Click(object sender, EventArgs e)
        {
            if (!ServiceManager.IsAdministrator())
            {
                if (MessageBox.Show("Removing requires admin privileges.\n\nRestart as Administrator?",
                    "Administrator Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try { ServiceManager.RunAsAdministrator(); Application.Exit(); }
                    catch (Exception ex) { MessageBox.Show("Failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
                return;
            }
            if (MessageBox.Show("Remove the scheduled task?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            var result = ServiceManager.RemoveScheduledTask();
            MessageBox.Show(result.Message, result.Success ? "Success" : "Error", MessageBoxButtons.OK,
                result.Success ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            UpdateServiceStatus();
        }

        private void BtnRefreshLog_Click(object sender, EventArgs e) { RefreshLog(); }

        private void BtnClearLog_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear the log?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Logger.ClearLog();
                RefreshLog();
            }
        }

        private void BtnOpenLogFile_Click(object sender, EventArgs e)
        {
            try
            {
                string logPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? ".", "log.txt");
                if (File.Exists(logPath)) Process.Start(logPath);
                else MessageBox.Show("Log file does not exist yet.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show("Failed to open log: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void refreshTimer_Tick(object sender, EventArgs e) { RefreshStatus(); }

        private void RefreshStatus()
        {
            try
            {
                _config = ConfigManager.Load();
                txtCurrentIp.Text = _config.LastKnownIp;
                txtLastUpdate.Text = _config.LastUpdateTime == DateTime.MinValue ? "(Never)" : _config.LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                UpdateServiceStatus();
            }
            catch { }
        }

        private void RefreshLog()
        {
            try
            {
                txtLog.Text = Logger.GetLogContent(500);
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();
            }
            catch (Exception ex) { txtLog.Text = "Error reading log: " + ex.Message; }
        }

        private void UpdateServiceStatus()
        {
            bool installed = ServiceManager.IsScheduledTaskInstalled();
            btnInstallService.Text = installed ? "Reinstall Scheduled Task" : "Install Scheduled Task";
            btnRemoveService.Enabled = installed;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text)) { MessageBox.Show("Enter username.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtUsername.Focus(); return false; }
            if (string.IsNullOrWhiteSpace(txtPassword.Text)) { MessageBox.Show("Enter password.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtPassword.Focus(); return false; }
            if (rbUpdateGroup.Checked && string.IsNullOrWhiteSpace(txtGroup.Text)) { MessageBox.Show("Enter group name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtGroup.Focus(); return false; }
            if (rbUpdateHostnames.Checked && string.IsNullOrWhiteSpace(txtHostnames.Text)) { MessageBox.Show("Enter hostnames.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); txtHostnames.Focus(); return false; }
            if (!chkUpdateIpv4.Checked && !chkUpdateIpv6.Checked) { MessageBox.Show("Select IPv4 or IPv6.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }

        private void SaveConfigFromUI()
        {
            _config.Username = txtUsername.Text.Trim();
            _config.Password = txtPassword.Text;
            _config.UpdateIntervalMinutes = (int)numInterval.Value;
            _config.UpdateIpv4 = chkUpdateIpv4.Checked;
            _config.UpdateIpv6 = chkUpdateIpv6.Checked;
            _config.UsePasswordHash = chkUsePasswordHash.Checked;
            if (rbUpdateGroup.Checked) { _config.Group = txtGroup.Text.Trim(); _config.Hostnames = string.Empty; }
            else if (rbUpdateHostnames.Checked) { _config.Group = string.Empty; _config.Hostnames = txtHostnames.Text.Trim(); }
            else { _config.Group = string.Empty; _config.Hostnames = string.Empty; }
        }

        private void SetStatus(string message, bool isError)
        {
            txtStatus.Text = message;
            txtStatus.ForeColor = isError ? Color.Red : Color.DarkGreen;
        }

        private void SetButtonsEnabled(bool enabled)
        {
            btnTestConnection.Enabled = enabled;
            btnUpdateNow.Enabled = enabled;
            btnSaveSettings.Enabled = enabled;
            btnInstallService.Enabled = enabled;
            btnRemoveService.Enabled = enabled && ServiceManager.IsScheduledTaskInstalled();
        }
    }
}
