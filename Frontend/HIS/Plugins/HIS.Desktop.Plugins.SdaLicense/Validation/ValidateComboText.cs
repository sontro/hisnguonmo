using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SdaLicense.Validation
{
    class ValidateComboText : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txt;
        internal DevExpress.XtraEditors.GridLookUpEdit cbo;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txt == null || cbo == null) return valid;

                if ((String.IsNullOrEmpty(txt.Text) || cbo.EditValue == null))
                {
                    this.ErrorText = "Thiếu trường dữ liệu bắt buộc nhập";
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
