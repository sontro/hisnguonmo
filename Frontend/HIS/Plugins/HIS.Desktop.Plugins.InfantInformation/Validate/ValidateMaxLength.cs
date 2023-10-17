using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.InfantInformation.Validation
{
    class ValidateMaxlength : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl textEdit;
        internal int maxLength;
        internal bool isValid;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (textEdit == null) return valid;
                if(isValid && String.IsNullOrEmpty(textEdit.Text))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá bắt buộc";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (!String.IsNullOrEmpty(textEdit.Text) && Inventec.Common.String.CountVi.Count(textEdit.Text) > maxLength)
                {
                    this.ErrorText = "Trường dữ liệu vượt quá " + maxLength + " ký tự";
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
