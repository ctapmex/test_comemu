using System.Windows.Forms;
using Remote4Support.Utils;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class FrmMain : Form
    {
        internal DockPanel DockPanel { get; }
        private SingletonToolWindowHelper<SessionTreeView> sessions;
        private SingletonToolWindowHelper<SessionDetail> sessionDetail;

        public FrmMain()
        {
            InitializeComponent();
            DockPanel = dockPanel1;

            sessions = new SingletonToolWindowHelper<SessionTreeView>("Sessions", DockPanel, null, x => new SessionTreeView(x.DockPanel));
            sessionDetail = new SingletonToolWindowHelper<SessionDetail>("Session Detail", DockPanel, sessions,
                x => new SessionDetail(x.InitializerResource as SingletonToolWindowHelper<SessionTreeView>));
            sessions.ShowWindow(DockState.DockRight);
            sessionDetail.ShowWindow(DockState.DockRightAutoHide);
        }
    }
}