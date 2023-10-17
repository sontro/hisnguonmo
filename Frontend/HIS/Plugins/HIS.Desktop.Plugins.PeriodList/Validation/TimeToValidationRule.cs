using HIS.Desktop.LibraryMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PeriodList.Validation
{
    class TimeToValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtTimeFrom;
        internal DevExpress.XtraEditors.DateEdit dtTimeTo;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtTimeFrom == null || dtTimeTo == null) return valid;
                if (dtTimeFrom.EditValue != null && dtTimeTo.EditValue != null)
                {
                    if (dtTimeTo.DateTime <= dtTimeFrom.DateTime)
                    {
                        //ErrorText = MessageUtil.GetMessage(Message.Enum.HeThongThongBaoThoiGianDenBeHonThoiGianTu);
                        ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
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
