using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EliteWork_Desktop_Tracker.Model.Impl.Log
{
    class InMemoryLogData : ILogData
    {
        private StringBuilder _LogData;
        public StringBuilder LogData { get { return _LogData; } }

        public InMemoryLogData()
        {
            _LogData = new StringBuilder();
        }

        public override void WriteLine(string line)
        {
            _LogData.AppendLine(line);
        }

        public override void WriteText(string text)
        {
            _LogData.Append(text);
        }

        public override string GetText()
        {
            return LogData.ToString();
        }
    }
}
