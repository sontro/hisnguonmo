using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentBedRoomList.Validation
{
    class TimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtAddTime;
        internal DevExpress.XtraEditors.DateEdit dtRemoveTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtAddTime == null || dtRemoveTime == null)
                    return valid;
                if (dtAddTime.EditValue == null || dtAddTime.DateTime == DateTime.MinValue)
                {
                    ErrorText = MessageUtil.GetMessage(LibraryMessage.Message.Enum.TruongDuLieuBatBuoc);
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return false;
                }
                if (dtRemoveTime.EditValue != null && dtRemoveTime.DateTime != DateTime.MinValue && dtAddTime.DateTime > dtRemoveTime.DateTime)
                {
                    ErrorText = "Thời gian vào không được lớn hơn thời gian ra";
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return false;
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
