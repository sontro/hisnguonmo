using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialUpdate.Validation
{
    class BidPackageValidate : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBidPackage;
        internal bool check;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBidPackage == null) return valid;

                if (check)
                {
                    if (string.IsNullOrEmpty(txtBidPackage.Text))
                    {
                        this.ErrorText = "Trường dữ liệu bắt buộc";
                        return valid;
                    }
                }

                if (!string.IsNullOrEmpty(txtBidPackage.Text) && txtBidPackage.Text.Length != 2)
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc nhập 2 ký tự";
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
