using System;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using log4net;
using log4net.Config;
using Remote4Support.Gui;

namespace Remote4Support
{
    static class Program
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo("config/log4net.config"));
            Log.Info("Starting");
            
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            if (Environment.OSVersion.Version.Major >= 6) SetProcessDPIAware();

            Remote4Support.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());

            Remote4Support.Shutdown();
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            String msg = String.Format("CurrentDomain_UnhandledException: IsTerminating={0}, ex={1}", e.IsTerminating, e.ExceptionObject);
            MessageBox.Show(msg, "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Log.Error(msg);
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (e.Exception.Message.Contains("Could not load file or assembly 'System.Core, Version=4.5.0.0"))
            {
                sb.Append("Remote4Support requires the Microsoft .NET Framework version 4.5, or greater, in order to run.\n\nPlease contact your System Administrator for more information.");
            }
            else
            {
                sb.Append(e.Exception);
            }

            Log.Error("Application_ThreadException", e.Exception);
            MessageBox.Show(sb.ToString(), "Application_ThreadException", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /*for DPI*/
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool SetProcessDPIAware();
    }
}
