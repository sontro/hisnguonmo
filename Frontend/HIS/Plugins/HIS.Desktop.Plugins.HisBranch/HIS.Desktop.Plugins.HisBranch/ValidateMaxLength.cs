using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.HisBranch
{
    class ValidateMaxLength : ValidationRule
    {
        internal TextEdit textEdit;
        internal int? maxLength;
        public override bool Validate(Control control, object value)
        {
            bool flag = false;
            bool result;
            try
            {
                if (this.textEdit == null)
                {
                    result = flag;
                    return result;
                }
                if (!string.IsNullOrEmpty(this.textEdit.Text) && Encoding.UTF8.GetByteCount(this.textEdit.Text) > this.maxLength)
                {
                    base.ErrorText = "Vượt quá độ dài cho phép (" + this.maxLength + ")";
                    base.ErrorType = ErrorType.Warning;
                    result = flag;
                    return result;
                }
                flag = true;
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            result = flag;
            return result;
        }
    }
}
