using Common;
using EliteWork_Desktop_Tracker.Context;
using EWWebProcessor;
using RegistryLib;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers
{
    class BugReportController
    {
        private static BugReportController _Controller = null;

        private BugReportController() { }

        public static BugReportController GetInstance()
        {
            if (_Controller == null)
                _Controller = new BugReportController();

            return _Controller;
        }

        public string SendBugReport(string report, bool isCrashReport)
        {
            if (!WebProcessor.CheckInternetConnection())
                return "Internet Connection Error";

            string url = SessionController.GetInstance().ServerApiProvider.CreateBugReportUrl();

            string email = "undefined@elitework.com";
            if (CurrentContext.GetInstance().LoginData != null &&
                !string.IsNullOrEmpty(CurrentContext.GetInstance().LoginData.Login))
                email = CurrentContext.GetInstance().LoginData.Login;
            else
            {
                string regEmail = string.Empty;
                RegistryProcessor.GetFromRegistry(CommonConst.REGISTRY_PATH,
                    CommonConst.EMAIL_VALUE_NAME, ref regEmail, RegistryProcessor.RegistryParts.HKEY_CURRENT_USER);
                if (!string.IsNullOrEmpty(regEmail))
                    email = regEmail;
            }

            string currVersion = "1.00";
            if (CurrentContext.GetInstance().VersionData != null)
                currVersion = CurrentContext.GetInstance().VersionData.CurrentVersion.ToString();

            string token = CommonConst.BUG_REPORT_TOKEN;

            string logFile = LogController.GetInstance().IsLogFileExists();

            NameValueCollection postParams = new NameValueCollection();
            postParams.Add("from", email);
            postParams.Add("body", report);
            postParams.Add("tracker_version", currVersion);
            postParams.Add("token", token);
            if (isCrashReport)
                postParams.Add("crash_report", "1");

            List<string> logFilePath = new List<string>();
            logFilePath.Add(logFile);
            string resp = WebProcessor.UploadFileWithParams(url, 
                SessionController.GetInstance().ServerApiProvider.
                PrepareFilesData(logFilePath, "activity_log", "text/plain"), postParams);

            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNavigationLine("Server reply: " + resp));

            return resp;
        }
    }
}
