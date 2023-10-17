using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.HisAppointmentPeriod.Validation
{
    class ValidaSpinMinute : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinHours;
        internal DevExpress.XtraEditors.SpinEdit spinMinute;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinHours.EditValue != null && spinMinute.EditValue == null)
                {
                    this.ErrorText = "trường dữ liệu bắt buộc nhập";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
