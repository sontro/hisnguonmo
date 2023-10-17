using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedHistory.ADO
{
    public class HisBedHistoryADO : V_HIS_BED_LOG
    {
        public bool IsChecked { get; set; }
        public DateTime startTime { get; set; }
        public DateTime? finishTime { get; set; }

        public bool IsSave { get; set; }//luu du lieu them moi/ sua du lieu
        public int Action { get; set; }
        public string BED_SERVICE_TYPE_CODE { get; set; }
        public long BED_CODE_ID { get; set; }

        public long? BILL_PATIENT_TYPE_ID { get; set; }

        public bool Error { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeBedId { get; set; }
        public string ErrorMessageBedId { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeFinishTime { get; set; }
        public string ErrorMessageFinishTime { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeStartTime { get; set; }
        public string ErrorMessageStartTime { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypeBebServiceTypeId { get; set; }
        public string ErrorMessageBebServiceTypeId { get; set; }

        public string ErrorMessagePrimaryPatientTypeId { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypePrimaryPatientTypeId { get; set; }
        public string ErrorMessagePatientTypeId { get; set; }
        public DevExpress.XtraEditors.DXErrorProvider.ErrorType ErrorTypePatientTypeId { get; set; }

        public bool HasDefaultPatientTypeId { get; set; }
        public bool HasServiceReq { get; set; }

        public HisBedHistoryADO()
        {
            this.startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }

        public HisBedHistoryADO(V_HIS_BED_LOG data, int action, bool isSave, List<HIS_SERVICE_REQ> listServiceReq)
        {
            if (data != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<HisBedHistoryADO>(this, data);
                this.startTime = (DateTime)Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.START_TIME);
                if (data.FINISH_TIME != null)
                {
                    this.finishTime = (DateTime)Inventec.Common.DateTime.Convert.TimeNumberToSystemDateTime(data.FINISH_TIME ?? 0);
                }
                else
                {
                    this.finishTime = null;
                }
                this.BED_CODE_ID = data.BED_ID;

                if (this.BED_SERVICE_TYPE_ID.HasValue)
                {
                    var bedType = HIS.Desktop.LocalStorage.BackendData.BackendDataWorker.Get<V_HIS_SERVICE>().FirstOrDefault(o => o.ID == this.BED_SERVICE_TYPE_ID.Value);
                    this.BED_SERVICE_TYPE_CODE = bedType != null ? bedType.SERVICE_CODE : "";
                }
                else
                    this.BED_SERVICE_TYPE_CODE = null;

                if (data.SERVICE_REQ_ID.HasValue || (listServiceReq != null && listServiceReq.Exists(o => o.BED_LOG_ID == data.ID)))
                {
                    this.HasServiceReq = true;
                }
            }
            else
            {
                this.startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            }
            this.Action = action;
            this.IsSave = isSave;
        }
    }
}
