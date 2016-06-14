using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Model.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Factories
{
    class LoginDataFactory
    {
        public static ILoginData CreateLoginData()
        {
            ILoginData loginData = new DefaultLoginData(); // SIMPLEST VARIANT NOW
            return loginData;
        }
    }
}
