using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.CacheStrategy
{
    interface ICacheStrategy
    {
        bool PostOldSessions(string currentSessionId);
        bool PostCurrentSession(string currentSessionId);
        void SetServerApiProvider(IServerApiProvider apiProvider);
    }
}
