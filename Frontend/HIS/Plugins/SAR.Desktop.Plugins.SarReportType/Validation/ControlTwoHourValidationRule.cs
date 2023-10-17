using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportType.Validation
{
    class ControlTwoHourValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal GridLookUpEdit HourFrom;
        internal GridLookUpEdit HourTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (HourFrom == null) return valid;
                if (HourTo == null) return valid;
                if (HourFrom.EditValue != null && HourTo.EditValue != null)
                {
                    if (long.Parse(HourFrom.EditValue.ToString()) > long.Parse(HourTo.EditValue.ToString()))
                    {
                        return valid;
                    }
                }
                else if (HourFrom.EditValue != null && HourTo.EditValue == null)
                {
                    return valid;
                }
                else if (HourFrom.EditValue == null && HourTo.EditValue != null)
                {
                    return valid;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
