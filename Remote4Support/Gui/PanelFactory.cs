using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remote4Support.Data;

namespace Remote4Support.Gui
{
    public static class PanelFactory
    {
        public static ToolWindow createPanel(SessionBase session)
        {
            ToolWindow panel = null;
            switch (session.SessionType)
            {
                case SessionTypes.Console:
                    panel = new PanelConsole((ConsoleSession) session);
                    break;
                case SessionTypes.Putty:
                    panel = new PanelPutty((PuttySession) session);
                    break;
            }

            return panel;
        }
    }
}
