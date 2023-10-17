using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineTypeCreate.Validtion
{
    class HeinLimitPriceDateTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtHeinLimitPriceIntrTime;
        internal DevExpress.XtraEditors.DateEdit dtHeinLimitPriceInTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtHeinLimitPriceIntrTime == null || dtHeinLimitPriceInTime == null)
                    return valid;
                if (!String.IsNullOrEmpty(dtHeinLimitPriceIntrTime.Text) && !String.IsNullOrEmpty(dtHeinLimitPriceInTime.Text))
                {
                    ErrorText = "Chỉ nhập thời gian theo ngày vào viện hoặc thời gian chỉ định";
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
