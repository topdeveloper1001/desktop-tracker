using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace EWWebProcessor
{
    public class TimeoutWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = CommonConst.INTERNET_CONNECTION_TIMEOUT;
            return w;
        }
    }
}
