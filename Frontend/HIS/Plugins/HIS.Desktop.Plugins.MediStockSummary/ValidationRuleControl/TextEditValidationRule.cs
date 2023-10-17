using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors.DXErrorProvider;

namespace HIS.Desktop.Plugins.MediStockSummary
{
    class TextEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit txt;
        internal long maxLength;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (String.IsNullOrEmpty(txt.Text))
                {
                    this.ErrorText = "Bạn chưa nhập lý do khóa";
                    this.ErrorType = ErrorType.Warning;
                    return valid;
                }
                if (!String.IsNullOrEmpty(txt.Text) && Encoding.UTF8.GetByteCount(txt.Text) > maxLength)
                {
                    this.ErrorText = "Trường dữ liệu vượt quá độ dài cho phép " + maxLength + " ký tự";
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
