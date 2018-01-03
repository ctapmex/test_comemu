using System;
using System.Collections.Generic;
using System.Windows.Forms;
using log4net;
using Remote4Support.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class FrmMain : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal DockPanel DockPanel { get; }
        private SingletonToolWindowHelper<SessionTreeView> sessions;
        private SingletonToolWindowHelper<SessionDetail> sessionDetail;

        private Dictionary<String, DockPanel> panel_groups = new Dictionary<String, DockPanel>();

        public FrmMain()
        {
            InitializeComponent();
            DockPanel = dockPanel1;

            sessions = new SingletonToolWindowHelper<SessionTreeView>("Sessions", DockPanel, null, x => new SessionTreeView(x.DockPanel));
            sessionDetail = new SingletonToolWindowHelper<SessionDetail>("Session Detail", DockPanel, sessions,
                x => new SessionDetail(x.InitializerResource as SingletonToolWindowHelper<SessionTreeView>));
            sessions.ShowWindow(DockState.DockRight);
            sessionDetail.ShowWindow(DockState.DockRightAutoHide);


            /*foreach (KeyValuePair<string, SessionBase> session in Remote4Support.sessionsMap)
            {
                DockPanel p1 = AddGroupDockPanel(session.Value.SessionGroup);
                ToolWindow panel1 = PanelFactory.createPanel(session.Value);
                panel1.Show(p1);
                ApplyDockRestrictions(panel1);
            }*/

        }

        private DockPanel AddGroupDockPanel(string name)
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

                panel_groups.Add(name,dp);
                result = dp;
            }

            return result;
        }

        private static void ApplyDockRestrictions(DockContent panel)
        {
            panel.DockAreas = DockAreas.Document | DockAreas.DockBottom | DockAreas.DockLeft | DockAreas.DockRight |
                              DockAreas.DockTop;
        }

        private void menuBarToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked = !mi.Checked;
            toolStrip1.Visible = mi.Checked;
        }

        private void statusBarToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem mi = (ToolStripMenuItem)sender;
            mi.Checked = !mi.Checked;
            statusStrip1.Visible = mi.Checked;
        }

     }
}