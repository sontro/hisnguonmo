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
    class UpdateRationData
    {
        public string TreatmentCode { get; set; }
        public string ServiceReqCode { get; set; }
        public HIS_RATION_TIME RationTime { get; set; }
        public List<HIS_SERE_SERV_RATION> CurrentSereServRations { get; set; }
        public List<HIS_SERE_SERV_RATION> OldSereServRations { get; set; }

        public UpdateRationData()
        {
        }

        public UpdateRationData(string treatmentCode, string serviceReqCode, HIS_RATION_TIME rationTime, List<HIS_SERE_SERV_RATION> currentSSRations, List<HIS_SERE_SERV_RATION> oldSSRations)
        {
            this.TreatmentCode = treatmentCode;
            this.ServiceReqCode = serviceReqCode;
            this.RationTime = rationTime;
            this.CurrentSereServRations = currentSSRations;
            this.OldSereServRations = oldSSRations;
        }

        public override string ToString()
        {
            List<string> currentrationInfos = new List<string>();
            List<string> oldRationInfos = new List<string>();
            if (CurrentSereServRations != null)
            {
                foreach (var ss in CurrentSereServRations)
                {
                    V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ss.SERVICE_ID);
                    if (sv != null)
                    {
                        currentrationInfos.Add(string.Format("{0}({1})", sv.SERVICE_NAME, sv.SERVICE_CODE));
                    }
                }
            }

            if (OldSereServRations != null)
            {
                foreach (var ss in OldSereServRations)
                {
                    V_HIS_SERVICE sv = HisServiceCFG.DATA_VIEW.FirstOrDefault(o => o.ID == ss.SERVICE_ID);
                    if (sv != null)
                    {
                        oldRationInfos.Add(string.Format("{0}({1})", sv.SERVICE_NAME, sv.SERVICE_CODE));
                    }
                }
            }

            string treatCode = string.IsNullOrWhiteSpace(this.TreatmentCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.TREATMENT_CODE, this.TreatmentCode);
            string reqCode = string.IsNullOrWhiteSpace(this.ServiceReqCode) ?
                "" : string.Format("{0}: {1}", SimpleEventKey.SERVICE_REQ_CODE, this.ServiceReqCode);
            string rationName = this.RationTime != null ? RationTime.RATION_TIME_NAME : "";

            return string.Format(
                "{0}. {1} {2} - {3} ==> {4} {5} - {6}",
                treatCode,
                reqCode,
                rationName,
                string.Join("; ", oldRationInfos),
                reqCode,
                rationName,
                string.Join("; ", currentrationInfos)
                );
        }
    }
}
