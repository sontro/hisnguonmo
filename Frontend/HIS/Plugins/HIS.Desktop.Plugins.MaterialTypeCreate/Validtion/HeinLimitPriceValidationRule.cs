using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialTypeCreate.Validtion
{
    class HeinLimitPriceValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitPrice;
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitRatio;
        internal DevExpress.XtraEditors.TextEdit txtHeinLimitRatioOld;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtHeinLimitPrice == null && txtHeinLimitRatio == null && txtHeinLimitRatioOld == null)
                    return valid;
                if ((!String.IsNullOrEmpty(txtHeinLimitPrice.Text)) && (!String.IsNullOrEmpty(txtHeinLimitRatio.Text) || !String.IsNullOrEmpty(txtHeinLimitRatioOld.Text)))
                {
                    ErrorText = "Chỉ cho phép nhập giá hoặc trần tỉ lệ";
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
