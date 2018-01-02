using System;
using System.Runtime.InteropServices;
using WeifenLuo.WinFormsUI.Docking;

namespace Remote4Support.Gui
{
    public partial class ToolWindow : DockContent
    {
        public ToolWindow()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindowEx(IntPtr hParent, IntPtr hChild, string szClass, string szWindow);
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        internal void TermPanel_Resize(object sender, EventArgs e)
        {
            IntPtr hConEmu = FindWindowEx(Handle, (IntPtr)0, null, null);
            if (hConEmu != (IntPtr)0)
            {
                MoveWindow(hConEmu, 0, 0, Width, Height, true);
            }
        }
    }
}
