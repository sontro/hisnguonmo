using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.HRM.Vietsens
{
    public class KskData
    {
        public KskData()
        {
            this.medicalTestName = "";
            this.dataMedicalResult = new List<KskDetailData>();
        }

        public string medicalTestName { get; set; }
        public List<KskDetailData> dataMedicalResult { get; set; }
    }

    public class KskDetailData
    {
        public KskDetailData()
        {
            this.employeeCode = "";
            this.medicalDate = "";
            this.medicalRank = "";
            this.medicalResult = "";
            this.bloodGroup = "";
            this.weight = "";
            this.height = "";
            this.examination_index = new List<KskExaminationData>();
        }

        public string employeeCode { get; set; }
        public string medicalDate { get; set; }
        public string medicalRank { get; set; }
        public long medicalCost { get; set; }
        public string medicalResult { get; set; }
        public string bloodGroup { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public List<KskExaminationData> examination_index { get; set; }
    }

    public class KskResultData
    {
        public string statusType { get; set; }
        public string entity { get; set; }
        public string status { get; set; }
    }

    public class KskExaminationData
    {
        public KskExaminationData()
        {
            this.service_group_type = "";
            this.service_group_type_name = "";
            this.service_group = "";
            this.service_group_name = "";
            this.service_code = "";
            this.service_name = "";
            this.service_result = "";
            this.service_unit = "";
            this.service_min = "";
            this.service_max = "";
        }

        public string service_group_type { get; set; }
        public string service_group_type_name { get; set; }
        public string service_group { get; set; }
        public string service_group_name { get; set; }
        public string service_code { get; set; }
        public string service_name { get; set; }
        public string service_result { get; set; }
        public string service_unit { get; set; }
        public string service_min { get; set; }
        public string service_max { get; set; }
    }
}
