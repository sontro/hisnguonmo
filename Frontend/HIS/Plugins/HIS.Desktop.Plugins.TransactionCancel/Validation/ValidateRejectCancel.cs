using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.TransactionCancel
{
    class ValidateRejectCancel : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit memoEdit;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (memoEdit == null) return valid;

                if (String.IsNullOrEmpty(memoEdit.Text.Trim()))
                {
                    this.ErrorText = "Bắt buộc nhập \"Lý do từ chối hủy\"";
                    return valid;
                }
                else if (Encoding.UTF8.GetByteCount(memoEdit.Text) > 500)
                {
                    this.ErrorText = "Trường dữ liệu vượt quá 500 ký tự";
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
