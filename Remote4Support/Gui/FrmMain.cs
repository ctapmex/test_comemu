using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Remote4Support.Data;
using Remote4Support.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class FrmMain : Form
    {
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

            ConsoleSession ses1 = new ConsoleSession {SessionGroup = "west", SessionName = "app1"};
            ConsoleSession ses2 = new ConsoleSession {SessionGroup = "east", SessionName = "app2" };
            ConsoleSession ses3 = new ConsoleSession {SessionGroup = "west", SessionName = "app3" };
            ToolWindow panel1 = new PanelConsole(ses1);
            ToolWindow panel2 = new PanelConsole(ses2);
            ToolWindow panel3 = new PanelConsole(ses3);

            DockPanel p1 = AddDockPanel(ses1.SessionGroup);
            DockPanel p2 = AddDockPanel(ses2.SessionGroup);
            DockPanel p3 = AddDockPanel(ses3.SessionGroup);
            panel1.Show(p1);
            panel2.Show(p2);
            panel3.Show(p3);
        }

        internal DockPanel AddDockPanel(string name)
        {
            panel_groups.TryGetValue(name, out var result);
            if (result == null)
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
                    ShowInTaskbar = false
                };

                dc.Show(DockPanel);
                dc.Controls.Add(dp);

                panel_groups.Add(name,dp);
                result = dp;
            }

            return result;
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