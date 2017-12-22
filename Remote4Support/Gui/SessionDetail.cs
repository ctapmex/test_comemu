
using Remote4Support.Utils;

namespace Remote4Support.Gui
{
    public partial class SessionDetail : ToolWindow
    {
        private SessionTreeView _treeViewInstance;
        private SingletonToolWindowHelper<SessionTreeView> _sessionsToolWindowHelper;

        public SessionDetail()
        {
            InitializeComponent();
            _treeViewInstance = null;
        }

        public SessionDetail(SingletonToolWindowHelper<SessionTreeView> sessions) : this()
        {
            _sessionsToolWindowHelper = sessions;
            if (_sessionsToolWindowHelper != null)
            {
                // We need to know when an instance of the SessionTreeView is created
                // so that we can register for the SelectionChanged event.
            }
        }

    }
}
