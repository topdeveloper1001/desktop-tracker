using Common;
using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Controllers;
using EliteWork_Desktop_Tracker.Helpers;
using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Properties;
using EWLocalCache;
using EWWebProcessor;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.ApplicationServices;
using RegistryLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker
{
    public partial class LoginForm : Form, ILogHandler/*, INotifyHandler*/
    {
        private static int[] RGB_TRANS_MASK = { 230, 240, 250 };
        private Point lastPoint = Point.Empty;
        private bool IsHide = false;
        private bool IsInternetExists = true;
        private int InternetConnectionCount = 0;
        //private ActualDataForm ActualDataForm = null;

        public LoginForm()
        {
            InitializeComponent();

            /////////////////
            //ActualDataForm = new ActualDataForm();
            //ActualDataForm.Visible = false;
            //int x = Screen.PrimaryScreen.WorkingArea.Width - ActualDataForm.Width;
            //int y = Screen.PrimaryScreen.WorkingArea.Height - ActualDataForm.Height;
            //ActualDataForm.Location = new Point(x, y);
            ///////////////////

            System.Net.ServicePointManager.Expect100Continue = false;
            //PrepareVersionPanel();
            _new_version_pn.Visible = false;
            //GetSavedEmailAndDetectVersion();
            SetEmailFromRegistry();
            SetPasswordFromRegistry(); // AHMED EDIT
            _header_pn.SendToBack();
            _username_pb.SendToBack();
            _pass_pb.SendToBack();
            label4.SendToBack();
            label5.SendToBack();
            _activity_log_pnl.Visible = false;

            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;
            this.TransparencyKey = Color.FromArgb(RGB_TRANS_MASK[0], RGB_TRANS_MASK[1],
                RGB_TRANS_MASK[2]);

            _version_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _version_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _login_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _login_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _log_lf_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _log_lf_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _down_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _down_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _full_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _full_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _close_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _close_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _download_now_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _download_now_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_log_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_log_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _to_tray_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _to_tray_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            //LogController.GetInstance().SetLogHandler(this);
            //SetVersionButton();
            //ConfVersionPanel();
        }

        private void PrepareVersionPanel()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                //this.Invoke((MethodInvoker)delegate ()
                //{
                    GetSavedEmailAndDetectVersion();
                    SetVersionButton();
                    ConfVersionPanel();
               // });
            }));
        }

        private void ConfVersionPanel()
        {
            try
            {
                _new_version_lb.Text =
                            string.Format("New version {0} {1:0.00} found, {0} please upgrade {0} to stay in sync!",
                            Environment.NewLine, CurrentContext.GetInstance().VersionData.NewestVersion);

                _installed_version_lb.Text = string.Format("Installed Version {0:0.00}",
                        CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            catch { }
        }

        private void SetEmailFromRegistry()
        {
            string email = string.Empty;
            RegistryProcessor.GetFromRegistry(CommonConst.REGISTRY_PATH,
                CommonConst.EMAIL_VALUE_NAME, ref email, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
            if (!string.IsNullOrEmpty(email))
                _username_Tb.Text = email;
        }

        // AHMED EDIT
        private void SetPasswordFromRegistry()
        {
            string pwd = string.Empty;
            RegistryProcessor.GetFromRegistry(CommonConst.REGISTRY_PATH,
                CommonConst.PWD_VALUE_NAME, ref pwd, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
            if (!string.IsNullOrEmpty(pwd))
            {
                pwd = StringCipher.Decrypt(pwd, CommonConst.DES_KEY);
                if (!string.IsNullOrEmpty(pwd))
                    _password_Tb.Text = pwd;
            }
        }
        // AHMED EDIT

        private void GetSavedEmailAndDetectVersion()
        {
            try
            {
                VersionController.GetInstance();
                string email = string.Empty;
                RegistryProcessor.GetFromRegistry(CommonConst.REGISTRY_PATH,
                    CommonConst.EMAIL_VALUE_NAME, ref email, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                if (!WebProcessor.CheckInternetConnection())
                {
                    LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Connection Error"));
                    MessageBox.Show("Connection Error");
                    CurrentContext.GetInstance().VersionData.State = VersionState.UNKNOWN;
                    IsInternetExists = false;
                    return;
                }
                else
                    IsInternetExists = true;

                if (!string.IsNullOrEmpty(email))
                {
                    CurrentContext.GetInstance().VersionData.VersionDetected =
                                            VersionController.GetInstance().GetVersionData(email);
                }
                else
                    CurrentContext.GetInstance().VersionData.VersionDetected =
                                            VersionController.GetInstance().GetVersionData(string.Empty);

                if (CurrentContext.GetInstance().VersionData.VersionDetected)
                {
                    _new_version_lb.TextAlign = ContentAlignment.MiddleCenter;
                    DetectVersionState();
                }
                else
                    CurrentContext.GetInstance().VersionData.State = VersionState.UNKNOWN;
            }
            catch
            { CurrentContext.GetInstance().VersionData.State = VersionState.UNKNOWN; }
        }

        private void DetectVersionState()
        {
            if (CurrentContext.GetInstance().VersionData.VersionDetected)
            {
                if (CurrentContext.GetInstance().VersionData.CurrentVersion >=
                    CurrentContext.GetInstance().VersionData.NewestVersion)
                    CurrentContext.GetInstance().VersionData.State = VersionState.LATEST;
                else
                {
                    if (CurrentContext.GetInstance().VersionData.MandatoryUpdate == 0)
                        CurrentContext.GetInstance().VersionData.State = VersionState.UPDATE_AVAILABLE;
                    else
                        CurrentContext.GetInstance().VersionData.State = VersionState.UPDATE_REQUIRED;
                }
            }
        }

        private void SetVersionButton()
        {
            if (CurrentContext.GetInstance().VersionData.State == VersionState.LATEST)
            {
                //_version_btn = null;
                _version_btn.BackgroundImage = Resources.version_button;
                this.Invoke((MethodInvoker)delegate ()
                {
                    _version_btn.Enabled = false;
                });
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Latest]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_AVAILABLE)
            {
                _version_btn.BackgroundImage = Resources.version_button_warn;
                this.Invoke((MethodInvoker)delegate ()
                {
                    _version_btn.Enabled = true;
                });
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Update Available]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_REQUIRED)
            {
                _version_btn.BackgroundImage = Resources.version_button_alarm;
                this.Invoke((MethodInvoker)delegate ()
                {
                    _version_btn.Enabled = true;
                });
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Update Required]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    _version_btn.BackgroundImage = Resources.version_button;
                    _version_btn.Enabled = false;
                    CurrentContext.GetInstance().VersionData.ButtonText =
                        string.Format("Checking for new version...", CurrentContext.GetInstance().VersionData.CurrentVersion);
                });
                /*string.Format("Ver. {0:0.00} Installed [Unknown]", CurrentContext.GetInstance().VersionData.CurrentVersion);*/
            }
            this.Invoke((MethodInvoker)delegate ()
            {
                _version_btn.Text = CurrentContext.GetInstance().VersionData.ButtonText;
            });
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        
        private void SetVersionButtonImage(Button button, bool mouseEnter)
        {
            if (CurrentContext.GetInstance().VersionData.State == VersionState.LATEST)
            {
                if (mouseEnter)
                    button.BackgroundImage = Resources.version_button_over;
                else
                    button.BackgroundImage = Resources.version_button;
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_AVAILABLE)
            {
                if (mouseEnter)
                    button.BackgroundImage = Resources.version_button_warn_over;
                else
                    button.BackgroundImage = Resources.version_button_warn;
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_REQUIRED)
            {
                if (mouseEnter)
                    button.BackgroundImage = Resources.version_button_alarm_over;
                else
                    button.BackgroundImage = Resources.version_button_alarm;
            }
            else
            {
                if (mouseEnter)
                    button.BackgroundImage = Resources.version_button_over;
                else
                    button.BackgroundImage = Resources.version_button;
            }
        }

        private void _version_btn_MouseEnter(object sender, EventArgs e)
        {
            SetVersionButtonImage(_version_btn, true);
            _version_btn.Refresh();
        }

        private void _version_btn_MouseLeave(object sender, EventArgs e)
        {
            SetVersionButtonImage(_version_btn, false);
            _version_btn.Refresh();
        }

        private void _close_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void _down_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Minimize\" button"));
            this.WindowState = FormWindowState.Minimized;
        }

        private void _login_btn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_username_Tb.Text) || string.IsNullOrEmpty(_password_Tb.Text))
            {
                LogController.GetInstance().LogData(LogController.
                    GetInstance().LogFormat.GetLoginFailedLine("Incorrect username or password"));
                MessageBox.Show("Login or password is empty");
            }
            else
            {
                if (!CurrentContext.GetInstance().IsSessionDataLocked)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
                    {
                        CurrentContext.GetInstance().IsSessionDataLocked = true;
                        if (!WebProcessor.CheckInternetConnection())
                        {
                            LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetLoginFailedLine("Network unavailable"));
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                MessageBox.Show("Network unavailable");
                            });
                            CurrentContext.GetInstance().IsSessionDataLocked = false;

                            return;
                        }
                        LoginState loginState = LoginDataController.GetInstance().Login(_username_Tb.Text, _password_Tb.Text);
                        if (loginState == LoginState.LOGGED)
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetLoginSuccessLine());
                                RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                    CommonConst.EMAIL_VALUE_NAME, CurrentContext.GetInstance().LoginData.Login,
                                    RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);

                                // AHMED EDITS
                                string pwd = CurrentContext.GetInstance().LoginData.Password;
                                pwd = StringCipher.Encrypt(pwd, CommonConst.DES_KEY);
                                if (!string.IsNullOrEmpty(pwd))
                                {
                                    RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                        CommonConst.PWD_VALUE_NAME, pwd, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                                }
                                // AHMED EDITS

                                MainForm mainForm = new MainForm();
                                mainForm.Show();
                                mainForm.Visible = false;
                                mainForm.Left = this.Left;
                                mainForm.Top = this.Top;
                                mainForm.Size = this.Size;
                                mainForm.Visible = true;
                                IsHide = true;
                                _balloon_ni.Visible = false;
                                this.Hide();
                                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetNavigationLine("Main page"));
                                LogController.GetInstance().RemoveLogHandler(this);
                            });
                        }
                        else 
                        {
                            this.Invoke((MethodInvoker)delegate ()
                            {
                                if (loginState == LoginState.CONNECTION_FAIL)
                                {
                                    LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetLoginFailedLine("Web server did not responded"));
                                    MessageBox.Show("Connection Error");
                                }
                                else if (loginState == LoginState.LOGIN_FAIL)
                                {
                                    LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetLoginFailedLine("Incorrect username or password"));
                                    MessageBox.Show("Login Error");
                                }
                            });
                        }
                        CurrentContext.GetInstance().IsSessionDataLocked = false;
                    }));
                }
            }
        }
        
        private void _version_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("New Version Page"));
            _local_time_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _new_version_pn.Visible = true;
        }

        private void _goback_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _new_version_pn.Visible = false;
        }

        private void _log_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Activity Log Page"));
            _local_time_al_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _activity_log_pnl.Visible = true;
        }

        private void _goback_log_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _activity_log_pnl.Visible = false;
        }

        public void AppendLine(string line)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                _activity_log_tb.AppendText(string.Format("{0}{1}", line, Environment.NewLine));
            });
        }

        private void ControlMouseEnter(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton.BackgroundImage = FormHelper.GetButtonOverImage(currentButton);
            currentButton.Refresh();
        }

        private void ControlMouseLeave(object sender, EventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton.BackgroundImage = FormHelper.GetButtonImage(currentButton);
            currentButton.Refresh();
        }

        private void ControlMouseDown(object sender, MouseEventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton.Location = new Point(currentButton.Location.X, currentButton.Location.Y + 1);
            currentButton.Refresh();
        }

        private void ControlMouseUp(object sender, MouseEventArgs e)
        {
            Button currentButton = sender as Button;
            currentButton.Location = new Point(currentButton.Location.X, currentButton.Location.Y - 1);
            currentButton.Refresh();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            /*ActualDataForm = new ActualDataForm();
            ActualDataForm.Visible = false;
            int x = Screen.PrimaryScreen.WorkingArea.Width - ActualDataForm.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - ActualDataForm.Height;
            ActualDataForm.Location = new Point(x, y);
            ActualDataForm.StartPosition = FormStartPosition.Manual;
            NotifycationHelper.GetInstance().SetNotifyForm(ActualDataForm);
            NotifycationHelper.GetInstance().SetNotifyHandler(this);*/

            LogController.GetInstance().SetLogHandler(this);
            PrepareVersionPanel();
            _current_timer_tm.Start();
        }
        
        private void _to_tray_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Minimize To Tray"));
            MinimizeToSystemTray();
        }

        private void MinimizeToSystemTray()
        {
            _balloon_ni.Visible = true;
            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void _balloon_ni_Click(object sender, EventArgs e)
        {
            if (!IsHide)
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Maximize From Tray"));
                Show();
                this.WindowState = FormWindowState.Normal;
                _balloon_ni.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        private void _download_now_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Dowmload Now\" button"));
            Process.Start("http://www.elitework.com/tracker/EliteWorkDesktopTracker.zip");
        }

        protected override CreateParams CreateParams
        {
            get
            {
                // Activate double buffering at the form level.  All child controls will be double buffered as well.

                CreateParams cp = base.CreateParams;

                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                cp.Style |= 0x20000; // AHMED_EDIT
                cp.ClassStyle |= 0x8; // AHMED EDIT

                return cp;
            }
        }

        private void _current_timer_tm_Tick(object sender, EventArgs e)
        {
            _local_time_al_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _local_time_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);

            if (!IsInternetExists)
            {
                InternetConnectionCount++;
                if (InternetConnectionCount >= CommonConst.CHECK_CONNECTION_INTERVAL)
                {
                    InternetConnectionCount = 0;
                    if (WebProcessor.CheckInternetConnection())
                    {
                        IsInternetExists = true;
                        try
                        {
                            CurrentContext.GetInstance().VersionData.VersionDetected =
                                                VersionController.GetInstance().GetVersionData(string.Empty);
                            if (CurrentContext.GetInstance().VersionData.VersionDetected)
                            {
                                _new_version_lb.TextAlign = ContentAlignment.MiddleCenter;
                                DetectVersionState();
                            }
                            else
                                CurrentContext.GetInstance().VersionData.State = VersionState.UNKNOWN;

                            SetVersionButton();
                            ConfVersionPanel();
                        }
                        catch
                        { CurrentContext.GetInstance().VersionData.State = VersionState.UNKNOWN; }
                    }
                    else
                    {
                        IsInternetExists = false;
                    }
                }
            }
        }
    }
}
