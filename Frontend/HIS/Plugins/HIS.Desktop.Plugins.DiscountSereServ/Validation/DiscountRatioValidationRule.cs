using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ.Validation
{
    class DiscountRatioValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit txtDiscountRatio;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDiscountRatio == null) return valid;
                if (txtDiscountRatio.Value < 0)
                {
                    ErrorText = Base.ResourceMessageLang.PhanTramChietKhauBeHonKhong;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (txtDiscountRatio.Value > 100)
                {
                    ErrorText = Base.ResourceMessageLang.PhanTramChietKhauLonHonMoTram;
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
