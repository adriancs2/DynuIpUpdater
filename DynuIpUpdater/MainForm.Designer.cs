namespace DynuIpUpdater
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnRemoveService = new System.Windows.Forms.Button();
            this.btnInstallService = new System.Windows.Forms.Button();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.btnUpdateNow = new System.Windows.Forms.Button();
            this.btnTestConnection = new System.Windows.Forms.Button();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.txtLastUpdate = new System.Windows.Forms.TextBox();
            this.lblLastUpdate = new System.Windows.Forms.Label();
            this.btnCheckIp = new System.Windows.Forms.Button();
            this.txtCurrentIp = new System.Windows.Forms.TextBox();
            this.lblCurrentIp = new System.Windows.Forms.Label();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.chkUpdateIpv6 = new System.Windows.Forms.CheckBox();
            this.chkUpdateIpv4 = new System.Windows.Forms.CheckBox();
            this.lblIntervalUnit = new System.Windows.Forms.Label();
            this.numInterval = new System.Windows.Forms.NumericUpDown();
            this.lblInterval = new System.Windows.Forms.Label();
            this.grpUpdateTarget = new System.Windows.Forms.GroupBox();
            this.lblHostnamesHint = new System.Windows.Forms.Label();
            this.txtHostnames = new System.Windows.Forms.TextBox();
            this.lblHostnames = new System.Windows.Forms.Label();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.rbUpdateHostnames = new System.Windows.Forms.RadioButton();
            this.rbUpdateGroup = new System.Windows.Forms.RadioButton();
            this.rbUpdateAll = new System.Windows.Forms.RadioButton();
            this.grpCredentials = new System.Windows.Forms.GroupBox();
            this.chkUsePasswordHash = new System.Windows.Forms.CheckBox();
            this.chkShowPassword = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.btnOpenLogFile = new System.Windows.Forms.Button();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.btnRefreshLog = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tabAbout = new System.Windows.Forms.TabPage();
            this.lblAboutTitle = new System.Windows.Forms.Label();
            this.lblAboutDesc = new System.Windows.Forms.Label();
            this.lnkGitHub = new System.Windows.Forms.LinkLabel();
            this.lnkDownload = new System.Windows.Forms.LinkLabel();
            this.lblOpenSource = new System.Windows.Forms.Label();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblDisclaimer = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.grpActions.SuspendLayout();
            this.grpStatus.SuspendLayout();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
            this.grpUpdateTarget.SuspendLayout();
            this.grpCredentials.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabSettings);
            this.tabControl.Controls.Add(this.tabLog);
            this.tabControl.Controls.Add(this.tabAbout);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(584, 661);
            this.tabControl.TabIndex = 0;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.grpActions);
            this.tabSettings.Controls.Add(this.grpStatus);
            this.tabSettings.Controls.Add(this.grpOptions);
            this.tabSettings.Controls.Add(this.grpUpdateTarget);
            this.tabSettings.Controls.Add(this.grpCredentials);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Padding = new System.Windows.Forms.Padding(10);
            this.tabSettings.Size = new System.Drawing.Size(576, 635);
            this.tabSettings.TabIndex = 0;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.btnRemoveService);
            this.grpActions.Controls.Add(this.btnInstallService);
            this.grpActions.Controls.Add(this.btnSaveSettings);
            this.grpActions.Controls.Add(this.btnUpdateNow);
            this.grpActions.Controls.Add(this.btnTestConnection);
            this.grpActions.Location = new System.Drawing.Point(13, 492);
            this.grpActions.Name = "grpActions";
            this.grpActions.Size = new System.Drawing.Size(550, 130);
            this.grpActions.TabIndex = 4;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Actions";
            // 
            // btnRemoveService
            // 
            this.btnRemoveService.Location = new System.Drawing.Point(205, 80);
            this.btnRemoveService.Name = "btnRemoveService";
            this.btnRemoveService.Size = new System.Drawing.Size(180, 35);
            this.btnRemoveService.TabIndex = 4;
            this.btnRemoveService.Text = "Remove Scheduled Task";
            this.btnRemoveService.UseVisualStyleBackColor = true;
            // 
            // btnInstallService
            // 
            this.btnInstallService.Location = new System.Drawing.Point(15, 80);
            this.btnInstallService.Name = "btnInstallService";
            this.btnInstallService.Size = new System.Drawing.Size(180, 35);
            this.btnInstallService.TabIndex = 3;
            this.btnInstallService.Text = "Install Scheduled Task";
            this.btnInstallService.UseVisualStyleBackColor = true;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Location = new System.Drawing.Point(275, 30);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(120, 35);
            this.btnSaveSettings.TabIndex = 2;
            this.btnSaveSettings.Text = "Save Settings";
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            // 
            // btnUpdateNow
            // 
            this.btnUpdateNow.Location = new System.Drawing.Point(145, 30);
            this.btnUpdateNow.Name = "btnUpdateNow";
            this.btnUpdateNow.Size = new System.Drawing.Size(120, 35);
            this.btnUpdateNow.TabIndex = 1;
            this.btnUpdateNow.Text = "Update Now";
            this.btnUpdateNow.UseVisualStyleBackColor = true;
            // 
            // btnTestConnection
            // 
            this.btnTestConnection.Location = new System.Drawing.Point(15, 30);
            this.btnTestConnection.Name = "btnTestConnection";
            this.btnTestConnection.Size = new System.Drawing.Size(120, 35);
            this.btnTestConnection.TabIndex = 0;
            this.btnTestConnection.Text = "Test Connection";
            this.btnTestConnection.UseVisualStyleBackColor = true;
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.txtStatus);
            this.grpStatus.Controls.Add(this.lblStatus);
            this.grpStatus.Controls.Add(this.txtLastUpdate);
            this.grpStatus.Controls.Add(this.lblLastUpdate);
            this.grpStatus.Controls.Add(this.btnCheckIp);
            this.grpStatus.Controls.Add(this.txtCurrentIp);
            this.grpStatus.Controls.Add(this.lblCurrentIp);
            this.grpStatus.Location = new System.Drawing.Point(13, 371);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(550, 115);
            this.grpStatus.TabIndex = 3;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(120, 85);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(415, 20);
            this.txtStatus.TabIndex = 5;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 88);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(40, 13);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Status:";
            // 
            // txtLastUpdate
            // 
            this.txtLastUpdate.Location = new System.Drawing.Point(120, 55);
            this.txtLastUpdate.Name = "txtLastUpdate";
            this.txtLastUpdate.ReadOnly = true;
            this.txtLastUpdate.Size = new System.Drawing.Size(200, 20);
            this.txtLastUpdate.TabIndex = 3;
            // 
            // lblLastUpdate
            // 
            this.lblLastUpdate.AutoSize = true;
            this.lblLastUpdate.Location = new System.Drawing.Point(15, 58);
            this.lblLastUpdate.Name = "lblLastUpdate";
            this.lblLastUpdate.Size = new System.Drawing.Size(68, 13);
            this.lblLastUpdate.TabIndex = 2;
            this.lblLastUpdate.Text = "Last Update:";
            // 
            // btnCheckIp
            // 
            this.btnCheckIp.Location = new System.Drawing.Point(330, 23);
            this.btnCheckIp.Name = "btnCheckIp";
            this.btnCheckIp.Size = new System.Drawing.Size(100, 23);
            this.btnCheckIp.TabIndex = 6;
            this.btnCheckIp.Text = "Check IP";
            this.btnCheckIp.UseVisualStyleBackColor = true;
            // 
            // txtCurrentIp
            // 
            this.txtCurrentIp.Location = new System.Drawing.Point(120, 25);
            this.txtCurrentIp.Name = "txtCurrentIp";
            this.txtCurrentIp.ReadOnly = true;
            this.txtCurrentIp.Size = new System.Drawing.Size(200, 20);
            this.txtCurrentIp.TabIndex = 1;
            // 
            // lblCurrentIp
            // 
            this.lblCurrentIp.AutoSize = true;
            this.lblCurrentIp.Location = new System.Drawing.Point(15, 28);
            this.lblCurrentIp.Name = "lblCurrentIp";
            this.lblCurrentIp.Size = new System.Drawing.Size(57, 13);
            this.lblCurrentIp.TabIndex = 0;
            this.lblCurrentIp.Text = "Current IP:";
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.chkUpdateIpv6);
            this.grpOptions.Controls.Add(this.chkUpdateIpv4);
            this.grpOptions.Controls.Add(this.lblIntervalUnit);
            this.grpOptions.Controls.Add(this.numInterval);
            this.grpOptions.Controls.Add(this.lblInterval);
            this.grpOptions.Location = new System.Drawing.Point(13, 285);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Size = new System.Drawing.Size(550, 80);
            this.grpOptions.TabIndex = 2;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Options";
            // 
            // chkUpdateIpv6
            // 
            this.chkUpdateIpv6.AutoSize = true;
            this.chkUpdateIpv6.Location = new System.Drawing.Point(120, 55);
            this.chkUpdateIpv6.Name = "chkUpdateIpv6";
            this.chkUpdateIpv6.Size = new System.Drawing.Size(86, 17);
            this.chkUpdateIpv6.TabIndex = 4;
            this.chkUpdateIpv6.Text = "Update IPv6";
            this.chkUpdateIpv6.UseVisualStyleBackColor = true;
            // 
            // chkUpdateIpv4
            // 
            this.chkUpdateIpv4.AutoSize = true;
            this.chkUpdateIpv4.Checked = true;
            this.chkUpdateIpv4.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUpdateIpv4.Location = new System.Drawing.Point(15, 55);
            this.chkUpdateIpv4.Name = "chkUpdateIpv4";
            this.chkUpdateIpv4.Size = new System.Drawing.Size(86, 17);
            this.chkUpdateIpv4.TabIndex = 3;
            this.chkUpdateIpv4.Text = "Update IPv4";
            this.chkUpdateIpv4.UseVisualStyleBackColor = true;
            // 
            // lblIntervalUnit
            // 
            this.lblIntervalUnit.AutoSize = true;
            this.lblIntervalUnit.Location = new System.Drawing.Point(205, 30);
            this.lblIntervalUnit.Name = "lblIntervalUnit";
            this.lblIntervalUnit.Size = new System.Drawing.Size(43, 13);
            this.lblIntervalUnit.TabIndex = 2;
            this.lblIntervalUnit.Text = "minutes";
            // 
            // numInterval
            // 
            this.numInterval.Location = new System.Drawing.Point(120, 28);
            this.numInterval.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
            this.numInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numInterval.Name = "numInterval";
            this.numInterval.Size = new System.Drawing.Size(80, 20);
            this.numInterval.TabIndex = 1;
            this.numInterval.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(15, 30);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(83, 13);
            this.lblInterval.TabIndex = 0;
            this.lblInterval.Text = "Update Interval:";
            // 
            // grpUpdateTarget
            // 
            this.grpUpdateTarget.Controls.Add(this.lblHostnamesHint);
            this.grpUpdateTarget.Controls.Add(this.txtHostnames);
            this.grpUpdateTarget.Controls.Add(this.lblHostnames);
            this.grpUpdateTarget.Controls.Add(this.txtGroup);
            this.grpUpdateTarget.Controls.Add(this.lblGroup);
            this.grpUpdateTarget.Controls.Add(this.rbUpdateHostnames);
            this.grpUpdateTarget.Controls.Add(this.rbUpdateGroup);
            this.grpUpdateTarget.Controls.Add(this.rbUpdateAll);
            this.grpUpdateTarget.Location = new System.Drawing.Point(13, 124);
            this.grpUpdateTarget.Name = "grpUpdateTarget";
            this.grpUpdateTarget.Size = new System.Drawing.Size(550, 155);
            this.grpUpdateTarget.TabIndex = 1;
            this.grpUpdateTarget.TabStop = false;
            this.grpUpdateTarget.Text = "Update Target";
            // 
            // lblHostnamesHint
            // 
            this.lblHostnamesHint.AutoSize = true;
            this.lblHostnamesHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblHostnamesHint.Location = new System.Drawing.Point(285, 120);
            this.lblHostnamesHint.Name = "lblHostnamesHint";
            this.lblHostnamesHint.Size = new System.Drawing.Size(280, 13);
            this.lblHostnamesHint.TabIndex = 7;
            this.lblHostnamesHint.Text = "(comma-separated, e.g., host1.dynu.com,host2.dynu.com)";
            // 
            // txtHostnames
            // 
            this.txtHostnames.Enabled = false;
            this.txtHostnames.Location = new System.Drawing.Point(285, 94);
            this.txtHostnames.Name = "txtHostnames";
            this.txtHostnames.Size = new System.Drawing.Size(250, 20);
            this.txtHostnames.TabIndex = 6;
            // 
            // lblHostnames
            // 
            this.lblHostnames.AutoSize = true;
            this.lblHostnames.Location = new System.Drawing.Point(200, 97);
            this.lblHostnames.Name = "lblHostnames";
            this.lblHostnames.Size = new System.Drawing.Size(63, 13);
            this.lblHostnames.TabIndex = 5;
            this.lblHostnames.Text = "Hostnames:";
            // 
            // txtGroup
            // 
            this.txtGroup.Enabled = false;
            this.txtGroup.Location = new System.Drawing.Point(285, 54);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(200, 20);
            this.txtGroup.TabIndex = 4;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Location = new System.Drawing.Point(200, 57);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(70, 13);
            this.lblGroup.TabIndex = 3;
            this.lblGroup.Text = "Group Name:";
            // 
            // rbUpdateHostnames
            // 
            this.rbUpdateHostnames.AutoSize = true;
            this.rbUpdateHostnames.Location = new System.Drawing.Point(15, 95);
            this.rbUpdateHostnames.Name = "rbUpdateHostnames";
            this.rbUpdateHostnames.Size = new System.Drawing.Size(162, 17);
            this.rbUpdateHostnames.TabIndex = 2;
            this.rbUpdateHostnames.Text = "Update specific hostname(s):";
            this.rbUpdateHostnames.UseVisualStyleBackColor = true;
            // 
            // rbUpdateGroup
            // 
            this.rbUpdateGroup.AutoSize = true;
            this.rbUpdateGroup.Location = new System.Drawing.Point(15, 55);
            this.rbUpdateGroup.Name = "rbUpdateGroup";
            this.rbUpdateGroup.Size = new System.Drawing.Size(141, 17);
            this.rbUpdateGroup.TabIndex = 1;
            this.rbUpdateGroup.Text = "Update a specific group:";
            this.rbUpdateGroup.UseVisualStyleBackColor = true;
            // 
            // rbUpdateAll
            // 
            this.rbUpdateAll.AutoSize = true;
            this.rbUpdateAll.Checked = true;
            this.rbUpdateAll.Location = new System.Drawing.Point(15, 25);
            this.rbUpdateAll.Name = "rbUpdateAll";
            this.rbUpdateAll.Size = new System.Drawing.Size(180, 17);
            this.rbUpdateAll.TabIndex = 0;
            this.rbUpdateAll.TabStop = true;
            this.rbUpdateAll.Text = "Update all hostnames in account";
            this.rbUpdateAll.UseVisualStyleBackColor = true;
            // 
            // grpCredentials
            // 
            this.grpCredentials.Controls.Add(this.chkUsePasswordHash);
            this.grpCredentials.Controls.Add(this.chkShowPassword);
            this.grpCredentials.Controls.Add(this.txtPassword);
            this.grpCredentials.Controls.Add(this.lblPassword);
            this.grpCredentials.Controls.Add(this.txtUsername);
            this.grpCredentials.Controls.Add(this.lblUsername);
            this.grpCredentials.Location = new System.Drawing.Point(13, 13);
            this.grpCredentials.Name = "grpCredentials";
            this.grpCredentials.Size = new System.Drawing.Size(550, 105);
            this.grpCredentials.TabIndex = 0;
            this.grpCredentials.TabStop = false;
            this.grpCredentials.Text = "Dynu Credentials";
            // 
            // chkUsePasswordHash
            // 
            this.chkUsePasswordHash.AutoSize = true;
            this.chkUsePasswordHash.Checked = true;
            this.chkUsePasswordHash.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUsePasswordHash.Location = new System.Drawing.Point(400, 57);
            this.chkUsePasswordHash.Name = "chkUsePasswordHash";
            this.chkUsePasswordHash.Size = new System.Drawing.Size(116, 17);
            this.chkUsePasswordHash.TabIndex = 5;
            this.chkUsePasswordHash.Text = "Use SHA256 Hash";
            this.chkUsePasswordHash.UseVisualStyleBackColor = true;
            // 
            // chkShowPassword
            // 
            this.chkShowPassword.AutoSize = true;
            this.chkShowPassword.Location = new System.Drawing.Point(330, 57);
            this.chkShowPassword.Name = "chkShowPassword";
            this.chkShowPassword.Size = new System.Drawing.Size(53, 17);
            this.chkShowPassword.TabIndex = 4;
            this.chkShowPassword.Text = "Show";
            this.chkShowPassword.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(120, 55);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '●';
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(15, 58);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(94, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Update Password:";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(120, 25);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(200, 20);
            this.txtUsername.TabIndex = 1;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Location = new System.Drawing.Point(15, 28);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(58, 13);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username:";
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.btnOpenLogFile);
            this.tabLog.Controls.Add(this.btnClearLog);
            this.tabLog.Controls.Add(this.btnRefreshLog);
            this.tabLog.Controls.Add(this.txtLog);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(10);
            this.tabLog.Size = new System.Drawing.Size(576, 635);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // btnOpenLogFile
            // 
            this.btnOpenLogFile.Location = new System.Drawing.Point(233, 585);
            this.btnOpenLogFile.Name = "btnOpenLogFile";
            this.btnOpenLogFile.Size = new System.Drawing.Size(120, 35);
            this.btnOpenLogFile.TabIndex = 3;
            this.btnOpenLogFile.Text = "Open Log File";
            this.btnOpenLogFile.UseVisualStyleBackColor = true;
            // 
            // btnClearLog
            // 
            this.btnClearLog.Location = new System.Drawing.Point(123, 585);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(100, 35);
            this.btnClearLog.TabIndex = 2;
            this.btnClearLog.Text = "Clear Log";
            this.btnClearLog.UseVisualStyleBackColor = true;
            // 
            // btnRefreshLog
            // 
            this.btnRefreshLog.Location = new System.Drawing.Point(13, 585);
            this.btnRefreshLog.Name = "btnRefreshLog";
            this.btnRefreshLog.Size = new System.Drawing.Size(100, 35);
            this.btnRefreshLog.TabIndex = 1;
            this.btnRefreshLog.Text = "Refresh";
            this.btnRefreshLog.UseVisualStyleBackColor = true;
            // 
            // txtLog
            // 
            this.txtLog.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.Location = new System.Drawing.Point(13, 13);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(550, 560);
            this.txtLog.TabIndex = 0;
            this.txtLog.WordWrap = false;
            // 
            // tabAbout
            // 
            this.tabAbout.Controls.Add(this.lnkGitHub);
            this.tabAbout.Controls.Add(this.lnkDownload);
            this.tabAbout.Controls.Add(this.lblAboutTitle);
            this.tabAbout.Controls.Add(this.lblAboutDesc);
            this.tabAbout.Controls.Add(this.lblOpenSource);
            this.tabAbout.Controls.Add(this.lblLicense);
            this.tabAbout.Controls.Add(this.lblDisclaimer);
            this.tabAbout.Location = new System.Drawing.Point(4, 22);
            this.tabAbout.Name = "tabAbout";
            this.tabAbout.Padding = new System.Windows.Forms.Padding(20);
            this.tabAbout.Size = new System.Drawing.Size(576, 635);
            this.tabAbout.TabIndex = 2;
            this.tabAbout.Text = "About";
            this.tabAbout.UseVisualStyleBackColor = true;
            // 
            // lblAboutTitle
            // 
            this.lblAboutTitle.AutoSize = true;
            this.lblAboutTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAboutTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(150)))), ((int)(((byte)(243)))));
            this.lblAboutTitle.Location = new System.Drawing.Point(20, 20);
            this.lblAboutTitle.Name = "lblAboutTitle";
            this.lblAboutTitle.Size = new System.Drawing.Size(204, 32);
            this.lblAboutTitle.TabIndex = 0;
            this.lblAboutTitle.Text = "Dynu IP Updater";
            // 
            // lblAboutDesc
            // 
            this.lblAboutDesc.Location = new System.Drawing.Point(23, 65);
            this.lblAboutDesc.Name = "lblAboutDesc";
            this.lblAboutDesc.Size = new System.Drawing.Size(530, 145);
            this.lblAboutDesc.TabIndex = 1;
            this.lblAboutDesc.Text = "A Windows desktop application for automatically updating Dynu.com Dynamic DNS rec" +
    "ords with encrypted credential storage and scheduled task support.\r\n\r\n\r\nSource c" +
    "ode:\r\n\r\n\r\n\r\nDownload:";
            // 
            // lnkGitHub
            // 
            this.lnkGitHub.AutoSize = true;
            this.lnkGitHub.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkGitHub.Location = new System.Drawing.Point(23, 132);
            this.lnkGitHub.Name = "lnkGitHub";
            this.lnkGitHub.Size = new System.Drawing.Size(269, 17);
            this.lnkGitHub.TabIndex = 2;
            this.lnkGitHub.TabStop = true;
            this.lnkGitHub.Text = "https://github.com/adriancs2/DynuIpUpdater";
            // 
            // lnkDownload
            // 
            this.lnkDownload.AutoSize = true;
            this.lnkDownload.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkDownload.Location = new System.Drawing.Point(23, 184);
            this.lnkDownload.Name = "lnkDownload";
            this.lnkDownload.Size = new System.Drawing.Size(322, 17);
            this.lnkDownload.TabIndex = 3;
            this.lnkDownload.TabStop = true;
            this.lnkDownload.Text = "https://github.com/adriancs2/DynuIpUpdater/releases";
            // 
            // lblOpenSource
            // 
            this.lblOpenSource.AutoSize = true;
            this.lblOpenSource.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOpenSource.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblOpenSource.Location = new System.Drawing.Point(23, 249);
            this.lblOpenSource.Name = "lblOpenSource";
            this.lblOpenSource.Size = new System.Drawing.Size(97, 20);
            this.lblOpenSource.TabIndex = 4;
            this.lblOpenSource.Text = "Open Source";
            // 
            // lblLicense
            // 
            this.lblLicense.AutoSize = true;
            this.lblLicense.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLicense.Location = new System.Drawing.Point(23, 279);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(207, 17);
            this.lblLicense.TabIndex = 5;
            this.lblLicense.Text = "License: Unlicense (Public Domain)";
            // 
            // lblDisclaimer
            // 
            this.lblDisclaimer.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblDisclaimer.Location = new System.Drawing.Point(23, 339);
            this.lblDisclaimer.Name = "lblDisclaimer";
            this.lblDisclaimer.Size = new System.Drawing.Size(530, 50);
            this.lblDisclaimer.TabIndex = 6;
            this.lblDisclaimer.Text = "Disclaimer: This project is not affiliated with, endorsed by, or sponsored by Dyn" +
    "u Systems Inc. Dynu is a trademark of Dynu Systems Inc.";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 30000;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 661);
            this.Controls.Add(this.tabControl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dynu IP Updater";
            this.tabControl.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.grpActions.ResumeLayout(false);
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
            this.grpUpdateTarget.ResumeLayout(false);
            this.grpUpdateTarget.PerformLayout();
            this.grpCredentials.ResumeLayout(false);
            this.grpCredentials.PerformLayout();
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabAbout.ResumeLayout(false);
            this.tabAbout.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.TabPage tabAbout;

        // Credentials group
        private System.Windows.Forms.GroupBox grpCredentials;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkShowPassword;
        private System.Windows.Forms.CheckBox chkUsePasswordHash;

        // Update target group
        private System.Windows.Forms.GroupBox grpUpdateTarget;
        private System.Windows.Forms.RadioButton rbUpdateAll;
        private System.Windows.Forms.RadioButton rbUpdateGroup;
        private System.Windows.Forms.RadioButton rbUpdateHostnames;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.Label lblHostnames;
        private System.Windows.Forms.TextBox txtHostnames;
        private System.Windows.Forms.Label lblHostnamesHint;

        // Options group
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.NumericUpDown numInterval;
        private System.Windows.Forms.Label lblIntervalUnit;
        private System.Windows.Forms.CheckBox chkUpdateIpv4;
        private System.Windows.Forms.CheckBox chkUpdateIpv6;

        // Status group
        private System.Windows.Forms.GroupBox grpStatus;
        private System.Windows.Forms.Label lblCurrentIp;
        private System.Windows.Forms.TextBox txtCurrentIp;
        private System.Windows.Forms.Button btnCheckIp;
        private System.Windows.Forms.Label lblLastUpdate;
        private System.Windows.Forms.TextBox txtLastUpdate;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TextBox txtStatus;

        // Actions group
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.Button btnTestConnection;
        private System.Windows.Forms.Button btnUpdateNow;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Button btnInstallService;
        private System.Windows.Forms.Button btnRemoveService;

        // Log tab
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnRefreshLog;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Button btnOpenLogFile;

        // About tab
        private System.Windows.Forms.Label lblAboutTitle;
        private System.Windows.Forms.Label lblAboutDesc;
        private System.Windows.Forms.LinkLabel lnkGitHub;
        private System.Windows.Forms.LinkLabel lnkDownload;
        private System.Windows.Forms.Label lblOpenSource;
        private System.Windows.Forms.Label lblLicense;
        private System.Windows.Forms.Label lblDisclaimer;

        // Timer
        private System.Windows.Forms.Timer refreshTimer;
    }
}
