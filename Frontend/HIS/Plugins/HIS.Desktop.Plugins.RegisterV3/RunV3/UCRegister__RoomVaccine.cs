using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Inventec.Common.Controls.PopupLoader;
using Inventec.Common.Controls.EditorLoader;
using HIS.Desktop.Utility;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void InitUcRoomVaccine()
        {
            try
            {
                if (panelControlRoom.Controls.Count > 0)
                    panelControlRoom.Controls.RemoveAt(0);

                ucRoomVaccine = new RunV3.UC.RoomVaccine(focusToUCPersonHomeInfo);
                ucRoomVaccine.Dock = DockStyle.Fill;

                panelControlRoom.Controls.Add(ucRoomVaccine);
                //item.TextVisible = false;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
