
using System.Windows.Forms;
using Remote4Support.Data;
using Remote4Support.Utils;

namespace Remote4Support.Gui
{
    public partial class SessionDetail : ToolWindow
    {
        /// <summary>
        /// Подключенный к данному объекту SessionTreeView
        /// </summary>
        private SessionTreeView _treeViewInstance;
        private SingletonToolWindowHelper<SessionTreeView> _sessionsToolWindowHelper;

        public SessionDetail()
        {
            InitializeComponent();
            _treeViewInstance = null;
            sessionDetailPropertyGrid.PropertySort = PropertySort.NoSort;
        }

        public SessionDetail(SingletonToolWindowHelper<SessionTreeView> sessionTreeView) : this()
        {
            _sessionsToolWindowHelper = sessionTreeView;
            if (_sessionsToolWindowHelper != null)
            {
                // We need to know when an instance of the SessionTreeView is created
                // so that we can register for the SelectionChanged event.
                _sessionsToolWindowHelper.InstanceChanged += SessionTreeViewInstanceChanged;
                SessionTreeViewInstanceChanged(_sessionsToolWindowHelper.Instance);
            }
        }

        private void SessionTreeViewInstanceChanged(SessionTreeView treeViewInstance)
        {
            if (_treeViewInstance == treeViewInstance)
                return;

            Attach(treeViewInstance);
        }

        /// <summary>
        /// подключение SessionTreeView к SessionDetails
        /// </summary>
        /// <param name="sessionTreeView"></param>
        private void Attach(SessionTreeView sessionTreeView)
        {
            Detach();
            _treeViewInstance = sessionTreeView;
            if (sessionTreeView != null)
            {
                _treeViewInstance.FormClosed += SessionTreeView_FormClosed;
                sessionTreeView.SelectionChanged += SelectedSessionChanged;
                SelectedSessionChanged(sessionTreeView.SelectedSession);
            }
        }

        /// <summary>
        /// отвязывание SessionTreeView от SessionDetails
        /// </summary>
        private void Detach()
        {
            if (_treeViewInstance != null)
            {
                _treeViewInstance.FormClosed -= SessionTreeView_FormClosed;
                _treeViewInstance.SelectionChanged -= SelectedSessionChanged;
                _treeViewInstance = null;
            }
            SelectedSessionChanged(null);
        }

        /// <summary>
        /// Закрытие формы SessionTreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SessionTreeView_FormClosed(object sender, FormClosedEventArgs e)
        {
            Detach();
        }

        /// <summary>
        ///  Смены выделенного элемента в списке сессий в SessionTreeView
        /// </summary>
        /// <param name="session"></param>
        private void SelectedSessionChanged(SessionBase session)
        {
            SessionBase oldSession = sessionDetailPropertyGrid.SelectedObject as SessionBase;
            if (oldSession != null)
            {
                
            }
            sessionDetailPropertyGrid.SelectedObject = session;
            if (session != null)
            {
                
            }
        }

        /// <summary>
        /// Закрытие текущей формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SessionDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            Detach();
            if (_sessionsToolWindowHelper != null)
            {
                _sessionsToolWindowHelper.InstanceChanged -= SessionTreeViewInstanceChanged;
            }
        }
    }
}
