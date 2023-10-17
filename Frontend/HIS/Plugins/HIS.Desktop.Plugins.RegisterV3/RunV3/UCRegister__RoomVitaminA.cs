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
using MOS.EFMODEL.DataModels;
using HIS.Desktop.LocalStorage.BackendData;

namespace HIS.Desktop.Plugins.RegisterV3.Run3
{
    public partial class UCRegister : UserControlBase
    {
        private void InitUcRoomVitaminA()
        {
            try
            {
                if (panelControlRoom.Controls.Count > 0)
                    panelControlRoom.Controls.RemoveAt(0);

                ucRoomVitaminA = new RunV3.UC.RoomVitaminA(focusToUCPersonHomeInfo);
                ucRoomVitaminA.Dock = DockStyle.Fill;

                panelControlRoom.Controls.Add(ucRoomVitaminA);
                ucRoomVitaminA.chkTreEm.Checked = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
        }
    }
}
