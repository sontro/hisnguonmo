using DevExpress.XtraEditors.DXErrorProvider;
using HIS.Desktop.LibraryMessage;
using HIS.Desktop.LocalStorage.BackendData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HoreHandover.Validation
{
    class ReceiveRoomValidationRule : ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtReceiveRoomCode;
        internal DevExpress.XtraEditors.GridLookUpEdit cboReceiveRoom;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (cboReceiveRoom == null || txtReceiveRoomCode == null) return valid;
                if (String.IsNullOrWhiteSpace(txtReceiveRoomCode.Text) || cboReceiveRoom.EditValue == null)
                {
                    ErrorText = MessageUtil.GetMessage(Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = ErrorType.Warning;
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                valid = false;
            }
            return valid;
        }
    }
}
