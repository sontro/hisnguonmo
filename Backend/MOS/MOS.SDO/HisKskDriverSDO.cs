using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisKskDriverSDO
    {
        public long? Id { get; set; }

        public long ServiceReqId { get; set; }
        public string KskDriverCode { get; set; }
        public long ConclusionTime { get; set; }
        public string Conclusion { get; set; }
        public string LicenseClass { get; set; }
        public string ConcluderLoginname { get; set; }
        public string ConcluderUsername { get; set; }
        public string ReasonBadHealthy { get; set; }
        public string SickCondition { get; set; }
        public decimal? Concentration { get; set; }
        public long? ConcentrationType { get; set; }
        public long? DrugType { get; set; }
        public long? AppointmentTime { get; set; }

        public string CmndNumber { get; set; }
        public long CmndDate { get; set; }
        public string CmndPlace { get; set; }

        public bool IsAutoSync { get; set; }
        public KskSyncSDO SyncData { get; set; }
    }
}
