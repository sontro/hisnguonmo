using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPriorityType.Validtion
{
    class ValidBhytPrefixs : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.MemoEdit memoEdit;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (memoEdit == null)
                {
                    return valid;
                }

                if (!String.IsNullOrWhiteSpace(memoEdit.Text) && !Regex.IsMatch(memoEdit.Text, @"^[a-zA-Z0-9,]*$"))
                {
                    this.ErrorText = "Chỉ nhập ký tự, số và dấu phẩy (,)";
                    this.ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                if (!String.IsNullOrWhiteSpace(memoEdit.Text) && Encoding.UTF8.GetByteCount(memoEdit.Text) > 4000)
                {
                    this.ErrorText = "Vượt quá độ dài cho phép (" + 4000 + ")";
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
