using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SdaNotify
{
    class ValidatespMax : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.SpinEdit spMax;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (spMax == null) return valid;
                if (String.IsNullOrEmpty(spMax.Text))
                {
                    this.ErrorText = "Trường dữ liệu bắt buộc";
                    return valid;
                }
                if (spMax.Value < 1)
                {
                    this.ErrorText = HIS.Desktop.Plugins.SdaNotify.Resources.ResourceMessage.InvoiceBook_GiaTriLonHonKhong;
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
