using HIS.Desktop.Plugins.TransactionBillOther.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionBillOther.Validation
{
    class GoodDiscountValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinGoodDiscount;
        internal DevExpress.XtraEditors.SpinEdit spinGoodAmount;
        internal DevExpress.XtraEditors.SpinEdit spinGoodPrice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinGoodDiscount == null)
                    return valid;
                if (spinGoodDiscount.Value > (spinGoodAmount.Value * spinGoodPrice.Value))
                {
                    ErrorText = String.Format(ResourceMessageManager.KhongDuocLonHon, ResourceMessageManager.ChietKhau, ResourceMessageManager.ThanhTien);
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
