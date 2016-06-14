using EliteWork_Desktop_Tracker.Controllers.CacheStrategy.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Controllers.CacheStrategy
{
    class CacheStrategyExecutor
    {
        private static CacheStrategyExecutor _Executor = null;
        private ICacheStrategy _Strategy = null;
        public ICacheStrategy CacheStrategy { get { return _Strategy; } set { _Strategy = value; } }

        private CacheStrategyExecutor() { }

        public static CacheStrategyExecutor GetInstance()
        {
            if (_Executor == null)
            {
                _Executor = new CacheStrategyExecutor();
                _Executor.CacheStrategy = new DefaultCacheStrategy();
            }

            return _Executor;
        }
    }
}
