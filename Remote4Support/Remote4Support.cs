using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;
using Remote4Support.Data;
using Remote4Support.Properties;

namespace Remote4Support
{
    public static class Remote4Support
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static SortedList<string, SessionBase> sessionsMap = new SortedList<string, SessionBase>();

        public static void Initialize()
        {
            Log.Info("Initialize ");
            LoadSessions();

            Log.Info("Initialized");
        }

        public static void Shutdown()
        {
            Log.Info("Shutting down...");
        }

        public static void SaveSessions()
        {
            Log.InfoFormat("Saving all sessions");
            SessionUtils.SaveSessionsToFile(sessionsMap.Values.ToList(), SessionsFileName);
        }

        public static void LoadSessions()
        {
            string fileName = SessionsFileName;
            Log.InfoFormat("Loading all sessions.  file={0}", fileName);

            try
            {
                if (File.Exists(fileName))
                {
                    List<SessionBase> sessions = SessionUtils.LoadSessionsFromFile(fileName);
                    // remove old
                    sessionsMap.Clear();

                    foreach (SessionBase session in sessions)
                    {
                        sessionsMap.Add(session.SessionId, session);
                    }
                }
                else
                {
                    Log.WarnFormat("Sessions file does not exist, nothing loaded.  file={0}", fileName);
                }

            }
            catch (Exception ex)
            {
                Log.Error("Error while loading sessions from " + fileName, ex);
            }
        }

        internal static Settings Settings => Settings.Default;
        private static string SessionsFileName => Path.Combine(Settings.SettingsFolder, "Sessions.XML");
    }
}
