using Common;
using EliteWork_Desktop_Tracker.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace EliteWork_Desktop_Tracker.Model.Impl.Log
{
    class FileLogData : ILogData
    {
        private string _LogFilePath;
        public string LogFilePath { get { return _LogFilePath; } set { _LogFilePath = value; } }

        public override void WriteLine(string line)
        {
            WriteData(line, true);
        }

        public override void WriteText(string text)
        {
            WriteData(text, false);
        }

        public void OverrideData(string[] text)
        {
            if (!Directory.Exists(CommonConst.LOG_FILE_PATH))
                return;

            string logFilePath = FileHelper.GetLogFilePath();
            if (!File.Exists(logFilePath))
                return;

            while (CurrentContext.GetInstance().IsLogDataLocked)
                Thread.Sleep(1000);

            CurrentContext.GetInstance().IsLogDataLocked = true;
            File.WriteAllLines(logFilePath, text);
            CurrentContext.GetInstance().IsLogDataLocked = false;
        }

        private void WriteData(string data, bool isLine)
        {
            try
            {
                if (!Directory.Exists(CommonConst.LOG_FILE_PATH))
                    Directory.CreateDirectory(CommonConst.LOG_FILE_PATH);

                string logFilePath = FileHelper.GetLogFilePath();
                while (CurrentContext.GetInstance().IsLogDataLocked)
                    Thread.Sleep(1000);

                CurrentContext.GetInstance().IsLogDataLocked = true;
                File.AppendAllText(logFilePath, isLine ? string.Format("{0}{1}", data, Environment.NewLine) : data);
                CurrentContext.GetInstance().IsLogDataLocked = false;
            }
            catch { }
        }

        public override string GetText()
        {
            throw new NotImplementedException();
        }
    }
}
