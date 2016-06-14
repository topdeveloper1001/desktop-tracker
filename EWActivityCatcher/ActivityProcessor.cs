using Common;
using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EWActivityCatcher
{
    public class ActivityProcessor
    {
        private static ActivityProcessor _ActivityProcessor = null;
        private IActivityHandler _ActivityHandler = null;
        private IKeyboardMouseEvents _GlobalHook;

        private ActivityProcessor() { }

        public static ActivityProcessor GetInstance()
        {
            if (_ActivityProcessor == null)
                _ActivityProcessor = new ActivityProcessor();

            return _ActivityProcessor;
        }

        public void SetActivityHandler(IActivityHandler ActivityHandler)
        {
            _ActivityHandler = ActivityHandler;
        }

        public Image MakeScreenshot()
        {
            Bitmap img = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(img))
            {
                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                 Screen.PrimaryScreen.Bounds.Y,
                                 0, 0,
                                 img.Size,
                                 CopyPixelOperation.SourceCopy);
            }
            Size imgSize = Utils.CalcScreenshotSize(img);
            if (imgSize.Width != img.Width)
                img = Utils.ResizeImage(img, imgSize.Width, imgSize.Height);

            if (_ActivityHandler != null)
                _ActivityHandler.ScreenshotActionFired(img, TimingHelper.GetCurrentTimestamp());

            return img;
        }

        #region Mouse / Keyboard Events

        public void SubscribeToMouseKeyEvents()
        {
            _GlobalHook = Hook.GlobalEvents();
            _GlobalHook.MouseDownExt += MouseDown;
            _GlobalHook.MouseMoveExt += MouseDown;
            _GlobalHook.KeyPress += KeyPress;
        }

        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (_ActivityHandler != null)
                _ActivityHandler.KeyboardActionFired(TimingHelper.GetCurrentTimestamp());
        }

        private void MouseDown(object sender, MouseEventExtArgs e)
        {
            if (_ActivityHandler != null)
                _ActivityHandler.MouseActionFired(TimingHelper.GetCurrentTimestamp());
        }

        public void UnsubscribeFromMouseKeyEvents()
        {
            _GlobalHook.MouseDownExt -= MouseDown;
            _GlobalHook.MouseMoveExt -= MouseDown;
            _GlobalHook.KeyPress -= KeyPress;
            _GlobalHook.Dispose();
        }

        #endregion
    }
}
