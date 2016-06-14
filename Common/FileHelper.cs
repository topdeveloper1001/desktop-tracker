using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Common
{
    public class FileHelper
    {
        public static string GetLogFilePath()
        {
            return string.Format("{0}\\{1}", CommonConst.LOG_FILE_PATH, CommonConst.LOG_FILE_NAME);
        }
    }
}
