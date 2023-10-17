using Inventec.Common.Logging;
using MOS.MANAGER.Config;
using MOS.EFMODEL.DataModels;
using MOS.LibraryEventLog;
using MOS.UTILITY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.EventLogUtil
{
    class AssignRationData
    {
        public string TreatmentCode { get; set; }
        public string ServiceReqCode { get; set; }
        public HIS_RATION_TIME RationTime { get; set; }
        public List<HIS_SERE_SERV_RATION> SereServRations { get; set; }
        public short? IsForHomie { get; set; }
        public bool IsHalfInFirstDay { get; set; }
        public short? IsForAutoCreateRation { get; set; }

        public AssignRationData()
        {
        }

        public AssignRationData(string treatmentCode, string serviceReqCode, HIS_RATION_TIME rationTime, List<HIS_SERE_SERV_RATION> ssRations, short? isForHomie, bool isHalfInFirstDay, short? isForAutoCreateRation)
        {
            this.TreatmentCode = treatmentCode;
            this.ServiceReqCode = serviceReqCode;
            this.RationTime = rationTime;
            this.SereServRations = ssRations;
            this.IsForHomie = isForHomie;
            this.IsHalfInFirstDay = isHalfInFirstDay;
            this.IsForAutoCreateRation = isForAutoCreateRation;
        }

        public override string ToString()
        {
            List<string> rationInfos = new List<string>();
            if (SereServRations != null)
            {
                foreach (var ss in SereServRations)
                {
                    V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ss.SERVICE_ID);
                    if (sv != null)
                    {
                        rationInfos.Add(string.Format("{0}({1})", sv.SERVICE_NAME, sv.SERVICE_CODE));
                    }
                }
            }

            string treatCode = string.IsNullOrWhiteSpace(this.TreatmentCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.TREATMENT_CODE, this.TreatmentCode);
            string reqCode = string.IsNullOrWhiteSpace(this.ServiceReqCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.SERVICE_REQ_CODE, this.ServiceReqCode);
            string rationName = this.RationTime != null ? RationTime.RATION_TIME_NAME : "";
            string Homie = this.IsForHomie == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.HisServiceReq_NguoiNha) : "";
            string HalfInFirstDay = this.IsHalfInFirstDay ? LogCommonUtil.GetEventLogContent(EventLog.Enum.HisServiceReq_AnTuchieu) : "";
            string AutoCreateRation = this.IsForAutoCreateRation == Constant.IS_TRUE ? LogCommonUtil.GetEventLogContent(EventLog.Enum.HisServiceReq_BaoAnTuDong) : "";

            return string.Format(
                "{0}. {1} {2} - {3} - {4}, {5}, {6}",
                treatCode,
                reqCode,
                rationName,
                string.Join("; ", rationInfos),
                Homie,
                HalfInFirstDay,
                AutoCreateRation
                );
        }
    }
}
