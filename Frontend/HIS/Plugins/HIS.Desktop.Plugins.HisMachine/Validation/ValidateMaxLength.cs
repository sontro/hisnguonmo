using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisMachine.Validation
{
    class ValidateMaxLength : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl textEdit;
        internal int maxLength;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (textEdit == null) return valid;
                if (!String.IsNullOrEmpty(textEdit.Text.Trim()) && Inventec.Common.String.CountVi.Count(textEdit.Text.Trim()) > maxLength)
                {
                    this.ErrorText = "Vượt quá ký tự cho phép. " + maxLength + " ký tự";
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
