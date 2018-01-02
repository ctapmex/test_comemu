
using Remote4Support.Data;

namespace Remote4Support.Gui
{
    public partial class PanelPutty : ToolWindow
    {
        public PanelPutty(PuttySession session)
        {
            InitializeComponent();

            AutoSize = true;
            Text = session.SessionName;
            Resize += new System.EventHandler(TermPanel_Resize);
        }
    }
}
