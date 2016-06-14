using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EWLocalCache;
using System.Collections.Generic;
using Common;
using System.IO;
using System.Threading;
using System.Drawing;
using EWActivityCatcher;

namespace EWLocalCacheTest
{
    [TestClass]
    public class EncryptionTest
    {
        [TestMethod]
        public void EncryptDecryptText()
        {
            string initial = "It will be encoded and decoded just the same, because you are just looking at the bytes";
            byte[] encrypted = CacheEncrypter.EncryptData(CacheUtils.GetBytes(initial), "s43a2x89");
            string wrongResult = CacheUtils.GetString(encrypted);
            Assert.AreNotEqual(initial, wrongResult);
            string result = CacheUtils.GetString(CacheEncrypter.DecryptData(encrypted, "s43a2x89"));
            Assert.AreEqual(initial, result);
        }

        private IList<string> PrepareData()
        {
            IList<string> data = new List<string>();
            data.Add("username=test_user");
            data.Add("password=test_pass");
            data.Add("start_timestamp=" + TimingHelper.GetCurrentTimestamp().ToString());
            data.Add("stop_time=-1");
            data.Add("post_time=" + TimingHelper.GetCurrentTimestamp().ToString());
            data.Add("key_times=2,45,88,109,332,2000");
            data.Add("mouse_times=2,45,88,109,332,2000");
            return data;
        }

        [TestMethod]
        public void SaveEncryptedDataToFile()
        {
            IList<string> data = PrepareData();
            string sessionId = (100 + TimingHelper.GetCurrentTimestamp()).ToString();
            CacheProcessor.SaveData(data, sessionId, true);
            Thread.Sleep(2000);
            CacheProcessor.SaveData(data, sessionId, true);
            Assert.IsTrue(File.Exists(CacheProcessor.GetDataEncryptedFilePath(sessionId)));
            CacheProcessor.DeleteAllSessions();
        }

        [TestMethod]
        public void SaveDecryptedDataToFile()
        {
            IList<string> data = PrepareData();
            string sessionId = TimingHelper.GetCurrentTimestamp().ToString();
            CacheProcessor.SaveData(data, sessionId, false);
            Thread.Sleep(2000);
            CacheProcessor.SaveData(data, sessionId, false);
            Assert.IsTrue(File.Exists(CacheProcessor.GetDataDecryptedFilePath(sessionId)));
            CacheProcessor.DeleteAllSessions();
        }

        [TestMethod]
        public void EncryptDecryptTextFromFile()
        {
            IList<string> data = PrepareData();
            string sessionId = (200 + TimingHelper.GetCurrentTimestamp()).ToString();
            CacheProcessor.SaveData(data, sessionId, false);
            CacheProcessor.EncryptData(sessionId);
            CacheProcessor.DeleteDecryptedData(sessionId);
            CacheProcessor.DecryptData(sessionId);
            CacheProcessor.DeleteEncryptedData(sessionId);
            IList<string> loadedData = CacheProcessor.LoadData(sessionId);

            Assert.IsTrue(loadedData.Count == data.Count);
            for (int i = 0; i < loadedData.Count; i++)
            {
                Assert.IsTrue(data[i].Equals(loadedData[i]));
            }

            CacheProcessor.DeleteAllSessions();
        }

        [TestMethod]
        public void DecryptedImageSaveTest()
        {
            Image img = ActivityProcessor.GetInstance().MakeScreenshot();
            string sessionId = (200 + TimingHelper.GetCurrentTimestamp()).ToString();
            CacheProcessor.SaveImage(img, "300", sessionId, false);
            string[] imgPaths = CacheProcessor.GetDecryptedImagePaths(sessionId);
            Assert.IsTrue(imgPaths.Length == 1);
            Assert.IsTrue(imgPaths[0].Contains("300.jpg"));
            CacheProcessor.DeleteAllSessions();
        }

        [TestMethod]
        public void EncryptedImageSaveTest()
        {
            Image img = ActivityProcessor.GetInstance().MakeScreenshot();
            string sessionId = (200 + TimingHelper.GetCurrentTimestamp()).ToString();
            CacheProcessor.SaveImage(img, "300", sessionId, true);
            string[] imgPaths = CacheProcessor.GetEncryptedImagePaths(sessionId);
            Assert.IsTrue(imgPaths.Length == 1);
            Assert.IsTrue(imgPaths[0].Contains("300"));
            CacheProcessor.DeleteAllSessions();
        }

        [TestMethod]
        public void SaveEncryptDecryptImageTest()
        {
            Image img = ActivityProcessor.GetInstance().MakeScreenshot();
            string sessionId = (200 + TimingHelper.GetCurrentTimestamp()).ToString();
            CacheProcessor.SaveImage(img, "300", sessionId, false);
            CacheProcessor.CreateEncryptedImages(sessionId);
            CacheProcessor.DeleteDecryptedImages(sessionId);
            CacheProcessor.CreateDecryptedImages(sessionId);
            CacheProcessor.DeleteEncryptedImages(sessionId);
            string[] imgPaths = CacheProcessor.GetDecryptedImagePaths(sessionId);
            Assert.IsTrue(imgPaths.Length == 1);
            Assert.IsTrue(imgPaths[0].Contains("300.jpg"));
        }
    }
}
