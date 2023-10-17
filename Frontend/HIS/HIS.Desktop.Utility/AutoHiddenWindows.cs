using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Utility
{
    internal class AutoHiddenWindows
    {
        internal const int SW_MAXIMIZE = 3;
        internal const int SW_MINIMIZE = 6;
        // more here: http://www.pinvoke.net/default.aspx/user32.showwindow

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}
