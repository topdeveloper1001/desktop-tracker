using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker.Helpers
{
    class NotifycationHelper : INotifyHandler
    {
        private static NotifycationHelper _Instance = null;

        private bool IsOverIcon = false;
        private int IconArea = 8;
        private KeyValuePair<int, int> OverIconPoint;
        private Form NotifyForm = null;
        private INotifyHandler _Handler = null;

        private NotifycationHelper()
        {
            NotifyTimer.GetInstance().InitTimer(this);
        }

        public static NotifycationHelper GetInstance()
        {
            if (_Instance == null)
                _Instance = new NotifycationHelper();

            return _Instance;
        }

        public void SetNotifyForm(Form notifyForm)
        {
            NotifyForm = notifyForm;
        }

        public void SetNotifyHandler(INotifyHandler handler)
        {
            _Handler = handler;
        }

        public void MouseMove()
        {
            if (!IsOverIcon)
            {
                IsOverIcon = true;
                NotifyTimer.GetInstance().StartTimer();
                OverIconPoint = new KeyValuePair<int, int>(Cursor.Position.X, Cursor.Position.Y);
                _Handler.TimerTick(true);
            }
        }

        public void TimerTick(bool show)
        {
            int xDelta = Math.Abs(Cursor.Position.X - OverIconPoint.Key);
            int yDelta = Math.Abs(Cursor.Position.Y - OverIconPoint.Value);
            if (IconArea < xDelta || IconArea < yDelta)
            {
                IsOverIcon = false;
                NotifyTimer.GetInstance().StopTimer();
                _Handler.TimerTick(false);
            }
        }
    }
}
