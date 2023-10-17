using HIS.Desktop.Plugins.SurgServiceReqExecute.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Validate.ValidationRule
{
    class FinishTimeValidationRule :
DevExpress.XtraEditors.DXErrorProvider.ValidationRule
    {
        internal DevExpress.XtraEditors.DateEdit startTime;
        internal DevExpress.XtraEditors.DateEdit finishTime;
        internal long treatmentOutTime;
        internal long instructionTime;
        internal bool keyCheck;//#19893
        internal bool keyCheckStatsTime;//#20201

        public override bool Validate(Control control, object value)
        {
            bool valid = true;
            try
            {
                if (finishTime.EditValue == null)
                    return valid;
                List<string> errMess = new List<string>();
                long timeFinish = finishTime.EditValue != null ? Inventec.Common.TypeConvert.Parse.ToInt64(finishTime.DateTime.ToString("yyyyMMddHHmm") + "00") : 0;
                if (startTime.EditValue != null && finishTime.EditValue != null && startTime.DateTime > finishTime.DateTime)
                {
                    errMess.Add(ResourceMessage.ThoiGianKetThucKhongDuocNhoHonThoiGianBatDau);
                    valid = false;
                }
                //#20113
                if (!keyCheckStatsTime)
                {
                    if (finishTime.EditValue != null && finishTime.DateTime > DateTime.Now)
                    {
                        errMess.Add(ResourceMessage.ThoiGianKetThucKhongDuocLonHonThoiGianHeThong);
                        valid = false;
                    }
                }
                if (!keyCheck)
                    if (timeFinish < instructionTime)
                    {
                        // errMess.Add(String.Format(ResourceMessage.ThoiGianKetThucThoiGianVaoVien));
                        errMess.Add(String.Format(ResourceMessage.ThoiGianKetThucKhongDuocNhoHonThoiGianYLenh));
                        valid = false;
                    }
                if (treatmentOutTime > 0 && timeFinish > treatmentOutTime)
                {
                    errMess.Add(String.Format(ResourceMessage.ThoiGianKetThucThoiGianRaVien));
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
