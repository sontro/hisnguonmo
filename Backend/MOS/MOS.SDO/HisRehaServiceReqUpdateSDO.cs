using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisRehaServiceReqUpdateSDO
    {
        public long ServiceReqId { get; set; }
        public string IcdText { get; set; }
        public string IcdName { get; set; }
        public string IcdCode { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdCauseName { get; set; }
        public string IcdSubCode { get; set; }
        public string SymptomBefore { get; set; }
        public string SymptomAfter { get; set; }
        public string RespiratoryBefore { get; set; }
        public string RespiratoryAfter { get; set; }
        public string EcgBefore { get; set; }
        public string EcgAfter { get; set; }
        public string Advise { get; set; }
    }
}
