using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using log4net;
using Remote4Support.Data;
using Remote4Support.Gui;
using Remote4Support.Properties;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support
{
    public static class Remote4Support
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static DockPanel DockPanel { get; set; }

        static SortedList<string, SessionBase> sessionsMap = new SortedList<string, SessionBase>();
        private static Dictionary<String, DockPanel> panel_groups = new Dictionary<String, DockPanel>();

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

        public static List<SessionBase> GetAllSessions()
        {
            return sessionsMap.Values.ToList();
        }

        private static DockPanel AddGroupDockPanel(string name)
        {
            if (!panel_groups.TryGetValue(name, out var result))
            {
                DockPanel dp = new DockPanel
                {
                    Text = name,
                    DocumentStyle = DocumentStyle.DockingWindow,
                    Dock = DockStyle.Fill
                };

                DockContent dc = new DockContent
                {
                    TabText = name,
                    ShowInTaskbar = false,
                    DockAreas = DockAreas.Document | DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop
                };

                dc.Show(DockPanel);
                dc.Controls.Add(dp);

                panel_groups.Add(name, dp);
                result = dp;
            }

            return result;
        }

        private static void ApplyDockRestrictions(DockContent panel)
        {
            panel.DockAreas = DockAreas.Document | DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight |
                              DockAreas.DockTop;
        }

        internal static Settings Settings => Settings.Default;
        private static string SessionsFileName => Path.Combine(Settings.SettingsFolder, "Sessions.XML");

        public static void OpenSession(SessionBase session)
        {
            DockPanel p1 = AddGroupDockPanel(session.SessionGroup);
            ToolWindow panel1 = PanelFactory.createPanel(session);
            panel1.Show(p1);
            ApplyDockRestrictions(panel1);
        }
    }
}
