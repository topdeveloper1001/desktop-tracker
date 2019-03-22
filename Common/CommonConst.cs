using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public class CommonConst
    {
        public static int SESSION_INTERVAL = 5 * 60 * 1000;
        public static int DELAY = 20 * 1000;
        public static int INACTIVE_INTERVAL = 6;
        public static int INTERNET_CONNECTION_TIMEOUT = 15 * 1000;
        public static int SYSTEM_SLEEP_TIMER_INTERVAL = 90 * 60 * 1000;

        public static string COMMON_EW_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string LOCAL_CACHE_PATH = string.Format("{0}\\EW\\cache", COMMON_EW_PATH);
        public static string LOG_FILE_PATH = string.Format("{0}\\EW\\log", COMMON_EW_PATH);
        public static string LOG_FILE_NAME = "log.txt";
        public static string DES_KEY = "s43a2x89";
        public static string DECR_IMG_EXT = ".jpg";
        public static string ENCR_IMG_EXT = ".ien";
        public static string ENCR_DATA_EXT = ".den";
        public static string DECR_DATA_EXT = ".txt";
        public static string SESSION_DATA_FILE = "data";
        public static double CURRENT_VERSION = 3.00;

        public static string REGISTRY_PATH = @"Software\EW\Account";
        public static string EMAIL_VALUE_NAME = "Email";
        public static string PWD_VALUE_NAME = "Pwd"; // AHMED EDIT
        public static string MINIMIZE_TO_TRAY = "MinimizeToSystemTray";
        public static string DISABLE_TRAY_NOTIFICATION = "ShowSystemTrayNotifications";
        public static string BUG_REPORT_TOKEN = "AsTtr65221$aa";

        public static string EW_API_LOGIN_URL = "http://members.elitework.com/controllers/api/tracker_login.php";
        public static string EW_API_POST_SESSION_URL = "http://members.elitework.com/controllers/api/postsession.php"; // email=?&password=?
        public static string EW_API_CURRENT_VERSION = "http://members.elitework.com/controllers/api/version_check.php";
        public static string EW_API_VIEWLOG = "http://members.elitework.com/controllers/api/view_logs.php";
        public static string EW_API_REPORT_URL = "http://members.elitework.com/controllers/api/bug_report.php";

        public static int MAX_SCREENSHOT_WIDTH = 400; // in px

        public static int BALLOON_DELAY = 1000;
        public static int LOG_INTERVAL = 30;
        public static int CHECK_CONNECTION_INTERVAL = 15;
    }
}
