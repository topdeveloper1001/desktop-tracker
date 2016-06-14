using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace EWActivityCatcher
{
    public interface IActivityHandler
    {
        void MouseActionFired(int timestamp);
        void KeyboardActionFired(int timestamp);
        void ScreenshotActionFired(Image img, int timestamp);
    }
}
