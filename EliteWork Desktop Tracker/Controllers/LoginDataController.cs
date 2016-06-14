using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EliteWork_Desktop_Tracker.Factories;
using EliteWork_Desktop_Tracker.Model;
using EWWebProcessor;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers
{
    class LoginDataController
    {
        private IServerApiProvider _ServerApiProvider = null;
        private static LoginDataController _Controller = null;
        
        public static LoginDataController GetInstance()
        {
            if (_Controller == null)
                _Controller = new LoginDataController();

            return _Controller;
        }

        private LoginDataController() { }

        public LoginState Login(string login, string password)
        {
            if (_ServerApiProvider == null)
                _ServerApiProvider = ServerApiFactory.CreateServerApiProvider();

            CurrentContext.GetInstance().LoginData = LoginDataFactory.CreateLoginData();
            string url = _ServerApiProvider.CreateLoginUrl(login, password);
            string response = WebProcessor.MakeGetRequest(url);
            LogController.GetInstance().LogData(LogController.
                            GetInstance().LogFormat.GetNetworkLine(string.Format("server reply \"{0}\"", response)));
            LoginState result = _ServerApiProvider.AnalizeLoginSuccess(response);
            if (result == LoginState.LOGGED)
            {
                CurrentContext.GetInstance().LoginData.Login = login;
                CurrentContext.GetInstance().LoginData.Password = password;
            }
            return result;
        }
    }
}
