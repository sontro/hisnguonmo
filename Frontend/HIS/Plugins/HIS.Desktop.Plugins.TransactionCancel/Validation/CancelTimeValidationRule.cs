using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TransactionCancel.Validation
{
    class CancelTimeValidationRule : DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dtCancelTime;
        internal long TransactionTime;

        public override bool Validate(System.Windows.Forms.Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dtCancelTime == null) return valid;
                if (dtCancelTime.EditValue == null || dtCancelTime.DateTime == DateTime.MinValue)
                {
                    ErrorText = Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }
                long time = Convert.ToInt64(dtCancelTime.DateTime.ToString("yyyyMMddHHmmss"));
                if (time < TransactionTime)
                {
                    ErrorText = Base.ResourceMessageLang.ThoiGianHuyGiaoDichLonHonThoiGianGiaoDich;
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
