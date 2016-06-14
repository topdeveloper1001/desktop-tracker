using EliteWork_Desktop_Tracker.Model;
using EWWebProcessor.FileUploader.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.ServerApi
{
    interface IServerApiProvider
    {
        string CreateLoginUrl(string login, string password);
        LoginState AnalizeLoginSuccess(string response);
        string CreatePostSessionUrl();
        string CreateVersionUrl(string email, string curVer);
        Dictionary<double, int> AnalizeVersionResponse(string resp);
        NameValueCollection CreatePostSessionParams(ISession session, ILoginData loginData);
        NameValueCollection CreatePostSessionParams(List<string> loadedData);
        IEnumerable<UploadFile> PrepareFilesData(List<string> filePaths, string fileName, string contentType);
        bool AnalizePostSessionResponse(string response);
        IList<string> PrepareDataForSave(ISession session, ILoginData loginData);
        NameValueCollection AppendPostSessionParams(List<string> loadedData, ISession session, ILoginData loginData);
        string CreateServerLogsUrl();
        string CreateBugReportUrl();
    }
}
