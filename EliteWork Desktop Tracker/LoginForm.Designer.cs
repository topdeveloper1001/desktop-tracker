namespace EliteWork_Desktop_Tracker
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this._titleLabel = new System.Windows.Forms.Label();
            this._support_lb = new System.Windows.Forms.Label();
            this._close_btn = new System.Windows.Forms.Button();
            this._down_btn = new System.Windows.Forms.Button();
            this._full_btn = new System.Windows.Forms.Button();
            this._login_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this._username_Tb = new System.Windows.Forms.TextBox();
            this._password_Tb = new System.Windows.Forms.TextBox();
            this._version_btn = new System.Windows.Forms.Button();
            this._log_lf_btn = new System.Windows.Forms.Button();
            this._header_pn = new System.Windows.Forms.Panel();
            this._new_version_pn = new System.Windows.Forms.Panel();
            this._local_time_lb = new System.Windows.Forms.Label();
            this._installed_version_lb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._goback_btn = new System.Windows.Forms.Button();
            this._new_version_lb = new System.Windows.Forms.Label();
            this._download_now_btn = new System.Windows.Forms.Button();
            this._activity_log_pnl = new System.Windows.Forms.Panel();
            this._activity_log_tb = new System.Windows.Forms.TextBox();
            this._local_time_al_lb = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this._goback_log_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this._username_pb = new System.Windows.Forms.PictureBox();
            this._pass_pb = new System.Windows.Forms.PictureBox();
            this._to_tray_btn = new System.Windows.Forms.Button();
            this._balloon_ni = new System.Windows.Forms.NotifyIcon(this.components);
            this._current_timer_tm = new System.Windows.Forms.Timer(this.components);
            this._new_version_pn.SuspendLayout();
            this._activity_log_pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._username_pb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._pass_pb)).BeginInit();
            this.SuspendLayout();
            // 
            // _titleLabel
            // 
            this._titleLabel.AutoSize = true;
            this._titleLabel.BackColor = System.Drawing.Color.Transparent;
            this._titleLabel.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._titleLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
            this._titleLabel.Location = new System.Drawing.Point(45, 10);
            this._titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._titleLabel.Name = "_titleLabel";
            this._titleLabel.Size = new System.Drawing.Size(271, 29);
            this._titleLabel.TabIndex = 0;
            this._titleLabel.Text = "EliteWork Desktop Tracker";
            this._titleLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this._titleLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            // 
            // _support_lb
            // 
            this._support_lb.AutoSize = true;
            this._support_lb.BackColor = System.Drawing.Color.Transparent;
            this._support_lb.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._support_lb.ForeColor = System.Drawing.Color.WhiteSmoke;
            this._support_lb.Location = new System.Drawing.Point(151, 565);
            this._support_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._support_lb.Name = "_support_lb";
            this._support_lb.Size = new System.Drawing.Size(0, 26);
            this._support_lb.TabIndex = 1;
            // 
            // _close_btn
            // 
            this._close_btn.BackColor = System.Drawing.Color.Transparent;
            this._close_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_close_btn.BackgroundImage")));
            this._close_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._close_btn.FlatAppearance.BorderSize = 0;
            this._close_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._close_btn.Location = new System.Drawing.Point(710, 10);
            this._close_btn.Margin = new System.Windows.Forms.Padding(4);
            this._close_btn.Name = "_close_btn";
            this._close_btn.Size = new System.Drawing.Size(30, 30);
            this._close_btn.TabIndex = 23;
            this._close_btn.UseVisualStyleBackColor = false;
            this._close_btn.Click += new System.EventHandler(this._close_btn_Click);
            // 
            // _down_btn
            // 
            this._down_btn.BackColor = System.Drawing.Color.Transparent;
            this._down_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_down_btn.BackgroundImage")));
            this._down_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._down_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this._down_btn.FlatAppearance.BorderSize = 0;
            this._down_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._down_btn.Location = new System.Drawing.Point(635, 10);
            this._down_btn.Margin = new System.Windows.Forms.Padding(4);
            this._down_btn.Name = "_down_btn";
            this._down_btn.Size = new System.Drawing.Size(30, 30);
            this._down_btn.TabIndex = 2;
            this._down_btn.UseVisualStyleBackColor = true;
            this._down_btn.Click += new System.EventHandler(this._down_btn_Click);
            // 
            // _full_btn
            // 
            this._full_btn.BackColor = System.Drawing.Color.Transparent;
            this._full_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_full_btn.BackgroundImage")));
            this._full_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._full_btn.FlatAppearance.BorderSize = 0;
            this._full_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._full_btn.Location = new System.Drawing.Point(672, 10);
            this._full_btn.Margin = new System.Windows.Forms.Padding(4);
            this._full_btn.Name = "_full_btn";
            this._full_btn.Size = new System.Drawing.Size(30, 30);
            this._full_btn.TabIndex = 21;
            this._full_btn.UseVisualStyleBackColor = false;
            // 
            // _login_btn
            // 
            this._login_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_login_btn.BackgroundImage")));
            this._login_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._login_btn.FlatAppearance.BorderSize = 0;
            this._login_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._login_btn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._login_btn.Location = new System.Drawing.Point(344, 342);
            this._login_btn.Margin = new System.Windows.Forms.Padding(0);
            this._login_btn.Name = "_login_btn";
            this._login_btn.Padding = new System.Windows.Forms.Padding(4);
            this._login_btn.Size = new System.Drawing.Size(162, 50);
            this._login_btn.TabIndex = 0;
            this._login_btn.TabStop = false;
            this._login_btn.Text = "LOGIN";
            this._login_btn.UseVisualStyleBackColor = false;
            this._login_btn.Click += new System.EventHandler(this._login_btn_Click);
            this._login_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._login_btn.MouseEnter += new System.EventHandler(this.ControlMouseEnter);
            this._login_btn.MouseLeave += new System.EventHandler(this.ControlMouseLeave);
            this._login_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(106, 86);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(320, 33);
            this.label3.TabIndex = 25;
            this.label3.Text = "Please login to your account";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(108, 164);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 26);
            this.label4.TabIndex = 26;
            this.label4.Text = "Username:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(108, 249);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(99, 26);
            this.label5.TabIndex = 27;
            this.label5.Text = "Password:";
            // 
            // _username_Tb
            // 
            this._username_Tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._username_Tb.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._username_Tb.Location = new System.Drawing.Point(124, 209);
            this._username_Tb.Margin = new System.Windows.Forms.Padding(4);
            this._username_Tb.Name = "_username_Tb";
            this._username_Tb.Size = new System.Drawing.Size(372, 24);
            this._username_Tb.TabIndex = 28;
            // 
            // _password_Tb
            // 
            this._password_Tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._password_Tb.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._password_Tb.Location = new System.Drawing.Point(124, 294);
            this._password_Tb.Margin = new System.Windows.Forms.Padding(4);
            this._password_Tb.Name = "_password_Tb";
            this._password_Tb.Size = new System.Drawing.Size(372, 24);
            this._password_Tb.TabIndex = 29;
            this._password_Tb.UseSystemPasswordChar = true;
            // 
            // _version_btn
            // 
            this._version_btn.BackgroundImage = global::EliteWork_Desktop_Tracker.Properties.Resources.version_button;
            this._version_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._version_btn.FlatAppearance.BorderSize = 0;
            this._version_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._version_btn.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._version_btn.Location = new System.Drawing.Point(19, 488);
            this._version_btn.Margin = new System.Windows.Forms.Padding(0);
            this._version_btn.Name = "_version_btn";
            this._version_btn.Padding = new System.Windows.Forms.Padding(4);
            this._version_btn.Size = new System.Drawing.Size(338, 44);
            this._version_btn.TabIndex = 30;
            this._version_btn.Text = "Checking for new version...";
            this._version_btn.UseVisualStyleBackColor = false;
            this._version_btn.Click += new System.EventHandler(this._version_btn_Click);
            this._version_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._version_btn.MouseEnter += new System.EventHandler(this._version_btn_MouseEnter);
            this._version_btn.MouseLeave += new System.EventHandler(this._version_btn_MouseLeave);
            this._version_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // _log_lf_btn
            // 
            this._log_lf_btn.BackgroundImage = global::EliteWork_Desktop_Tracker.Properties.Resources.log_button_1;
            this._log_lf_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._log_lf_btn.FlatAppearance.BorderSize = 0;
            this._log_lf_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._log_lf_btn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._log_lf_btn.ForeColor = System.Drawing.Color.White;
            this._log_lf_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._log_lf_btn.Location = new System.Drawing.Point(480, 488);
            this._log_lf_btn.Margin = new System.Windows.Forms.Padding(0);
            this._log_lf_btn.Name = "_log_lf_btn";
            this._log_lf_btn.Padding = new System.Windows.Forms.Padding(4);
            this._log_lf_btn.Size = new System.Drawing.Size(250, 44);
            this._log_lf_btn.TabIndex = 31;
            this._log_lf_btn.Text = "Activity Log";
            this._log_lf_btn.UseVisualStyleBackColor = false;
            this._log_lf_btn.Click += new System.EventHandler(this._log_btn_Click);
            this._log_lf_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._log_lf_btn.MouseEnter += new System.EventHandler(this.ControlMouseEnter);
            this._log_lf_btn.MouseLeave += new System.EventHandler(this.ControlMouseLeave);
            this._log_lf_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // _header_pn
            // 
            this._header_pn.BackColor = System.Drawing.Color.Transparent;
            this._header_pn.Location = new System.Drawing.Point(0, 0);
            this._header_pn.Margin = new System.Windows.Forms.Padding(4);
            this._header_pn.Name = "_header_pn";
            this._header_pn.Size = new System.Drawing.Size(750, 50);
            this._header_pn.TabIndex = 22;
            this._header_pn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this._header_pn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            // 
            // _new_version_pn
            // 
            this._new_version_pn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_new_version_pn.BackgroundImage")));
            this._new_version_pn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._new_version_pn.Controls.Add(this._local_time_lb);
            this._new_version_pn.Controls.Add(this._installed_version_lb);
            this._new_version_pn.Controls.Add(this.label1);
            this._new_version_pn.Controls.Add(this._goback_btn);
            this._new_version_pn.Controls.Add(this._new_version_lb);
            this._new_version_pn.Controls.Add(this._download_now_btn);
            this._new_version_pn.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._new_version_pn.Location = new System.Drawing.Point(5, 50);
            this._new_version_pn.Margin = new System.Windows.Forms.Padding(4);
            this._new_version_pn.Name = "_new_version_pn";
            this._new_version_pn.Size = new System.Drawing.Size(740, 486);
            this._new_version_pn.TabIndex = 45;
            // 
            // _local_time_lb
            // 
            this._local_time_lb.AutoSize = true;
            this._local_time_lb.BackColor = System.Drawing.Color.Transparent;
            this._local_time_lb.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._local_time_lb.Location = new System.Drawing.Point(450, 450);
            this._local_time_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._local_time_lb.Name = "_local_time_lb";
            this._local_time_lb.Size = new System.Drawing.Size(223, 29);
            this._local_time_lb.TabIndex = 36;
            this._local_time_lb.Text = "Local Time: 01:00 AM\r\n";
            this._local_time_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _installed_version_lb
            // 
            this._installed_version_lb.AutoSize = true;
            this._installed_version_lb.BackColor = System.Drawing.Color.Transparent;
            this._installed_version_lb.Font = new System.Drawing.Font("Calibri", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._installed_version_lb.Location = new System.Drawing.Point(15, 60);
            this._installed_version_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._installed_version_lb.Name = "_installed_version_lb";
            this._installed_version_lb.Size = new System.Drawing.Size(195, 26);
            this._installed_version_lb.TabIndex = 35;
            this._installed_version_lb.Text = "Installed Version 3.11";
            this._installed_version_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(316, 36);
            this.label1.TabIndex = 34;
            this.label1.Text = "Check for Latest Version…";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _goback_btn
            // 
            this._goback_btn.BackgroundImage = global::EliteWork_Desktop_Tracker.Properties.Resources.settings;
            this._goback_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._goback_btn.FlatAppearance.BorderSize = 0;
            this._goback_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._goback_btn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._goback_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._goback_btn.Location = new System.Drawing.Point(20, 438);
            this._goback_btn.Margin = new System.Windows.Forms.Padding(0);
            this._goback_btn.Name = "_goback_btn";
            this._goback_btn.Padding = new System.Windows.Forms.Padding(4);
            this._goback_btn.Size = new System.Drawing.Size(162, 44);
            this._goback_btn.TabIndex = 33;
            this._goback_btn.Text = "Go back!";
            this._goback_btn.UseVisualStyleBackColor = false;
            this._goback_btn.Click += new System.EventHandler(this._goback_btn_Click);
            this._goback_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._goback_btn.MouseEnter += new System.EventHandler(this.ControlMouseEnter);
            this._goback_btn.MouseLeave += new System.EventHandler(this.ControlMouseLeave);
            this._goback_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // _new_version_lb
            // 
            this._new_version_lb.AutoSize = true;
            this._new_version_lb.BackColor = System.Drawing.Color.Transparent;
            this._new_version_lb.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._new_version_lb.Location = new System.Drawing.Point(106, 184);
            this._new_version_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._new_version_lb.Name = "_new_version_lb";
            this._new_version_lb.Size = new System.Drawing.Size(169, 116);
            this._new_version_lb.TabIndex = 26;
            this._new_version_lb.Text = "New version \r\n3.12 found, \r\nplease upgrade \r\nto stay in sync!\r\n";
            this._new_version_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _download_now_btn
            // 
            this._download_now_btn.BackColor = System.Drawing.Color.Transparent;
            this._download_now_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_download_now_btn.BackgroundImage")));
            this._download_now_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._download_now_btn.FlatAppearance.BorderSize = 0;
            this._download_now_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._download_now_btn.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._download_now_btn.ForeColor = System.Drawing.Color.White;
            this._download_now_btn.Location = new System.Drawing.Point(342, 202);
            this._download_now_btn.Margin = new System.Windows.Forms.Padding(0);
            this._download_now_btn.Name = "_download_now_btn";
            this._download_now_btn.Padding = new System.Windows.Forms.Padding(4);
            this._download_now_btn.Size = new System.Drawing.Size(335, 115);
            this._download_now_btn.TabIndex = 25;
            this._download_now_btn.Text = "DOWNLOAD NOW";
            this._download_now_btn.UseVisualStyleBackColor = false;
            this._download_now_btn.Click += new System.EventHandler(this._download_now_btn_Click);
            this._download_now_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._download_now_btn.MouseEnter += new System.EventHandler(this.ControlMouseEnter);
            this._download_now_btn.MouseLeave += new System.EventHandler(this.ControlMouseLeave);
            this._download_now_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // _activity_log_pnl
            // 
            this._activity_log_pnl.AutoSize = true;
            this._activity_log_pnl.Controls.Add(this._activity_log_tb);
            this._activity_log_pnl.Controls.Add(this._local_time_al_lb);
            this._activity_log_pnl.Controls.Add(this.label17);
            this._activity_log_pnl.Controls.Add(this._goback_log_btn);
            this._activity_log_pnl.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._activity_log_pnl.Location = new System.Drawing.Point(5, 50);
            this._activity_log_pnl.Margin = new System.Windows.Forms.Padding(4);
            this._activity_log_pnl.Name = "_activity_log_pnl";
            this._activity_log_pnl.Size = new System.Drawing.Size(740, 486);
            this._activity_log_pnl.TabIndex = 47;
            // 
            // _activity_log_tb
            // 
            this._activity_log_tb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._activity_log_tb.Location = new System.Drawing.Point(31, 75);
            this._activity_log_tb.Margin = new System.Windows.Forms.Padding(4);
            this._activity_log_tb.Multiline = true;
            this._activity_log_tb.Name = "_activity_log_tb";
            this._activity_log_tb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._activity_log_tb.Size = new System.Drawing.Size(684, 338);
            this._activity_log_tb.TabIndex = 38;
            // 
            // _local_time_al_lb
            // 
            this._local_time_al_lb.AutoSize = true;
            this._local_time_al_lb.BackColor = System.Drawing.Color.Transparent;
            this._local_time_al_lb.Font = new System.Drawing.Font("Calibri", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._local_time_al_lb.Location = new System.Drawing.Point(450, 450);
            this._local_time_al_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._local_time_al_lb.Name = "_local_time_al_lb";
            this._local_time_al_lb.Size = new System.Drawing.Size(223, 29);
            this._local_time_al_lb.TabIndex = 37;
            this._local_time_al_lb.Text = "Local Time: 01:00 AM\r\n";
            this._local_time_al_lb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.Color.Transparent;
            this.label17.Font = new System.Drawing.Font("Calibri", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label17.Location = new System.Drawing.Point(12, 12);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(241, 36);
            this.label17.TabIndex = 34;
            this.label17.Text = "Tracker Activity Log";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _goback_log_btn
            // 
            this._goback_log_btn.BackgroundImage = global::EliteWork_Desktop_Tracker.Properties.Resources.settings;
            this._goback_log_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._goback_log_btn.FlatAppearance.BorderSize = 0;
            this._goback_log_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._goback_log_btn.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this._goback_log_btn.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._goback_log_btn.Location = new System.Drawing.Point(20, 438);
            this._goback_log_btn.Margin = new System.Windows.Forms.Padding(0);
            this._goback_log_btn.Name = "_goback_log_btn";
            this._goback_log_btn.Padding = new System.Windows.Forms.Padding(4);
            this._goback_log_btn.Size = new System.Drawing.Size(162, 44);
            this._goback_log_btn.TabIndex = 33;
            this._goback_log_btn.Text = "Go back!";
            this._goback_log_btn.UseVisualStyleBackColor = false;
            this._goback_log_btn.Click += new System.EventHandler(this._goback_log_btn_Click);
            this._goback_log_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ControlMouseDown);
            this._goback_log_btn.MouseEnter += new System.EventHandler(this.ControlMouseEnter);
            this._goback_log_btn.MouseLeave += new System.EventHandler(this.ControlMouseLeave);
            this._goback_log_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ControlMouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(58, 84);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // _username_pb
            // 
            this._username_pb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_username_pb.BackgroundImage")));
            this._username_pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._username_pb.Location = new System.Drawing.Point(114, 199);
            this._username_pb.Margin = new System.Windows.Forms.Padding(4);
            this._username_pb.Name = "_username_pb";
            this._username_pb.Size = new System.Drawing.Size(392, 45);
            this._username_pb.TabIndex = 33;
            this._username_pb.TabStop = false;
            // 
            // _pass_pb
            // 
            this._pass_pb.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_pass_pb.BackgroundImage")));
            this._pass_pb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._pass_pb.Location = new System.Drawing.Point(114, 284);
            this._pass_pb.Margin = new System.Windows.Forms.Padding(4);
            this._pass_pb.Name = "_pass_pb";
            this._pass_pb.Size = new System.Drawing.Size(392, 45);
            this._pass_pb.TabIndex = 34;
            this._pass_pb.TabStop = false;
            // 
            // _to_tray_btn
            // 
            this._to_tray_btn.BackColor = System.Drawing.Color.Transparent;
            this._to_tray_btn.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("_to_tray_btn.BackgroundImage")));
            this._to_tray_btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this._to_tray_btn.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this._to_tray_btn.FlatAppearance.BorderSize = 0;
            this._to_tray_btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._to_tray_btn.Location = new System.Drawing.Point(598, 10);
            this._to_tray_btn.Margin = new System.Windows.Forms.Padding(4);
            this._to_tray_btn.Name = "_to_tray_btn";
            this._to_tray_btn.Size = new System.Drawing.Size(30, 30);
            this._to_tray_btn.TabIndex = 48;
            this._to_tray_btn.UseVisualStyleBackColor = true;
            this._to_tray_btn.Click += new System.EventHandler(this._to_tray_btn_Click);
            // 
            // _balloon_ni
            // 
            this._balloon_ni.Icon = ((System.Drawing.Icon)(resources.GetObject("_balloon_ni.Icon")));
            this._balloon_ni.Text = "EliteWork Desktop Tracker\r\n";
            this._balloon_ni.Visible = true;
            this._balloon_ni.Click += new System.EventHandler(this._balloon_ni_Click);
            // 
            // _current_timer_tm
            // 
            this._current_timer_tm.Interval = 1000;
            this._current_timer_tm.Tick += new System.EventHandler(this._current_timer_tm_Tick);
            // 
            // LoginForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(755, 602);
            this.Controls.Add(this._to_tray_btn);
            this.Controls.Add(this._activity_log_pnl);
            this.Controls.Add(this._new_version_pn);
            this.Controls.Add(this._pass_pb);
            this.Controls.Add(this._username_pb);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this._header_pn);
            this.Controls.Add(this._log_lf_btn);
            this.Controls.Add(this._version_btn);
            this.Controls.Add(this._password_Tb);
            this.Controls.Add(this._username_Tb);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._login_btn);
            this.Controls.Add(this._close_btn);
            this.Controls.Add(this._down_btn);
            this.Controls.Add(this._full_btn);
            this.Controls.Add(this._support_lb);
            this.Controls.Add(this._titleLabel);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EliteWork Desktop Tracker";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this._new_version_pn.ResumeLayout(false);
            this._new_version_pn.PerformLayout();
            this._activity_log_pnl.ResumeLayout(false);
            this._activity_log_pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._username_pb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._pass_pb)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _titleLabel;
        private System.Windows.Forms.Label _support_lb;
        private System.Windows.Forms.Button _close_btn;
        private System.Windows.Forms.Button _down_btn;
        private System.Windows.Forms.Button _full_btn;
        private System.Windows.Forms.Button _login_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox _username_Tb;
        private System.Windows.Forms.TextBox _password_Tb;
        private System.Windows.Forms.Button _version_btn;
        private System.Windows.Forms.Button _log_lf_btn;
        private System.Windows.Forms.Panel _header_pn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox _username_pb;
        private System.Windows.Forms.PictureBox _pass_pb;
        private System.Windows.Forms.Panel _new_version_pn;
        private System.Windows.Forms.Label _installed_version_lb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button _goback_btn;
        private System.Windows.Forms.Label _new_version_lb;
        private System.Windows.Forms.Button _download_now_btn;
        private System.Windows.Forms.Label _local_time_lb;
        private System.Windows.Forms.Panel _activity_log_pnl;
        private System.Windows.Forms.TextBox _activity_log_tb;
        private System.Windows.Forms.Label _local_time_al_lb;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Button _goback_log_btn;
        private System.Windows.Forms.Button _to_tray_btn;
        private System.Windows.Forms.NotifyIcon _balloon_ni;
        private System.Windows.Forms.Timer _current_timer_tm;
    }
}

