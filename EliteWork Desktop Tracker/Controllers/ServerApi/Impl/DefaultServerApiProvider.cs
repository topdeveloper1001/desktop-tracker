using Common;
using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Model;
using EWWebProcessor;
using EWWebProcessor.FileUploader.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.ServerApi.Impl
{
    class DefaultServerApiProvider : IServerApiProvider
    {
        public LoginState AnalizeLoginSuccess(string response)
        {
            if (response.Contains("LOGIN_SUCCESS"))
            {
                string[] parts = response.Split(':');
                if (parts != null && parts.Length == 3)
                {
                    CurrentContext.GetInstance().LoginData.Username = parts[1];
                    return LoginState.LOGGED;
                }
            }
            else if (response.Contains("LOGIN_FAILED"))
                return LoginState.LOGIN_FAIL;

            return LoginState.CONNECTION_FAIL;
        }

        public string CreateLoginUrl(string login, string password)
        {
            return string.Format(CommonConst.EW_API_LOGIN_URL + "?email={0}&password={1}", login, password);
        }

        public string CreatePostSessionUrl()
        {
            return CommonConst.EW_API_POST_SESSION_URL;
        }

        public NameValueCollection CreatePostSessionParams(ISession session, ILoginData loginData)
        {
            return WebUtils.ToNameValueCollection(CreatePostSessionParamsToDic(session, loginData));
        }

        public NameValueCollection CreatePostSessionParams(List<string> loadedData)
        {
            return WebUtils.ToNameValueCollection(CreatePostSessionParamsToDic(loadedData));
        }
        
        public NameValueCollection AppendPostSessionParams(List<string> loadedData, ISession session, ILoginData loginData)
        {
            return WebUtils.ToNameValueCollection(AppendDataForSaveToDic(loadedData, session, loginData));
        }

        public IEnumerable<UploadFile> PrepareFilesData(List<string> filePaths, string fileName, string contentType)
        {
            IList<UploadFile> uploadFiles = new List<UploadFile>();
            foreach (string filePath in filePaths)
            {
                UploadFile uploadFile = new UploadFile();
                try
                {
                    uploadFile.Name = fileName /*"screenshot_files[]"*/;
                    uploadFile.Filename = Path.GetFileName(filePath);
                    uploadFile.ContentType = contentType /*"image/png"*/;
                    uploadFile.Stream = File.Open(filePath, FileMode.Open);
                }
                catch { continue; }

                uploadFiles.Add(uploadFile);
            }
            return uploadFiles;
        }

        public bool AnalizePostSessionResponse(string response)
        {
            return response.Contains("OK");
        }

        private IDictionary<string, string> CreatePostSessionParamsToDic(List<string> loadedData)
        {
            IDictionary<string, string> postParams = new Dictionary<string, string>();
            foreach (string data in loadedData)
                postParams.Add(data.Split('=')[0], data.Split('=')[1]);

            return postParams;
        }

        private IDictionary<string, string> CreatePostSessionParamsToDic(ISession session, ILoginData loginData)
        {
            IDictionary<string, string> postParams = new Dictionary<string, string>();
            postParams.Add("username", loginData.Login);
            postParams.Add("password", loginData.Password);
            postParams.Add("start_timestamp", session.StartTimestamp.ToString());
            postParams.Add("stop_time", session.StopTime.ToString());
            postParams.Add("post_time", TimingHelper.GetCurrentTimestamp().ToString()/*session.PostTime.ToString()*/);

            string keyTimes = ConvertListToString(session.KeyboardTimes);
            if (!string.IsNullOrEmpty(keyTimes))
                postParams.Add("key_times", keyTimes);

            string mouseTimes = ConvertListToString(session.MouseTimes);
            if (!string.IsNullOrEmpty(mouseTimes))
                postParams.Add("mouse_times", mouseTimes);

            // Ahmed
            string activeAppTitles = ConvertStringListToString(session.ActiveAppTitles);
            if (!string.IsNullOrEmpty(activeAppTitles))
                postParams.Add("active_app_titles", activeAppTitles);

            return postParams;
        }

        private string ConvertStringListToString(List<String> parameters)
        {
            if (parameters.Count <= 0)
                return string.Empty;

            string result = string.Empty;
            parameters.Sort();
            StringBuilder builder = new StringBuilder();
            foreach (String parameter in parameters)
                builder.Append(string.Format("{0},", parameter));

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private string ConvertListToString(List<int> parameters)
        {
            if (parameters.Count <= 0)
                return string.Empty;

            string result = string.Empty;
            parameters.Sort();
            StringBuilder builder = new StringBuilder();
            foreach (int parameter in parameters)
                builder.Append(string.Format("{0},", parameter));

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        private IDictionary<string, string> AppendDataForSaveToDic(List<string> loadedData, ISession session, ILoginData loginData)
        {
            IDictionary<string, string> postParams = new Dictionary<string, string>();
            postParams.Add("username", loginData.Login);
            postParams.Add("password", loginData.Password);
            postParams.Add("start_timestamp", session.StartTimestamp.ToString());
            postParams.Add("stop_time", session.StopTime.ToString());
            postParams.Add("post_time", TimingHelper.GetCurrentTimestamp().ToString()/*session.PostTime.ToString()*/);

            string keyTimes = CombineData(session.KeyboardTimes, loadedData, "key_times");
            if (!string.IsNullOrEmpty(keyTimes))
                postParams.Add("key_times", keyTimes);

            string mouseTimes = CombineData(session.MouseTimes, loadedData, "mouse_times");
            if (!string.IsNullOrEmpty(mouseTimes))
                postParams.Add("mouse_times", mouseTimes);

            string activeAppTitles = CombineStringData(session.ActiveAppTitles, loadedData, "active_app_titles");
            if (!string.IsNullOrEmpty(activeAppTitles))
                postParams.Add("active_app_titles", activeAppTitles);

            return postParams;
        }

        private string CombineStringData(List<String> sessionData, List<string> loadedData, string key)
        {
            List<String> resultData = new List<String>();

            try
            {
                string data = ParseDataValue(loadedData, key);
                string[] dataArr = null;
                if (!string.IsNullOrEmpty(data))
                {
                    dataArr = data.Split(',');
                    if (dataArr != null && dataArr.Length > 0)
                    {
                        List<string> dataLst = new List<string>(dataArr);
                        resultData.AddRange(dataLst.ConvertAll<String>(delegate (string i) { return i; }));
                    }
                }
            }
            catch
            {
                resultData = new List<String>();
            }
            if (sessionData != null && sessionData.Count > 0)
                resultData.AddRange(sessionData);

            return ConvertStringListToString(resultData);
        }

        private string CombineData(List<int> sessionData, List<string> loadedData, string key)
        {
            List<int> resultData = new List<int>();

            try
            {
                string data = ParseDataValue(loadedData, key);
                string[] dataArr = null;
                if (!string.IsNullOrEmpty(data))
                {
                    dataArr = data.Split(',');
                    if (dataArr != null && dataArr.Length > 0)
                    {
                        List<string> dataLst = new List<string>(dataArr);
                        resultData.AddRange(dataLst.ConvertAll<int>(delegate (string i) { return Int32.Parse(i); }));
                    }
                }
            }
            catch
            {
                resultData = new List<int>();
            }
            if (sessionData != null && sessionData.Count > 0)
                resultData.AddRange(sessionData);

            return ConvertListToString(resultData);
        }

        private string ParseDataValue(List<string> savedData, string key)
        {
            foreach (string line in savedData)
            {
                if ((line.Split('=')[0]).Equals(key))
                    return line.Split('=')[1];
            }

            return string.Empty;
        }

        public IList<string> PrepareDataForSave(ISession session, ILoginData loginData)
        {
            IList<string> buffer = new List<string>();
            buffer.Add(string.Format("{0}={1}{2}", "username", loginData.Login, Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "password", loginData.Password, Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "start_timestamp", 
                session.StartTimestamp.ToString(), Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "stop_time", 
                session.StopTime.ToString(), Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "post_time", 
                session.PostTime.ToString(), Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "key_times", 
                ConvertListToString(session.KeyboardTimes), Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "mouse_times", 
                ConvertListToString(session.MouseTimes), Environment.NewLine));
            buffer.Add(string.Format("{0}={1}{2}", "active_app_titles",
                ConvertStringListToString(session.ActiveAppTitles), Environment.NewLine));
            return buffer;
        }

        public string CreateVersionUrl(string email, string curVer)
        {
            if (string.IsNullOrEmpty(email))
                return string.Format(CommonConst.EW_API_CURRENT_VERSION + "?v={0}", curVer);
            else
                return string.Format(CommonConst.EW_API_CURRENT_VERSION + "?email={0}&v={1}", email, curVer);
        }

        public Dictionary<double, int> AnalizeVersionResponse(string resp)
        {
            if (string.IsNullOrEmpty(resp))
                return null;

            string[] parts = resp.Split(';');
            if (parts == null || parts.Length != 2)
                return null;

            Dictionary<double, int> versionData = new Dictionary<double, int>();
            try
            { versionData.Add(Double.Parse(parts[0], CultureInfo.InvariantCulture), Int32.Parse(parts[1])); }
            catch
            { return null; }

            return versionData;
        }

        public string CreateServerLogsUrl()
        {
            return string.Format(CommonConst.EW_API_VIEWLOG + "?email={0}&password={1}", 
                CurrentContext.GetInstance().LoginData.Login, CurrentContext.GetInstance().LoginData.Password);
        }

        public string CreateBugReportUrl()
        {
            return CommonConst.EW_API_REPORT_URL;
        }
    }
}
