using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExaminationReqEdit.Validation
{
    class ExcuteRoomValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtExecuteRoomCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboExecuteRoom;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtExecuteRoomCode == null || cboExecuteRoom == null) return valid;
                if (txtExecuteRoomCode.EditValue == null || String.IsNullOrEmpty(cboExecuteRoom.Text))
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
