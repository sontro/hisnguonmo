using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.DXErrorProvider;
namespace HIS.Desktop.Utility.ValidateRule
{
    class CommonSpinValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spinEdit;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spinEdit == null) return valid;
                if (String.IsNullOrEmpty(spinEdit.Text))
                    return valid;
                if (spinEdit.Value < 0)
                    return valid;
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
