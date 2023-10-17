using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionDebtCollect.Validtion
{
    class SpinVATValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinVAT;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinVAT == null)
                    return valid;
                if (spinVAT.EditValue != null && (spinVAT.Value < 0 || spinVAT.Value > 100))
                {
                    ErrorText = HIS.Desktop.Plugins.TransactionDebtCollect.Base.ResourceMessageLang.GiaTriTrongKhoang0Den100;
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
