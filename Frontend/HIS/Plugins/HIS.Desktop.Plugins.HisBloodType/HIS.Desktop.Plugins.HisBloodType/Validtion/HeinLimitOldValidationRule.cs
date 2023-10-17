using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisBloodType.Validtion
{
    class HeinLimitOldValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitPriceOld;
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitPrice;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtHeinLimitPriceOld == null || txtHeinLimitPrice == null)
                    return valid;
                if (!String.IsNullOrEmpty(txtHeinLimitPriceOld.Text) && !String.IsNullOrEmpty(txtHeinLimitPrice.Text))
                {
                    ErrorText = "Chỉ nhập giá hoặc trần tỉ lệ";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                else
                {
                    valid = true;
                }

            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
