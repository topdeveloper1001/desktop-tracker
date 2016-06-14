using EliteWork_Desktop_Tracker.Controllers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
//using System.Linq;
using System.Windows.Forms;

namespace EliteWork_Desktop_Tracker
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern IntPtr OpenThread(uint dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        static extern bool TerminateThread(IntPtr hThread, uint dwExitCode);

        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0].Equals("crash_mode"))
            {
                //bool result;
                //var mutex = new System.Threading.Mutex(true, "7BB8BBB0-432E-45e5-9443-EB6827ABAF69", out result);

                SetDefaultCulture(new CultureInfo("en-US"));

                Application.Run(new CrashReportAlert());

                //GC.KeepAlive(mutex);
            }
            else
            {
                bool result;
                var mutex = new System.Threading.Mutex(true, "7BB8BBB0-432E-45e5-9443-EB6827ABAF69", out result);

                if (!result)
                    return;

                SetDefaultCulture(new CultureInfo("en-US"));

                Application.ThreadException += new
                        ThreadExceptionEventHandler(UIThreadException);

                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

                AppDomain.CurrentDomain.UnhandledException += new
                     UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                Application.Run(new LoginForm());
                GC.KeepAlive(mutex);
            }            
        }

        public static void SetDefaultCulture(CultureInfo culture)
        {
            Type type = typeof(CultureInfo);

            try
            {
                type.InvokeMember("s_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("s_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }

            try
            {
                type.InvokeMember("m_userDefaultCulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });

                type.InvokeMember("m_userDefaultUICulture",
                                    BindingFlags.SetField | BindingFlags.NonPublic | BindingFlags.Static,
                                    null,
                                    culture,
                                    new object[] { culture });
            }
            catch { }
        }

        private static void CurrentDomain_UnhandledException(Object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = (Exception)e.ExceptionObject;
                LogException(ex);
            }
            catch
            {
                try
                {
                    LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.
                                            GetNavigationLine("CD UNHANDLED EXCEPTION: " +
                                            "Fatal exception happend inside UnhadledExceptionHandler"));
                    RunCrashApp();
                    Process.GetCurrentProcess().Kill();
                }
                finally
                { }
            }
        }

        private static void UIThreadException(object sender, ThreadExceptionEventArgs t)
        {
            try
            {
                Exception ex = t.Exception;
                LogException(ex);
            }
            catch
            {
                try
                {
                    LogController.GetInstance().LogData(LogController.
                                            GetInstance().LogFormat.
                                            GetNavigationLine("UIT UNHANDLED EXCEPTION: " +
                                            "Fatal exception happend inside UnhadledExceptionHandler"));
                    RunCrashApp();
                    Process.GetCurrentProcess().Kill();
                }
                finally
                { }
            }
        }

        private static void LogException(Exception ex)
        {
            LogController.GetInstance().LogData(LogController.GetInstance().
                LogFormat.GetCrashReportLine((ex.Message + ex.StackTrace).Replace(System.Environment.NewLine, string.Empty)));

            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            foreach (ProcessThread pt in currentThreads)
            {
                IntPtr ptrThread = OpenThread(1, false, (uint)pt.Id);
                if (AppDomain.GetCurrentThreadId() != pt.Id)
                {
                    try { TerminateThread(ptrThread, 1); }
                    catch { }
                }
            }

            RunCrashApp();
            Process.GetCurrentProcess().Kill();
        }

        private static void RunCrashApp()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            string dir = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            startInfo.FileName = string.Format("{0}\\EliteWork Desktop Tracker.exe", dir);
            startInfo.Arguments = "crash_mode";
            Process.Start(startInfo);
        }
    }
}
