using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.HisAppointmentPeriod.Validate
{
    class ValidaSpinHours : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinFrom;
        internal DevExpress.XtraEditors.SpinEdit spinTo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false ;
            try
            {
                if (spinFrom.EditValue == null && spinTo.EditValue == null)
                {
                    this.ErrorText = "trường dữ liệu bắt buộc nhập";
                    this.ErrorType = ErrorType.Warning;
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
