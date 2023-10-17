using HIS.Desktop.LibraryMessage;
using HIS.Desktop.Plugins.TransactionBillOther.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.Validation
{
    class GoodDescriptionValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtGoodDescription;
        internal DevExpress.XtraEditors.SpinEdit spinGoodDiscount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtGoodDescription == null)
                    return valid;

                if (spinGoodDiscount.Value > 0 && String.IsNullOrWhiteSpace(txtGoodDescription.Text))
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (!String.IsNullOrWhiteSpace(txtGoodDescription.Text) && txtGoodDescription.Text.Length >= 500)
                {
                    ErrorText = String.Format(ResourceMessageManager.DoDaiKhongDuocVuotQua, ResourceMessageManager.LyDo, 500);
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
