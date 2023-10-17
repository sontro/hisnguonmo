using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class SoLoValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBid;
        internal bool IsValidate;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBid == null) return true;
                if (IsValidate && string.IsNullOrEmpty(txtBid.Text)) return valid;

                if (!string.IsNullOrEmpty(txtBid.Text) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtBid.Text, 100))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + 100 + " kí tự)";
                    return valid;
                }

                valid = true;
            }
            catch (Exception ex)
            {
                valid = false;
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
