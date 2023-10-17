using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineUpdate.Validation
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
                    if (string.IsNullOrEmpty(txtBidPackage.Text.Trim()))
                    {
                        this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                        return valid;
                    }
                }
                if (!string.IsNullOrEmpty(txtBidPackage.Text.Trim()) && Inventec.Common.String.CheckString.IsOverMaxLengthUTF8(txtBidPackage.Text.Trim(), 4))
                {
                    this.ErrorText = "Trường dữ liệu vượt quá maxlength( " + 4 + " kí tự)";
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
