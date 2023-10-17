using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPriorityType.Validtion
{
    class ValidAge : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinAgeFrom;
        internal DevExpress.XtraEditors.SpinEdit spinAgeTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinAgeFrom == null || spinAgeTo == null)
                {
                    return valid;
                }

                if (spinAgeFrom.EditValue != null && spinAgeTo.EditValue != null)
                {
                    if (spinAgeFrom.Value > spinAgeTo.Value)
                    {
                        this.ErrorText = "Không được phép nhập \"Tuổi đến\" bé hơn \"Tuổi từ\"";
                        this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                        return valid;
                    }
                    valid = true;
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
