using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class TimingHelper
    {
        public static string GetCurrentTime()
        {
            return DateTime.Now.ToLongTimeString();
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy.MM.dd");
        }

        public static DateTime GetDateTimeFromString(string date)
        {
            return DateTime.ParseExact(date, "yyyy.MM.dd",
                                       System.Globalization.CultureInfo.InvariantCulture);
        }

        public static bool MoreThanInterval(DateTime startData, DateTime endData, int interval)
        {
            TimeSpan span = endData.Subtract(startData);
            return span.Days > interval;
        }

        public static int GetCurrentTimestamp()
        {
            //return DateTime.Now.Millisecond;
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
        
        public static int CalcTimestampDelta(int earliestTimestamp, int latestTimestamp)
        {
            return (latestTimestamp - earliestTimestamp);
        }
    }
}
