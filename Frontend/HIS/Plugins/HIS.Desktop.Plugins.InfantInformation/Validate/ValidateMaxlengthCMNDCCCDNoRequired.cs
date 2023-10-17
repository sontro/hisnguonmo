using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InfantInformation.Validate
{
    class ValidateMaxlengthCMNDCCCDNoRequired : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl textEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                //if (textEdit == null) return valid;

                if (!String.IsNullOrEmpty(textEdit.Text) && ((textEdit.Text.Length < 9) || ((textEdit.Text.Length > 9) && (textEdit.Text.Length < 12)) || (textEdit.Text.Length > 12)))
                {

                    this.ErrorText = "Dữ liệu không đúng định dạng. CMND/CCCD phải có 9 hoặc 12 ký tự";
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
