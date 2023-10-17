using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImpMestCreate.Validation
{
    class BidValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBid;
        internal bool IsValidate;
        internal int maxLength;
        internal bool isBidPackage;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBid == null) return true;
                if (IsValidate && string.IsNullOrEmpty(txtBid.Text)) return valid;

                if (!isBidPackage)
                {
                    if (!string.IsNullOrEmpty(txtBid.Text.Trim()) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtBid.Text.Trim(), maxLength))
                    {
                        this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + maxLength + " kí tự)";
                        return valid;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtBid.Text.Trim()) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtBid.Text.Trim(), txtBid.Properties.MaxLength))
                    {
                        this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + txtBid.Properties.MaxLength + " kí tự)";
                        return valid;
                    }
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
