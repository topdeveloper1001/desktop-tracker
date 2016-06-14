using System;
using System.Collections.Generic;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model
{
    abstract class ILoginData
    {
        private LoginState _State;
        public LoginState CurrentLoginState { get { return _State; } set { _State = value; } }

        private string _Login;
        public string Login { get { return _Login; } set { _Login = value; } }

        private string _Password;
        public string Password { get { return _Password; } set { _Password = value; } }

        private string _Username;
        public string Username { get { return _Username; } set { _Username = value; } }
    }
}
