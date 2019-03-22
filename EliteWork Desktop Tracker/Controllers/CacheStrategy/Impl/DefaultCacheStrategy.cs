using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Context.Timer;
using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EWLocalCache;
using EWWebProcessor;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.CacheStrategy.Impl
{
    class DefaultCacheStrategy : ICacheStrategy
    {
        private IServerApiProvider _ServerApiProvider = null;

        /* MOCK */
       // private int FAKE = -1;
        /* MOCK */    

        public void SetServerApiProvider(IServerApiProvider apiProvider)
        {
            _ServerApiProvider = apiProvider;
        }

        public bool PostCurrentSession(string currentSessionId)
        {
            NameValueCollection postParams = null;
            string response = PrepareDataAndPostSession(currentSessionId, true, ref postParams);
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine(string.Format("Server Reply \"{0}\"", response)));
            AnalizePostResponse(response, currentSessionId, postParams);
            return true;
        }

        public bool PostOldSessions(string currentSessionId)
        {
            IList<string> cachedDirectories = CacheProcessor.GetCachedSessionFolders();
            if (cachedDirectories.Count <= 0)
                return true;

            foreach (string cachedDirectory in cachedDirectories)
            {
                NameValueCollection postParams = null;
                string dirName = (new DirectoryInfo(cachedDirectory)).Name;
                if (currentSessionId.Equals(dirName))
                    continue;

                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Data Uploading"));
                string response = PrepareDataAndPostSession(dirName, false, ref postParams);

                if (response.Equals("EMPTY_FOLDER"))
                    LogController.GetInstance().LogData(LogController.
                                GetInstance().LogFormat.GetNetworkLine(string.Format("\"{0}\"", response)));
                else
                    LogController.GetInstance().LogData(LogController.
                                GetInstance().LogFormat.GetNetworkLine(string.Format("Server Reply \"{0}\"", response)));

                AnalizePostResponse(response, dirName, null);
            }
            return true;
        }

        private string PrepareDataAndPostSession(string sessionId, 
            bool appendData, ref NameValueCollection postParams)
        {
            //CacheProcessor.CreateDecryptedImages(sessionId);
            //List<string> decryptedImagePaths = new List<string>(CacheProcessor.GetDecryptedImagePaths(sessionId));
            //if (decryptedImagePaths == null || decryptedImagePaths.Count <= 0)
            //    return string.Empty;

            CacheProcessor.DecryptData(sessionId);
            List<string> loadedData = CacheProcessor.LoadData(sessionId);

            if ((loadedData == null || loadedData.Count <= 0) && !appendData)
            {
                CacheProcessor.DeleteSessionFolder(sessionId);
                return "EMPTY_FOLDER";
            }

            CacheProcessor.CreateDecryptedImages(sessionId);
            List<string> decryptedImagePaths = new List<string>(CacheProcessor.GetDecryptedImagePaths(sessionId));

            if (appendData)
                postParams = _ServerApiProvider.AppendPostSessionParams(loadedData,
                CurrentContext.GetInstance().Session, CurrentContext.GetInstance().LoginData);
            else
                postParams = _ServerApiProvider.CreatePostSessionParams(loadedData);

            string postUrl = _ServerApiProvider.CreatePostSessionUrl();

            if (!WebProcessor.CheckInternetConnection())
            {
                ErrorTimer.GetInstance().StartTimer();
                return "CONNECTION_FAIL";
            }

            return WebProcessor.UploadFileWithParams(postUrl,
            _ServerApiProvider.PrepareFilesData(decryptedImagePaths, "screenshot_files[]", "image/jpeg"), postParams);

            /* MOCK */
            /*FAKE++;

            if (FAKE == 0)
                return WebProcessor.UploadFileWithParams(postUrl,
                _ServerApiProvider.PrepareFilesData(decryptedImagePaths), postParams);
            else if (FAKE > 0 && FAKE < 3)
                return "FAIL";
            else
                return WebProcessor.UploadFileWithParams(postUrl,
                _ServerApiProvider.PrepareFilesData(decryptedImagePaths), postParams);*/

                /* MOCK */

        }

        private void AnalizePostResponse(string response, 
            string sessionId, NameValueCollection postData)
        {
            if (string.IsNullOrEmpty(response) || response.Equals("EMPTY_FOLDER"))
                return;

            if (!_ServerApiProvider.AnalizePostSessionResponse(response))
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Upload Fail"));
                CacheProcessor.CreateEncryptedImages(sessionId);
                CacheProcessor.DeleteDecryptedImages(sessionId);

                if (postData != null)
                {
                    IList<string> data = new List<string>();
                    for (int i = 0; i < postData.Count; i++)
                    {
                        string key = postData.GetKey(i);
                        string value = postData[key];
                        data.Add(string.Format("{0}={1}", key, value));
                    }
                    CacheProcessor.SaveData(data, sessionId, true);
                }
                else
                    CacheProcessor.EncryptData(sessionId);

                CacheProcessor.DeleteDecryptedData(sessionId);
                CurrentContext.GetInstance().Session.KeyboardTimes.Clear();
                CurrentContext.GetInstance().Session.MouseTimes.Clear();
                // Ahmed
                CurrentContext.GetInstance().Session.ActiveAppTitles.Clear();
            }
            else
            {
                LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine("Upload Success"));
                CurrentContext.GetInstance().Session.KeyboardTimes.Clear();
                CurrentContext.GetInstance().Session.MouseTimes.Clear();
                CurrentContext.GetInstance().Session.ActiveAppTitles.Clear(); // Ahmed
                CacheProcessor.DeleteAllImages(sessionId);
                CacheProcessor.DeleteEncryptedData(sessionId);
                CacheProcessor.DeleteDecryptedData(sessionId);
                CacheProcessor.DeleteSessionFolder(sessionId);
            }
        }
    }
}
