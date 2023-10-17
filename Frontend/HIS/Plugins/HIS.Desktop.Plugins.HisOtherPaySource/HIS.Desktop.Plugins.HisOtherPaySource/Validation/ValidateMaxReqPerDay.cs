using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.HisOtherPaySource.Validtion
{
    public class ValidateMaxReqPerDay : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinEdit == null) return valid;

                if (spinEdit.EditValue != null)
                {
                    decimal valueEdit = spinEdit.Value;
                    if (valueEdit < 0 || valueEdit != (int)valueEdit)
                    {
                        this.ErrorText = "Trường dữ liệu phải là số nguyên dương";
                        this.ErrorType = ErrorType.Warning;
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
