using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionRepay.Validation
{
    class RepayReasonValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtRepayReasonCode;
        internal DevExpress.XtraEditors.LookUpEdit cboRepayReason;
        internal long isRequred = 0;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtRepayReasonCode == null || cboRepayReason == null) return valid;
                if ((String.IsNullOrEmpty(txtRepayReasonCode.Text) || cboRepayReason.EditValue == null) && isRequred == 1)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
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
