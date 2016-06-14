using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EliteWork_Desktop_Tracker.Controllers.ServerApi.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Factories
{
    class ServerApiFactory
    {
        public static IServerApiProvider CreateServerApiProvider()
        {
            IServerApiProvider apiController = new DefaultServerApiProvider(); //SIMPLEST VARIANT NOW
            return apiController;
        } 
    }
}
