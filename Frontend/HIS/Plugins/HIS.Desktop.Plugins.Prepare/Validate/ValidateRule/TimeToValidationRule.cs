using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Prepare.Validate.ValidateRule
{
    class TimeToValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit timeFrom;
        internal DevExpress.XtraEditors.DateEdit timeTo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (timeTo.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if (timeFrom.EditValue != null && timeTo.EditValue != null && timeFrom.DateTime > timeTo.DateTime)
                {
                    this.ErrorText = "Thời gian đến nhỏ hơn thời gian từ";
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
