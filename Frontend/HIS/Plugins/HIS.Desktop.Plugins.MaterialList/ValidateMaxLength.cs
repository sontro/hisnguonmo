using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.MaterialList
{
    class ValidateMaxLength : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit memoEdit;
        internal int? maxLength;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (memoEdit == null) return valid;

                if (String.IsNullOrEmpty(memoEdit.Text))
                {
                    this.ErrorText = "Bạn chưa nhập lý do khóa";
                    return valid;
                }

                if (!String.IsNullOrEmpty(memoEdit.Text) && Encoding.UTF8.GetByteCount(memoEdit.Text) > maxLength)
                {
                    this.ErrorText = "Nhập quá " + maxLength + "ký tự";
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
