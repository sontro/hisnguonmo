using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialUpdate.Validation
{
    class BidYearValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.TextEdit txtBidYear;
        internal bool check;
        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (txtBidYear == null) return valid;

                if (check)
                {
                    if (string.IsNullOrEmpty(txtBidYear.Text))
                    {
                        this.ErrorText = HIS.Desktop.LibraryMessage.MessageUtil.GetMessage(HIS.Desktop.LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                        return valid;
                    }
                }

                if (!string.IsNullOrEmpty(txtBidYear.Text))
                {
                    var valueTxt = Inventec.Common.TypeConvert.Parse.ToInt32(txtBidYear.Text);
                    if (valueTxt < 1000)
                    {
                        this.ErrorText = "Năm không hợp lệ";
                        return valid;
                    }

                    var year = Inventec.Common.TypeConvert.Parse.ToInt32(txtBidYear.Text);
                    if (year > 0 && year > DateTime.Now.Year)
                    {
                        this.ErrorText = "Năm không được lớn hơn năm hiện tại";
                        return valid;
                    }
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
