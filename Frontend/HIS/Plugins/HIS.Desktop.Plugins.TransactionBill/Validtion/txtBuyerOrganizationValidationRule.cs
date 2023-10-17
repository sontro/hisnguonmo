using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBill.Validtion
{
    class txtBuyerOrganizationValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBuyerOrganization;
        internal bool isRequiredPin;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBuyerOrganization.Visible && !String.IsNullOrEmpty(txtBuyerOrganization.Text))
                {
                    if (Inventec.Common.String.CountVi.Count(txtBuyerOrganization.Text) > 500)
                    {
                        ErrorText = Base.ResourceMessageLang.DoDaiDonViVuotQua500KyTu;
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
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
