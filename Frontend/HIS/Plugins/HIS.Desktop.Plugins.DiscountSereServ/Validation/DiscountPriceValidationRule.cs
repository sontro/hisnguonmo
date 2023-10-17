using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DiscountSereServ.Validation
{
    class DiscountPriceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit txtDiscountPrice;
        internal decimal virTotalPrice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtDiscountPrice == null) return valid;
                if (txtDiscountPrice.Value < 0)
                {
                    ErrorText = Base.ResourceMessageLang.SoTienChietKhauBeHonKhong;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (txtDiscountPrice.Value > virTotalPrice)
                {
                    ErrorText = Base.ResourceMessageLang.SoTienChietKhauLonHonSoTienBenhNhanPhaiTra;
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
