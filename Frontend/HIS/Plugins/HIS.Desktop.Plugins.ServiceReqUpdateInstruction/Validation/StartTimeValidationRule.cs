using HIS.Desktop.Plugins.ServiceReqUpdateInstruction.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqUpdateInstruction.Validtion
{
    class StartTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtEndTime;
        internal DevExpress.XtraEditors.DateEdit dtStartTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            long? startTime = null;
            long? endTime = null;
            try
            {
                if (dtStartTime == null || dtEndTime == null)
                    return valid;

                if (dtStartTime.EditValue != null && dtStartTime.DateTime != DateTime.MinValue)
                    startTime = Inventec.Common.TypeConvert.Parse.ToInt64(
                        Convert.ToDateTime(dtStartTime.EditValue).ToString("yyyyMMddHHmm") + "00");

                if (dtEndTime.EditValue != null && dtEndTime.DateTime != DateTime.MinValue)
                    endTime = Inventec.Common.TypeConvert.Parse.ToInt64(
                        Convert.ToDateTime(dtEndTime.EditValue).ToString("yyyyMMddHHmm") + "00");

                if (startTime > endTime)
                {
                    ErrorText = ResourceMessage.ThoiGianBatDauBeHonThoiGianKetThuc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
