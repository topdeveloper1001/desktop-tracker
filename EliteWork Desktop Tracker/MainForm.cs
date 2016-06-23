using Common;
using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Controllers;
using EliteWork_Desktop_Tracker.Helpers;
using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Properties;
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
    public partial class MainForm : Form, INotificationHandler, ILogHandler, INotifyHandler
    {
        private static int[] RGB_TRANS_MASK = { 230, 240, 250 };
        private Point lastPoint = Point.Empty;
        private bool _IsTimerPanel = false;
        private bool _MinimizeToTrayChecked = false;
        private bool _DisableNotifyChecked = false;
        private bool _BugReportFirstClick = false;
        private bool _IsReportSent = false;
        private bool _IsCloseApp = false;
        private bool _IsSessionSleep = false;
        private bool IsInternetExists = true;
        private int InternetConnectionCount = 0;
        private bool ShowNotifyForm = false;
        private ActualDataForm ActualDataForm = null;

        private string BUG_REPORT_TEXT = "To better understand the issue, please help share the following" + Environment.NewLine +
                    "details while reporting bugs in EliteWork tracker application." + Environment.NewLine + Environment.NewLine +
                    "1. Brief description - what happens, what you think etc." + Environment.NewLine +
                    "2. Steps to reproduce - how can we reproduce the bug?" + Environment.NewLine +
                    "3. Actual result - what result you are getting?" + Environment.NewLine +
                    "4. Expected result - what result you are expecting?" + Environment.NewLine +
                    "5. Priority(Low/High/Medium) - expected time to fix?" + Environment.NewLine + Environment.NewLine +
                    "Thank you for your patience.";

        public MainForm()
        {
            InitializeComponent();

            _new_version_pn.Visible = false;
            _settings_pn.Visible = false;
            _activity_log_pnl.Visible = false;
            _bug_report_pnl.Visible = false;
            _header_pn.SendToBack();
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;
            this.TransparencyKey = Color.FromArgb(RGB_TRANS_MASK[0], RGB_TRANS_MASK[1],
                RGB_TRANS_MASK[2]);

            _start_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _start_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _down_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _down_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _full_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _full_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _close_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _close_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _download_now_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _download_now_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _version_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _version_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_settings_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_settings_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _report_bugs_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _report_bugs_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _activity_log_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _activity_log_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _clear_cache_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _clear_cache_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _log_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _log_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _settings_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _settings_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_log_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_log_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _goback_bug_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _goback_bug_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _submit_bug_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _submit_bug_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            _to_tray_btn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            _to_tray_btn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;

            SetVersionButton();
            ConfVersionPanel();
            _welcome_lb.Text = string.Format("Welcome back, {0}!", CurrentContext.GetInstance().LoginData.Username);

            //LogController.GetInstance().SetLogHandler(this);
            SessionController.GetInstance().SetNotificationHandler(this);
            InitSettingsCheckboxes();

            if (!WebProcessor.CheckInternetConnection())
            {
                IsInternetExists = false;
            }
        }

        private void InitSettingsCheckboxes()
        {
            _MinimizeToTrayChecked = ReadSettingsFromRegistry(CommonConst.MINIMIZE_TO_TRAY);
            _minimize_to_tray_pb.BackgroundImage = _MinimizeToTrayChecked ?
                Resources.checked_checkbox_20 : Resources.unchecked_checkbox_20;
            _minimize_to_tray_pb.Refresh();

            _DisableNotifyChecked = ReadSettingsFromRegistry(CommonConst.DISABLE_TRAY_NOTIFICATION);
            _disable_notify_pb.BackgroundImage = _DisableNotifyChecked ?
                Resources.checked_checkbox_20 : Resources.unchecked_checkbox_20;
            _disable_notify_pb.Refresh();

            _settings_pn.Refresh();
        }
        
        private bool ReadSettingsFromRegistry(string name)
        {
            string value = string.Empty;
            RegistryProcessor.GetFromRegistry(CommonConst.REGISTRY_PATH,
                name, ref value, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
            if (string.IsNullOrEmpty(value))
                return false;
            else
            {
                try
                { return Boolean.Parse(value); }
                catch
                { return false; }
            }
        }

        private void ConfVersionPanel()
        {
            _new_version_lb.Text =
                        string.Format("New version {0} {1:0.00} found, {0} please upgrade {0} to stay in sync!",
                        Environment.NewLine, CurrentContext.GetInstance().VersionData.NewestVersion);

            _installed_version_lb.Text = string.Format("Installed Version {0:0.00}",
                    CurrentContext.GetInstance().VersionData.CurrentVersion);
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
        
        private void _start_btn_MouseEnter(object sender, EventArgs e)
        {
            if (CurrentContext.GetInstance().CurrentState == State.STARTED)
            {
                _start_btn.BackgroundImage = Resources.stop_button_over;
                _timer_pb.BackgroundImage = Resources.stop_button_over_n;
            }
            else
            {
                _start_btn.BackgroundImage = Resources.start_button_over;
                _timer_pb.BackgroundImage = Resources.start_button_over_n;
            }

            _start_btn.Refresh();
        }

        private void _start_btn_MouseLeave(object sender, EventArgs e)
        {
            if (CurrentContext.GetInstance().CurrentState == State.STARTED)
            {
                _start_btn.BackgroundImage = Resources.stop_button;
                _timer_pb.BackgroundImage = Resources.stop_button_n;
            }
            else
            {
                _start_btn.BackgroundImage = Resources.start_button;
                _timer_pb.BackgroundImage = Resources.start_button_n;
            }

            _start_btn.Refresh();
        }

        private void _start_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if (CurrentContext.GetInstance().CurrentState == State.STARTED)
                {
                    LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.GetSessionLine("Stopped"));
                    _start_btn.BackgroundImage = Resources.start_button_over;
                    _timer_pb.BackgroundImage = Resources.start_button_over_n;
                    this.Icon = Resources.combined_64;
                    _balloon_ni.Icon = Resources.combined_64;
                    ActualDataForm.BackgroundImage = Resources.notify_form_stopped;
                    _start_btn.Text = "START";
                    CurrentContext.GetInstance().CurrentState = State.STOPPED;
                    SessionController.GetInstance().StopSession(true);
                    StopTimerAndChangePanel();
                    if (!_DisableNotifyChecked)
                        ShowBalloonNotification("EliteWork Desktop Tracker", "Tracker Stopped");
                }
                else
                {
                    LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.GetSessionLine("Started"));
                    if (_MinimizeToTrayChecked)
                        MinimizeToSystemTray();
                    _start_btn.BackgroundImage = Resources.stop_button_over;
                    _timer_pb.BackgroundImage = Resources.stop_button_over_n;
                    this.Icon = Resources.icon_green;
                    _balloon_ni.Icon = Resources.icon_green;
                    ActualDataForm.BackgroundImage = Resources.notify_form_started_2;
                    _start_btn.Text = "STOP";
                    CurrentContext.GetInstance().CurrentState = State.STARTED;
                    SessionController.GetInstance().StartSession(true);
                    _timer_tm.Start();
                    TimerCall();
                    ShowNotifyForm = true;
                    if (!_DisableNotifyChecked)
                        ShowBalloonNotification("EliteWork Desktop Tracker", "Tracker Started");
                }
            }
            catch { return; }
            _start_btn.Refresh();
        }

        public void SessionStopped()
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                if (_IsCloseApp)
                    Application.Exit();
            });
        }

        private void _close_btn_Click(object sender, EventArgs e)
        {
            if (CurrentContext.GetInstance().CurrentState == State.STARTED)
            {
                DialogResult dialogResult = MessageBox.Show("A session is currently active, do you really want to quit the application? This will end the active session.",
                    "Exit", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    _IsCloseApp = true;
                    CloseSession();
                }
                else if (dialogResult == DialogResult.No)
                { }
            }
            else
                Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ActualDataForm = new ActualDataForm(this);
            ActualDataForm.Visible = false;
            int x = Screen.PrimaryScreen.WorkingArea.Width - ActualDataForm.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Height - ActualDataForm.Height;
            ActualDataForm.Location = new Point(x, y);
            ActualDataForm.StartPosition = FormStartPosition.Manual;
            NotifycationHelper.GetInstance().SetNotifyForm(ActualDataForm);
            NotifycationHelper.GetInstance().SetNotifyHandler(this);

            _timer_pb.Visible = false;
            AppendTransparentControl(_timer_pb, _first_timer_lb);
            AppendTransparentControl(_timer_pb, _second_timer_lb);
            AppendTransparentControl(_timer_pb, _tird_timer_lb);
            AppendTransparentControl(_timer_pb, _four_timer_lb);
            LogController.GetInstance().SetLogHandler(this);

            PowerManager.IsMonitorOnChanged += new EventHandler(MonitorOnChanged);
            SystemEvents.SessionSwitch += OnSessionSwitch;
            SystemEvents.PowerModeChanged += OnPowerChange;
            _current_timer_tm.Start();

            _balloon_ni.Visible = true;
            //_balloon_ni.ShowBalloonTip(CommonConst.BALLOON_DELAY);

            //MessageBoxManager.Yes = "NO";
            //MessageBoxManager.No = "YES";
            //MessageBoxManager.Register();
        }

        private void CloseSession()
        {
            LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSessionLine("Stopped"));
            _start_btn.BackgroundImage = Resources.start_button_over;
            _timer_pb.BackgroundImage = Resources.start_button_over_n;
            this.Icon = Resources.icon_gray;
            _balloon_ni.Icon = Resources.icon_gray;
            ActualDataForm.BackgroundImage = Resources.notify_form_stopped;
            _start_btn.Text = "START";
            CurrentContext.GetInstance().CurrentState = State.STOPPED;
            SessionController.GetInstance().StopSession(true);
            StopTimerAndChangePanel();
        }

        void MonitorOnChanged(object sender, EventArgs e)
        {
            if (!PowerManager.IsMonitorOn && !_IsSessionSleep)
            {
                LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.GetNavigationLine("Monitor status changed: Off"));
                CurrentContext.GetInstance().IsSessionSleep = true;
                _IsSessionSleep = true;
                CloseSession();
            }
        }

        void OnPowerChange(Object sender, PowerModeChangedEventArgs e)
        {
            switch (e.Mode)
            {
                case PowerModes.Suspend:
                    if (!_IsSessionSleep)
                    {
                        LogController.GetInstance().LogData(LogController.
                                                GetInstance().LogFormat.GetNavigationLine("Power mode status changed: Suspend"));
                        CurrentContext.GetInstance().IsSessionSleep = true;
                        _IsSessionSleep = true;
                        CloseSession();
                    }
                    break;
                default:
                    break;
            }
        }

        void OnSessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            if (e.Reason == SessionSwitchReason.SessionLogoff && !_IsSessionSleep)
            {
                LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.GetNavigationLine("System session status changed: Logoff"));
                CurrentContext.GetInstance().IsSessionSleep = true;
                _IsSessionSleep = true;
                CloseSession();
            }
            else if (e.Reason == SessionSwitchReason.SessionLock && !_IsSessionSleep)
            {
                LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.GetNavigationLine("System session status changed: Lock"));
                CurrentContext.GetInstance().IsSessionSleep = true;
                _IsSessionSleep = true;
                CloseSession();
            }

        }

        private void AppendTransparentControl(Control hostControl, Control attachedControl)
        {
            var pos = this.PointToScreen(attachedControl.Location);
            pos = hostControl.PointToClient(pos);
            attachedControl.Parent = hostControl;
            attachedControl.Location = pos;
            attachedControl.BackColor = Color.Transparent;
        }

        private void _timer_tm_Tick(object sender, EventArgs e)
        {
            TimerCall();
        }

        private void TimerCall()
        {
            Point currentSessionTime = SessionController.GetInstance().GetCurrentSessionTime();
            _second_timer_lb.Text = string.Format("{0:D2}h {1:D2}m", currentSessionTime.Y, currentSessionTime.X);
            ActualDataForm.SetTime(_second_timer_lb.Text);
            _second_timer_lb.Left = (_timer_pb.Width - _second_timer_lb.Width) / 2;

            if (!_IsTimerPanel)
            {
                ShowTimerPanel();
                _four_timer_lb.Text = "00m since last screenshot!";
                _IsTimerPanel = true;
            }
            else
            {
                int minutes = SessionController.GetInstance().SinceLastScreenshot();
                _four_timer_lb.Text = string.Format("{0:D2}m since last screenshot!", minutes);
            }
            _four_timer_lb.Left = (_timer_pb.Width - _four_timer_lb.Width) / 2;

            if (SessionController.GetInstance().SinceLastActivity() >= CommonConst.INACTIVE_INTERVAL)
            {
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSessionLine("Session stopped for inactivity!"));
                _start_btn.BackgroundImage = Resources.start_button_over;
                _timer_pb.BackgroundImage = Resources.start_button_over_n;
                this.Icon = Resources.icon_gray;
                _balloon_ni.Icon = Resources.icon_gray;
                ActualDataForm.BackgroundImage = Resources.notify_form_stopped;
                _start_btn.Text = "START";
                CurrentContext.GetInstance().CurrentState = State.STOPPED;
                CurrentContext.GetInstance().IsSessionSleep = true;
                _IsSessionSleep = true;
                SessionController.GetInstance().StopSession(false);
                StopTimerAndChangePanel();
                if (!_DisableNotifyChecked)
                    ShowBalloonNotification("EliteWork Desktop Tracker", "Session stopped for inactivity!");
            }
        }

        private void StopTimerAndChangePanel()
        {
            _timer_tm.Stop();
            _first_timer_lb.Text = "Last Session";
            _first_timer_lb.Left = (_timer_pb.Width - _first_timer_lb.Width) / 2;

            _tird_timer_lb.Text = "Tracking Stopped";
            ActualDataForm.SetStatus(_tird_timer_lb.Text);
            _tird_timer_lb.Left = (_timer_pb.Width - _tird_timer_lb.Width) / 2;
            _four_timer_lb.Visible = false;
            _IsTimerPanel = false;
        }

        private void ShowTimerPanel()
        {
            Point currentTime = SessionController.GetInstance().GetCurrentTime();
            _first_timer_lb.Text = string.Format("Started at {0:D2}:{1:D2}", currentTime.Y, currentTime.X);
            _first_timer_lb.Left = (_timer_pb.Width - _first_timer_lb.Width) / 2;

            _tird_timer_lb.Text = "Tracking Active";
            ActualDataForm.SetStatus(_tird_timer_lb.Text);
            _tird_timer_lb.Left = (_timer_pb.Width - _tird_timer_lb.Width) / 2;
            _second_timer_lb.Left = (_timer_pb.Width - _second_timer_lb.Width) / 2;
            _start_btn.Location = new Point(46, _timer_pb.Location.Y - (_start_btn.Height / 2 - _timer_pb.Height / 2));
            _timer_pb.Visible = true;
            _four_timer_lb.Visible = true;

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
        
        private void SetVersionButton()
        {
            if (CurrentContext.GetInstance().VersionData.State == VersionState.LATEST)
            {
                _version_btn.BackgroundImage = Resources.version_button;
                _version_btn.Enabled = false;
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Latest]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_AVAILABLE)
            {
                _version_btn.BackgroundImage = Resources.version_button_warn;
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Update Available]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else if (CurrentContext.GetInstance().VersionData.State == VersionState.UPDATE_REQUIRED)
            {
                _version_btn.BackgroundImage = Resources.version_button_alarm;
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Ver. {0:0.00} Installed [Update Required]", CurrentContext.GetInstance().VersionData.CurrentVersion);
            }
            else
            {
                _version_btn.BackgroundImage = Resources.version_button;
                _version_btn.Enabled = false;
                CurrentContext.GetInstance().VersionData.ButtonText =
                    string.Format("Checking for new version...", CurrentContext.GetInstance().VersionData.CurrentVersion);
                /* string.Format("Ver. {0:0.00} Installed [Unknown]", CurrentContext.GetInstance().VersionData.CurrentVersion);*/
            }
            _version_btn.Text = CurrentContext.GetInstance().VersionData.ButtonText;
        }

        private void _version_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("New Version Page"));
            _local_time_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _new_version_pn.BringToFront();
            _new_version_pn.Visible = true;
        }

        private void _goback_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _new_version_pn.Visible = false;
        }

        private void _minimize_to_tray_pb_Click(object sender, EventArgs e)
        {
            if (_MinimizeToTrayChecked)
            {
                RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                    CommonConst.MINIMIZE_TO_TRAY, "false",
                                        RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                _MinimizeToTrayChecked = false;
                _minimize_to_tray_pb.BackgroundImage = Resources.unchecked_checkbox_20;
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSettingsLine("Tray option disabled"));
            }
            else
            {
                RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                    CommonConst.MINIMIZE_TO_TRAY, "true",
                                        RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                _MinimizeToTrayChecked = true;
                _minimize_to_tray_pb.BackgroundImage = Resources.checked_checkbox_20;
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSettingsLine("Tray option enabled"));
            }
            _minimize_to_tray_pb.Refresh();
            _settings_pn.Refresh();
        }

        private void _disable_notify_pb_Click(object sender, EventArgs e)
        {
            if (_DisableNotifyChecked)
            {
                RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                    CommonConst.DISABLE_TRAY_NOTIFICATION, "false",
                                         RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                _DisableNotifyChecked = false;
                _disable_notify_pb.BackgroundImage = Resources.unchecked_checkbox_20;
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSettingsLine("Popup enabled"));
            }
            else
            {
                RegistryProcessor.SetToRegistry(CommonConst.REGISTRY_PATH,
                                    CommonConst.DISABLE_TRAY_NOTIFICATION, "true",
                                      RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                _DisableNotifyChecked = true;
                _disable_notify_pb.BackgroundImage = Resources.checked_checkbox_20;
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSettingsLine("Popup disabled"));
            }
            _disable_notify_pb.Refresh();
            _settings_pn.Refresh();
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

        private void _settings_btn_Click(object sender, EventArgs e)
        {
            _local_time_cs_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetNavigationLine("Settings"));
            _settings_pn.BringToFront();
            _settings_pn.Visible = true;
        }

        private void _goback_settings_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _settings_pn.Visible = false;
        }

        private void ShowBalloonNotification(string title, string text)
        {
            //_balloon_ni.Icon = SystemIcons.Information;
            _balloon_ni.BalloonTipTitle = title;
            _balloon_ni.BalloonTipText = text;
            _balloon_ni.BalloonTipIcon = ToolTipIcon.Info;
            _balloon_ni.Visible = true;
            _balloon_ni.ShowBalloonTip(CommonConst.BALLOON_DELAY);
        }

        public void ScreenshotTaken()
        {
            LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSessionLine("Screenshot Taken"));
            if (!_DisableNotifyChecked)
                ShowBalloonNotification("EliteWork Desktop Tracker", "Screenshot Taken");
        }

        private void _activity_log_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Activity Log Page"));
            _local_time_al_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _activity_log_pnl.BringToFront();
            _activity_log_pnl.Visible = true;
            _settings_pn.Visible = false;
        }

        private void _goback_log_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _activity_log_pnl.Visible = false;
            _settings_pn.Visible = true;
        }

        private void _balloon_ni_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Maximize From Tray"));
            Show();
            this.WindowState = FormWindowState.Normal;
            //_balloon_ni.Visible = false;
            this.ShowInTaskbar = true;
        }

        private void MinimizeToSystemTray()
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Minimize To Tray"));
            _balloon_ni.Visible = true;
            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void _down_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Minimize\" button"));
            this.WindowState = FormWindowState.Minimized;
        }

        public void AppendLine(string line)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                _activity_log_tb.AppendText(string.Format("{0}{1}", line, Environment.NewLine));
            });
        }

        private void _log_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetNavigationLine("View work logs"));
            SessionController.GetInstance().OpenLogsUrlInBrowser();
        }

        private void _bug_report_tb_Click(object sender, EventArgs e)
        {
            if (!_BugReportFirstClick)
            {
                _BugReportFirstClick = true;
                _bug_report_tb.ForeColor = Color.Black;
                _bug_report_tb.Text = string.Empty;
                _bug_report_tb.Font = new System.Drawing.Font("Calibri",
                    14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            }
        }

        private void _report_bugs_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Bug Report Page"));
            _local_time_rb_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _bug_report_pnl.BringToFront();
            _bug_report_pnl.Visible = true;
            _settings_pn.Visible = false;
        }

        private void _goback_bug_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Go Back\" button"));
            _settings_pn.Visible = true;
            _bug_report_pnl.Visible = false;
            if (_IsReportSent)
            {
                _IsReportSent = false;
                _bug_report_tb.ForeColor = Color.Gray;
                _bug_report_tb.Font = new System.Drawing.Font("Calibri Light",
                    14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
                _bug_report_tb.Text = BUG_REPORT_TEXT;
                _BugReportFirstClick = false;
            }
        }

        private void _submit_bug_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Submit Bug Report\" button"));
            if (string.IsNullOrEmpty(_bug_report_tb.Text) ||
                _bug_report_tb.Text.Contains("reporting bugs in EliteWork tracker"))
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Error, no description submitted for bug report."));
                MessageBox.Show("Please add some brief description to your bug report.", "Wrong bug report");
                return;
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                if (BugReportController.GetInstance().SendBugReport(_bug_report_tb.Text, false).Equals("OK"))
                {
                    _IsReportSent = true;
                    MessageBox.Show("The bug report was submitted successfully", "Success");
                }
                else
                    MessageBox.Show("Can't send the bug report", "Error");
            }));
        }

        private void _to_tray_btn_Click(object sender, EventArgs e)
        {
            MinimizeToSystemTray();
        }

        private void _clear_cache_btn_Click(object sender, EventArgs e)
        {
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Click \"Clear Cache\" button"));
            DialogResult dialogResult = MessageBox.Show("Do you really want to clear the cached data?", 
                "Clear cached data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ClearCacheState state = SessionController.GetInstance().ClearCache();
                if (state == ClearCacheState.EMPTY_CACHE)
                    MessageBox.Show("No cached data found", "Notification");
                else if (state == ClearCacheState.ERROR)
                    MessageBox.Show("There was an error clearning cache, please try again", "Notification");
                else if (state == ClearCacheState.SUCCESS)
                    MessageBox.Show("The cache has been cleared successfully", "Notification");
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
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
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;   // WS_EX_COMPOSITED
                cp.Style |= 0x20000; // AHMED_EDIT
                cp.ClassStyle |= 0x8; // AHMED EDIT
                return cp;
            }
        }

        private void _connection_timer_tm_Tick(object sender, EventArgs e)
        {
            if (_four_timer_lb.Text.Contains("screenshot"))
            {
                _four_timer_lb.Text = "Connection lost, retrying" + Environment.NewLine + 
                    "in " + SessionController.GetInstance().GetErrorTimerInterval() + " minute(s)!";
            }
            else
            {
                int minutes = SessionController.GetInstance().SinceLastScreenshot();
                _four_timer_lb.Text = string.Format("{0:D2}m since last screenshot!", minutes);
            }
            _four_timer_lb.Left = (_timer_pb.Width - _four_timer_lb.Width) / 2;
        }

        public void ConnectionStateChanged(bool connectionExist)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                if (connectionExist)
                {
                    _connection_timer_tm.Stop();
                    int minutes = SessionController.GetInstance().SinceLastScreenshot();
                    _four_timer_lb.Text = string.Format("{0:D2}m since last screenshot!", minutes);
                    _four_timer_lb.Left = (_timer_pb.Width - _four_timer_lb.Width) / 2;
                }
                else
                    _connection_timer_tm.Start();
            });
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

        private void _current_timer_tm_Tick(object sender, EventArgs e)
        {
            _local_time_al_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _local_time_cs_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _local_time_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);
            _local_time_rb_lb.Text = string.Format("Local Time: {0:hh:mm tt}", DateTime.Now);

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

        public void ActivityFired()
        {
            if (CurrentContext.GetInstance().CurrentState == State.STOPPED && _IsSessionSleep)
            {
                _IsSessionSleep = false;
                CurrentContext.GetInstance().IsSessionSleep = false;
                LogController.GetInstance().LogData(LogController.
                                        GetInstance().LogFormat.GetSessionLine("Session restarted automatically!"));
                if (_MinimizeToTrayChecked)
                    MinimizeToSystemTray();
                _start_btn.BackgroundImage = Resources.stop_button_over;
                _timer_pb.BackgroundImage = Resources.stop_button_over_n;
                this.Icon = Resources.icon_green;
                _balloon_ni.Icon = Resources.icon_green;
                ActualDataForm.BackgroundImage = Resources.notify_form_started_2;
                _start_btn.Text = "STOP";
                CurrentContext.GetInstance().CurrentState = State.STARTED;
                SessionController.GetInstance().StartSession(false);
                _timer_tm.Start();
                TimerCall();
                if (!_DisableNotifyChecked)
                    ShowBalloonNotification("EliteWork Desktop Tracker", "Session restarted automatically!");
            }
        }

        public void TimerTick(bool show)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                if (show)
                    ActualDataForm.Visible = true;
                else
                    ActualDataForm.Visible = false;
            });
        }

        private void _balloon_ni_MouseMove(object sender, MouseEventArgs e)
        {
            _balloon_ni.Visible = true;

            if (ShowNotifyForm)
                NotifycationHelper.GetInstance().MouseMove();
        }

        public void ShowMainForm()
        {
            try
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
            }
            catch { }
        }
    }
}
