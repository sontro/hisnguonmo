using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionNumOrderUpdate.Validation
{
    class NumOrderValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal long oldNumOder;        
        internal DevExpress.XtraEditors.SpinEdit spinNumOrder;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinNumOrder == null) return valid;
                if (spinNumOrder.EditValue == null || spinNumOrder.Value <= 0)
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc); ;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (((long)spinNumOrder.Value) == oldNumOder)
                {
                    ErrorText = "Bạn Chưa thay đổi số biên lai (hóa đơn)";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
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
