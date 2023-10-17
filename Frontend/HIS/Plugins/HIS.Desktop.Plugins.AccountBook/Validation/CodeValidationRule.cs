using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAccountBookList.Validation
{
    class CodeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtAccountBookCode;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool vaild = false;
            try
            {
                var chars = ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789").ToCharArray();

                if (txtAccountBookCode == null) return vaild;
                if (string.IsNullOrEmpty(txtAccountBookCode.Text) || txtAccountBookCode.Text.Length > 6 || txtAccountBookCode.Text.ToCharArray().Any(c => !chars.Contains(c))) return vaild;
                vaild = true;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return vaild;
        }
    }
}
