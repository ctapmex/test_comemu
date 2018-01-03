
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using log4net;

namespace Remote4Support.Data
{
    public static class SessionUtils
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SaveSessionsToFile(List<SessionBase> sessions, string fileName)
        {
            Log.InfoFormat("Saving {0} sessions to {1}", sessions.Count, fileName);

            sessions.Sort();
            XmlSerializer s = new XmlSerializer(sessions.GetType(), new Type[] { typeof(ConsoleSession), typeof(PuttySession) });
            using (TextWriter w = new StreamWriter(fileName))
            {
                s.Serialize(w, sessions);
            }
        }

        public static List<SessionBase> LoadSessionsFromFile(string fileName)
        {
            List<SessionBase> sessions = new List<SessionBase>();
            if (File.Exists(fileName))
            {
                XmlSerializer s = new XmlSerializer(sessions.GetType(), new Type[] { typeof(ConsoleSession), typeof(PuttySession) });
                using (TextReader r = new StreamReader(fileName))
                {
                    sessions = (List<SessionBase>)s.Deserialize(r);
                }
                Log.InfoFormat("Loaded {0} sessions from {1}", sessions.Count, fileName);
            }
            else
            {
                Log.WarnFormat("Could not load sessions, file doesn't exist.  file={0}", fileName);
            }
            return sessions;
        }
    }
}
