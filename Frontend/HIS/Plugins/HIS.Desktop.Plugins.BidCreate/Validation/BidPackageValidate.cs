using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidCreate.Validation
{
    class BidPackageValidate : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBidPackage;
        internal DevExpress.XtraTab.XtraTabControl xtraTabControl;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBidPackage == null || xtraTabControl == null) return valid;

                if (string.IsNullOrEmpty(txtBidPackage.Text))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
                    return valid;
                }
                if (!string.IsNullOrEmpty(txtBidPackage.Text) && xtraTabControl.SelectedTabPageIndex == 1 && (Encoding.UTF8.GetByteCount(txtBidPackage.Text.Trim()) > 4 || Encoding.UTF8.GetByteCount(txtBidPackage.Text.Trim()) < 2))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc nhập lớn hơn hoặc bằng 2 ký tự. Và bé hơn hoặc bằng 4 ký tự";
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
