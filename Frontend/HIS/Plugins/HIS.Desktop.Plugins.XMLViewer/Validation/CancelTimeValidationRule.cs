using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.XMLViewer.Validation
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
                if (dtCancelTime == null)
                    return valid;
                if (String.IsNullOrEmpty(dtCancelTime.Text))
                {
                    ErrorText = HIS.Desktop.Plugins.XMLViewer.Base.ResourceMessageLang.TruongDuLieuBatBuoc;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (dtCancelTime.EditValue != null && dtCancelTime.DateTime == DateTime.MinValue)
                {
                    ErrorText = HIS.Desktop.Plugins.XMLViewer.Base.ResourceMessageLang.ThoiGianHuyGiaoDichLonHonThoiGianGiaoDich;
                    ErrorType = DevExpress.XtraEditors.DXErrorProvider.ErrorType.Warning;
                    return valid;
                }

                if (dtCancelTime.EditValue != null && dtCancelTime.DateTime != DateTime.MinValue)
                {
                    long CancelTime = Inventec.Common.TypeConvert.Parse.ToInt64(
                      Convert.ToDateTime(dtCancelTime.EditValue).ToString("yyyyMMddHHmm") + "00");
                    if (CancelTime < TransactionTime)
                    {
                        ErrorText = HIS.Desktop.Plugins.XMLViewer.Base.ResourceMessageLang.ThoiGianHuyGiaoDichLonHonThoiGianGiaoDich;
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
