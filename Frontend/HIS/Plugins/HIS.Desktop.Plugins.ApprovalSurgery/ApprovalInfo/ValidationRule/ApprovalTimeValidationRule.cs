using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.ApprovalSurgery.ApprovalInfo.ValidationRule
{
    class ApprovalTimeValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        public enum ACTION
        { 
            APPROVAL,
            UNAPPROVAL,
            REJECT,
            UNREJECT
        }

        internal DevExpress.XtraEditors.DateEdit time;
        public int action;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (time.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if (TrimMilliseconds(time.DateTime) < TrimMilliseconds(DateTime.Now))
                {
                    switch (action)
                    {
                        case 1:
                            this.ErrorText = "Thời gian duyệt nhỏ hơn thời gian hiện tại";
                            break;
                        case 2:
                            this.ErrorText = "Thời gian hủy duyệt nhỏ hơn thời gian hiện tại";
                            break;
                        case 3:
                            this.ErrorText = "Thời gian từ chối nhỏ hơn thời gian hiện tại";
                            break;
                        case 4:
                            this.ErrorText = "Thời gian hủy từ chối nhỏ hơn thời gian hiện tại";
                            break;
                        default:
                            break;
                    }
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

        public DateTime TrimMilliseconds(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0, dt.Kind);
        }
    }
}
