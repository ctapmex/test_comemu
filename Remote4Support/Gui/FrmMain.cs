using System.Windows.Forms;
using log4net;
using Remote4Support.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class FrmMain : Form
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SingletonToolWindowHelper<SessionTreeView> sessions;
        private SingletonToolWindowHelper<SessionDetail> sessionDetail;

        public FrmMain()
        {
            InitializeComponent();
            Remote4Support.DockPanel = dockPanel1;

            sessions = new SingletonToolWindowHelper<SessionTreeView>("Sessions", dockPanel1, null, x => new SessionTreeView(x.DockPanel));
            sessionDetail = new SingletonToolWindowHelper<SessionDetail>("Session Detail", dockPanel1, sessions,
                x => new SessionDetail(x.InitializerResource as SingletonToolWindowHelper<SessionTreeView>));
            sessions.ShowWindow(DockState.DockRight);
            sessionDetail.ShowWindow(DockState.DockRightAutoHide);
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