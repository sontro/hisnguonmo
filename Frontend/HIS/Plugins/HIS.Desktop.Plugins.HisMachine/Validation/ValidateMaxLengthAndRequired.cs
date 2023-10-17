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
    class ValidateMaxLengthAndRequired : ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl textEdit;
        internal int maxLength;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (textEdit == null) return valid;
                if(String.IsNullOrEmpty(textEdit.Text.Trim()))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (!String.IsNullOrEmpty(textEdit.Text.Trim()) && Inventec.Common.String.CountVi.Count(textEdit.Text.Trim()) > maxLength)
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
