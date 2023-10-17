using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Library.TreatmentEndTypeExt
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.BaseControl txt;
        internal int? maxLength;
        internal bool IsRequired;
        internal string tooltip;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txt == null) return valid;
                if (IsRequired && String.IsNullOrEmpty(txt.Text) && !string.IsNullOrEmpty(tooltip))
                {
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    this.ErrorText = tooltip;
                    return valid;
                }
                if (!String.IsNullOrEmpty(txt.Text) && Encoding.UTF8.GetByteCount(txt.Text) > maxLength && maxLength > 0)
                {
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    this.ErrorText = "Dữ liệu vượt quá độ dài cho phép " + maxLength + " ký tự";
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
