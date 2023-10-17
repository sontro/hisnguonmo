using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.Prepare.Validate.ValidateRule
{
    class TimeFromValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit timeFrom;
        internal DevExpress.XtraEditors.DateEdit timeTo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (timeFrom.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if (timeTo.EditValue != null && timeFrom.DateTime > timeTo.DateTime)
                {
                    this.ErrorText = "Thời gian từ lớn hơn thời gian dự kiến đến";
                    return valid;
                }
                //if (timè.EditValue != null && startTime.DateTime > DateTime.Now)
                //{
                //    this.ErrorText = "Thời gian bắt đầu không được lớn hơn thời gian hiện tại";
                //    return valid;
                //}

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
