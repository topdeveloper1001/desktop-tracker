using Common;
using EliteWork_Desktop_Tracker.Context;
using EliteWork_Desktop_Tracker.Controllers.ServerApi;
using EliteWork_Desktop_Tracker.Controllers.ServerApi.Impl;
using EliteWork_Desktop_Tracker.Factories;
using EliteWork_Desktop_Tracker.Model;
using EliteWork_Desktop_Tracker.Model.Impl.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace EliteWork_Desktop_Tracker.Controllers
{
    class LogController
    {
        private static LogController _Controller = null;
        private List<ILogHandler> _LogHandlers = null;
        private ILogData _FileLog = null;
        private ILogData _InMemoryLog = null;
        private ILogFormatProvider _LogFormatProvider = null;
        public ILogFormatProvider LogFormat { get { return _LogFormatProvider; } }

        private LogController()
        {
            _LogFormatProvider = new DefaultLogFormatProvider();
            _FileLog = LogFactory.GetLogData(LogDataType.FILE);
            _InMemoryLog = LogFactory.GetLogData(LogDataType.IN_MEMORY);
            LoadLogData();
        }

        public static LogController GetInstance()
        {
            if (_Controller == null)
                _Controller = new LogController();

            return _Controller;
        }

        public ILogData GetLogData(LogDataType logType)
        {
            if (logType == LogDataType.FILE)
                return _FileLog;
            else if (logType == LogDataType.IN_MEMORY)
                return _InMemoryLog;
            else
                return null;
        }

        public string IsLogFileExists()
        {
            if (!Directory.Exists(CommonConst.LOG_FILE_PATH))
                return string.Empty;

            string logFilePath = FileHelper.GetLogFilePath();
            if (!File.Exists(logFilePath))
                return string.Empty;
            else
                return logFilePath;
        }

        public string ReadFileLog()
        {
            if (!Directory.Exists(CommonConst.LOG_FILE_PATH))
                return null;

            string logFilePath = FileHelper.GetLogFilePath();
            if (!File.Exists(logFilePath))
                return null;

            string fullLog = string.Empty;
            string[] logData = File.ReadAllLines(logFilePath);
            foreach (string line in logData)
                fullLog += string.Format("{0}{1}", line, Environment.NewLine);

            return fullLog;
        }

        private void LoadLogData()
        {
            if (!Directory.Exists(CommonConst.LOG_FILE_PATH))
                return;

            string logFilePath = FileHelper.GetLogFilePath();
            if (!File.Exists(logFilePath))
                return;

            while (CurrentContext.GetInstance().IsLogDataLocked)
                Thread.Sleep(1000);

            CurrentContext.GetInstance().IsLogDataLocked = true;
            string[] logData = File.ReadAllLines(logFilePath);
            CurrentContext.GetInstance().IsLogDataLocked = false;

            List<string> actualData = new List<string>();
            foreach (string line in logData)
            {
                if (IsActualLog(line))
                {
                    actualData.Add(line);
                    _InMemoryLog.WriteLine(line);
                }
            }

            if (actualData != null && actualData.Count > 0 && actualData.Count != logData.Length)
                (_FileLog as FileLogData).OverrideData(actualData.ToArray());
        }

        private bool IsActualLog(string line)
        {
            try
            {
                string logDate = line.Split('-')[0].Substring(1).Trim();
                return !TimingHelper.MoreThanInterval(TimingHelper.
                    GetDateTimeFromString(logDate), DateTime.Now, CommonConst.LOG_INTERVAL);
            }
            catch
            {
                return false;
            }
        }

        public void SetLogHandler(ILogHandler logHandler)
        {
            if (_LogHandlers == null)
                _LogHandlers = new List<ILogHandler>();

            _LogHandlers.Add(logHandler);
            if (_InMemoryLog != null)
                logHandler.AppendLine(_InMemoryLog.GetText());
        }

        public void RemoveLogHandler(ILogHandler logHandler)
        {
            if (_LogHandlers == null || _LogHandlers.Count <= 0)
                return;

            ILogHandler handler = null;
            for (int i = _LogHandlers.Count - 1; i >= 0; i--)
            {
                handler = _LogHandlers[i];
                if (handler.Equals(logHandler))
                {
                    _LogHandlers.Remove(handler);
                    break;
                }
            }

            /*foreach(ILogHandler handler in _LogHandlers)
            {
                if (handler.Equals(logHandler))
                {
                    _LogHandlers.Remove(handler);
                    break;
                }
            }*/
        }

        /*public void LogMultilineData(List<string> lines)
        {
            string finalLine = string.Empty;
            foreach (string line in lines)
                finalLine += line + Environment.NewLine;

            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                _FileLog.WriteText(finalLine);
            }));

            _InMemoryLog.WriteText(finalLine);

            if (_LogHandlers != null)
            {
                foreach (ILogHandler handler in _LogHandlers)
                {
                    if (handler != null)
                    {
                        handler.AppendLine(finalLine);
                    }
                }
            }
        }*/

        public void LogData(string line)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback((s) =>
            {
                _FileLog.WriteLine(line);
            }));
            _InMemoryLog.WriteLine(line);

            if (_LogHandlers != null)
            {
                foreach (ILogHandler handler in _LogHandlers)
                {
                    if (handler != null)
                        handler.AppendLine(line);
                }
            }
        }
    }
}
