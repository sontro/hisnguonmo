using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleEdit.Validation
{
    class DiscountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.CheckEdit checkImpExpPrice;
        internal DevExpress.XtraEditors.SpinEdit spinExpPrice;
        internal DevExpress.XtraEditors.SpinEdit spinAmount;
        internal DevExpress.XtraEditors.SpinEdit spinDiscount;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinDiscount == null || checkImpExpPrice == null || spinExpPrice == null || spinAmount == null) return valid;
                if (checkImpExpPrice.Checked && spinDiscount.Value < 0)
                {
                    ErrorText = Base.ResourceMessageLang.SoTienChietKhauBeHonKhong;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                else if (checkImpExpPrice.Checked && spinDiscount.Value > 0)
                {
                    var totalPrice = spinAmount.Value * spinExpPrice.Value;
                    if (spinDiscount.Value > totalPrice)
                    {
                        ErrorText = Base.ResourceMessageLang.SoTienChietKhauLonHonTongThanhTien;
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
