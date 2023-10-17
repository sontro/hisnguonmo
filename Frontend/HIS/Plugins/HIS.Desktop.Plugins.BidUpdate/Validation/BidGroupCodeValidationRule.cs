using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BidUpdate.Validation
{
    class BidGroupCodeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBidGroupCode;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
           
            try
            {
                if (txtBidGroupCode == null) return valid;

                //if (string.IsNullOrEmpty(txtBidGroupCode.Text))
                //{
                //    this.ErrorText = "Trường dữ liệu bắt buộc";
                //    return valid;
                //}

                if (!string.IsNullOrEmpty(txtBidGroupCode.Text) && Encoding.UTF8.GetByteCount(txtBidGroupCode.Text.Trim()) > 4)
                {
                    this.ErrorText = "Trường dữ liệu nhập quá giới hạn";
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
