using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CallPatientCashier
{
    class ShowFormProcessor
    {
        internal static void ShowFormInExtendMonitor(Form control)
        {
            try
            {
                Screen[] sc;
                sc = Screen.AllScreens;
                if (sc.Length <= 1)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Không tìm thấy màn hình mở rộng");
                    control.Show();
                }
                else
                {
                    control.FormBorderStyle = FormBorderStyle.None;
                    control.Left = sc[1].Bounds.Width;
                    control.Top = sc[1].Bounds.Height;
                    control.StartPosition = FormStartPosition.Manual;
                    control.Location = sc[1].Bounds.Location;
                    Point p = new Point(sc[1].Bounds.Location.X, sc[1].Bounds.Location.Y);
                    control.Location = p;
                    control.WindowState = FormWindowState.Maximized;
                    control.Show();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
        }
    }
}
