using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EWActivityCatcher;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace EWActivityCatcherTest
{
    [TestClass]
    public class ActivityTest
    {
        [TestMethod]
        public void ScreenshotTest()
        {
            Image img = ActivityProcessor.GetInstance().MakeScreenshot();
            img.Save("test.jpg");
            Assert.IsTrue(File.Exists("test.jpg"));
        }
    }
}
