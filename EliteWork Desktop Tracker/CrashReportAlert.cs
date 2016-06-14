using EliteWork_Desktop_Tracker.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker
{
    public partial class CrashReportAlert : Form
    {
        public CrashReportAlert()
        {
            InitializeComponent();
            //this.TopMost = true;
        }

        private void _report_crash_btn_Click(object sender, EventArgs e)
        {
            _report_crash_btn.Enabled = false;
            _report_crash_manually_btn.Enabled = false;

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                try
                {
                    string resp = BugReportController.GetInstance().SendBugReport("Crash Report", true);
                    if (resp.Equals("Internet Connection Error"))
                    {
                        DialogResult result = MessageBox.Show("Internet Connection Error. Please report it later using the \"Bug Report\" feature under \"Settings\"", "Error", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                    if (resp.Equals("OK"))
                    {
                        DialogResult result = MessageBox.Show("The crash report was submitted successfully", "Success", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show(resp + " .Please report it later using the \"Bug Report\" feature under \"Settings\"", "Success", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }));
        }

        private void _report_crash_manually_btn_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void CrashReportAlert_FormClosing(object sender, FormClosingEventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
