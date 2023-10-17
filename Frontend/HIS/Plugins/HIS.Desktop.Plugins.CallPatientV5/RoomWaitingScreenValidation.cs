using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.CallPatientV5
{
    class RoomWaitingScreenValidation : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
         DevExpress.XtraEditors.TextEdit txtRoomCode;
         DevExpress.XtraEditors.GridLookUpEdit cboRoom;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtRoomCode == null || cboRoom == null) return valid;
                if (String.IsNullOrEmpty(txtRoomCode.Text) || cboRoom.EditValue == null)
                    return valid;
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
