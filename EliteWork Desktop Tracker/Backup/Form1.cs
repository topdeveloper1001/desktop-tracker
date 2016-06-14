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
    public partial class Form1 : Form
    {
        private static int[] RGB_TRANS_MASK = { 230, 240, 250 };
        private Point lastPoint = Point.Empty;

        public Form1()
        {
            InitializeComponent();

            _login_btn.FlatAppearance.BorderColor = Color.FromArgb(255, 236, 123, 46);
            _login_btn.BackColor = Color.FromArgb(255, 244, 175, 146);

            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = this.BackgroundImage.Width;
            this.Height = this.BackgroundImage.Height;
            this.TransparencyKey = Color.FromArgb(RGB_TRANS_MASK[0], RGB_TRANS_MASK[1],
                RGB_TRANS_MASK[2]);
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

        private void _login_btn_MouseUp(object sender, MouseEventArgs e)
        {
            _login_btn.BackColor = Color.FromArgb(255, 244, 175, 146);
        }

        private void _login_btn_MouseHover(object sender, EventArgs e)
        {
            _login_btn.BackColor = Color.FromArgb(255, 244, 175, 146);
        }
    }
}
