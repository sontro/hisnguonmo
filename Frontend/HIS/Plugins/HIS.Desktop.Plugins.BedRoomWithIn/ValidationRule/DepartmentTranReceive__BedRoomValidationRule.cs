using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.BedRoomWithIn.ValidationRule
{
    class DepartmentTranReceive__BedRoomValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBedRoomCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboBedRoom;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBedRoomCode == null || cboBedRoom == null) return valid;

                if ((String.IsNullOrEmpty(txtBedRoomCode.Text) || cboBedRoom.EditValue == null))
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
