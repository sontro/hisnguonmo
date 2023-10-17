using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalSurgery.Validate.ValidationRule
{
    class TimePlanToValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DateTime? timeFrom;
        internal DateTime? timeTo;
        internal DevExpress.XtraEditors.DateEdit timePlanFrom;
        internal DevExpress.XtraEditors.DateEdit timePlanTo;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (timePlanTo.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if (timePlanFrom.EditValue != null && timePlanTo.EditValue != null && timePlanFrom.DateTime > timePlanTo.DateTime)
                {
                    this.ErrorText = "Thời gian kế hoạch đến nhỏ hơn thời gian kế hoạch từ";
                    return valid;
                }
                if (timeFrom.HasValue && timeTo.HasValue)
                {
                    if (timePlanTo.DateTime > timeTo.Value || timePlanTo.DateTime < timeFrom.Value)
                    {
                        this.ErrorText = "Thời gian kế hoạch đến không nằm trong khoảng thời gian dự kiến của lịch";
                        return valid;
                    }
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
