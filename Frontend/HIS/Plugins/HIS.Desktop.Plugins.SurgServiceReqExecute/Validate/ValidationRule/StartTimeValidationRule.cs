using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
    class StartTimeValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit startTime;
        internal DevExpress.XtraEditors.DateEdit finishTime;
        internal long instructionTime;
        internal long treatmentOutTime;
        internal bool keyCheck;//#19893
        internal bool keyCheckStatsTime;//#20201

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (startTime.EditValue == null)
                {
                    this.ErrorText = ResourceMessage.TruongDuLieuBatBuoc; //Inventec.Desktop.Common.LibraryMessage.MessageUtil.GetMessage(Inventec.Desktop.Common.LibraryMessage.Message.Enum.HeThongTBTruongDuLieuBatBuocPhaiNhap);
                    return false;
                }
                List<string> errMess = new List<string>();
                long timeStart = startTime.EditValue != null ? Inventec.Common.TypeConvert.Parse.ToInt64(startTime.DateTime.ToString("yyyyMMddHHmm") + "00") : 0;
                if (!keyCheck)
                    if (timeStart < instructionTime)
                    {
                        errMess.Add(ResourceMessage.ThoiGianBatDauPhaiLonHonThoiGianYLenh);
                        valid = false;
                    }
                if (finishTime.EditValue != null && startTime.DateTime > finishTime.DateTime)
                {
                    errMess.Add(ResourceMessage.ThoiGianBatDauKhongDuocLonHonThoiGianKetThuc);
                    valid = false;
                }
                //#20113
                if (!keyCheckStatsTime)
                {
                    if (startTime.EditValue != null && startTime.DateTime > DateTime.Now)
                    {
                        errMess.Add(ResourceMessage.ThoiGianKetThucKhongDuocLonHonThoiGianHeThong);
                        valid = false;
                    }
                }

                if (treatmentOutTime > 0 && timeStart > treatmentOutTime)
                {
                    errMess.Add(String.Format(ResourceMessage.ThoiGianBatDauThoiGianRaVien));
                    valid = false;
                }

                if (!valid)
                {
                    this.ErrorText = String.Join(";", errMess);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            return valid;
        }
    }
}
