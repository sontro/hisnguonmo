using HIS.Desktop.Plugins.UpdateExamServiceReq.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.UpdateExamServiceReq.Validate
{
    class DateEditValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit dateEdit;
        internal long inTime;

        public override bool Validate(Control control, object value)
        {
            bool valid = false;
            try
            {
                if (dateEdit.EditValue == null)
                {
                    this.ErrorText = Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return valid;
                }

                if (dateEdit.EditValue != null && inTime > 0)
                {


                    long date = Inventec.Common.DateTime.Convert.SystemDateTimeToTimeNumber(dateEdit.DateTime).Value;
                    if (date < inTime)
                    {
                        Inventec.Common.Logging.LogSystem.Info("date < inTime ");
                        this.ErrorText = ResourceMessage.ThoiGianKhongDuocNhoHonThoiGianVaoVien;
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
