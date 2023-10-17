using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.VaccinationExam.Validate
{
    class DateEditValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtAppointmentTime;
        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtAppointmentTime == null) return valid;
                if (String.IsNullOrEmpty(dtAppointmentTime.Text) || dtAppointmentTime.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if ((Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dtAppointmentTime.DateTime) ?? 0) < (Inventec.Common.DateTime.Get.EndDay() ?? 0))
                {
                    this.ErrorText = "Thời gian hẹn phải lớn hơn ngày hiện tại";
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
