using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SdaLicense
{
    public class ValidateMaxlength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit mme;
        internal int maxLength;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (mme == null) return valid;
                if (mme != null && string.IsNullOrEmpty(mme.Text.Trim()))
                {
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    this.ErrorText = "Thiếu trường dữ liệu bắt buộc";
                    return valid;
                }
                if (mme != null && !string.IsNullOrEmpty(mme.Text.Trim()) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(mme.Text.Trim(), maxLength))
                {
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    this.ErrorText = "Trường dữ liệu vượt quá ký tự cho phép, " + maxLength + " ký tự";
                    return valid;
                }
                valid = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Warn(ex);
            }
            return valid;
        }
    }
}
