using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker
{
    public partial class ActualDataForm : Form
    {
        private static int[] RGB_TRANS_MASK = { 230, 240, 250 };

        public ActualDataForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;
            this.TransparencyKey = Color.FromArgb(RGB_TRANS_MASK[0], RGB_TRANS_MASK[1],
                RGB_TRANS_MASK[2]);
            this.TopMost = true;
        }

        public void SetStatus(string status)
        {
            _tird_timer_lb.Text = status;
            _tird_timer_lb.Left = (this.Width - _tird_timer_lb.Width) / 2;
        }

        public void SetTime(string time)
        {
            _second_timer_lb.Text = time;
            _second_timer_lb.Left = (this.Width - _second_timer_lb.Width) / 2;
        }
    }
}
