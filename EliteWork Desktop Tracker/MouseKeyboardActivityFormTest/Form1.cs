using EWActivityCatcher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MouseKeyboardActivityFormTest
{
    public partial class Form1 : Form, IActivityHandler
    {
        private int count = 0;

        public Form1()
        {
            InitializeComponent();
            ActivityProcessor.GetInstance().SetActivityHandler(this);
            ActivityProcessor.GetInstance().SubscribeToMouseKeyEvents();
        }

        public void KeyboardActionFired(int timestamp)
        {
            count++;
            label1.Text = count.ToString();
        }

        public void MouseActionFired(int timestamp)
        {
            count++;
            label1.Text = count.ToString();
        }

        public void ScreenshotActionFired(Image img, int timestamp)
        {
            throw new NotImplementedException();
        }
    }
}
