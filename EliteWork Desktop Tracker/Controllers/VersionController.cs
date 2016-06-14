using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EliteWork_Desktop_Tracker.Factories;
using EWWebProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers
{
    class VersionController
    {
        private VersionController() { }

        private IServerApiProvider _ServerApiProvider = null;
        private static VersionController _Controller = null;

        public static VersionController GetInstance()
        {
            if (_Controller == null)
            {
                _Controller = new VersionController();
                _Controller.CreateServerApiProvider();
                _Controller.CreateVersionData();
            }
            
            return _Controller;
        }

        private void CreateVersionData()
        {
            CurrentContext.GetInstance().VersionData = VersionFactory.CreateVersion();
        }

        private void CreateServerApiProvider()
        {
            if (_ServerApiProvider == null)
                _ServerApiProvider = ServerApiFactory.CreateServerApiProvider();
        }

        public bool GetVersionData(string email)
        {
            string url = _ServerApiProvider.CreateVersionUrl(email, 
                CurrentContext.GetInstance().VersionData.CurrentVersion.ToString());

            string response = WebProcessor.MakeGetRequest(url);
            Dictionary<double, int> versionData = _ServerApiProvider.AnalizeVersionResponse(response);

            if (versionData == null)
                return false;

            CurrentContext.GetInstance().VersionData.NewestVersion = versionData.Keys.First();
            CurrentContext.GetInstance().VersionData.MandatoryUpdate = versionData.Values.First();
            return true;
        }
    }
}
