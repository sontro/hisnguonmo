using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TreatmentLog.Validate
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl memoEdit;
        internal int? maxLength;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (memoEdit == null) return valid;
                if(!String.IsNullOrEmpty(memoEdit.Text.Trim()) && Encoding.UTF8.GetByteCount(memoEdit.Text) > maxLength)
				{
                    ErrorText = "Trường dữ liệu vượt quá ký tự cho phép, " + maxLength + " ký tự";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
