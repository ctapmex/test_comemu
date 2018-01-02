using System.Drawing;
using System.Windows.Forms;
using ConEmu.WinForms;
using Remote4Support.Data;

namespace Remote4Support.Gui
{
    public partial class PanelConsole : ToolWindow
    {
        private ConEmuControl conemu;
        private ConEmuSession con_session;

        public PanelConsole(ConsoleSession session)
        {
            InitializeComponent();

            AutoSize = true;
            Text = session.SessionName;
            Resize += new System.EventHandler(TermPanel_Resize);

            Controls.Add(conemu = new ConEmuControl() { AutoStartInfo = null, Dock = DockStyle.Fill });
            con_session = conemu.Start(new ConEmuStartInfo()
            {
                ConsoleProcessCommandLine = session.CommandLine
            });
        }

    }
}
