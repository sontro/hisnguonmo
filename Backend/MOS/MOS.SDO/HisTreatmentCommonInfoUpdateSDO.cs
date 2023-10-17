using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentCommonInfoUpdateSDO
    {
        public long Id { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string TraditionalIcdCode { get; set; }
        public string TraditionalIcdName { get; set; }
        public string IcdCauseCode { get; set; }
        public string IcdCauseName { get; set; }
        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public string TraditionalIcdSubCode { get; set; }
        public string TraditionalIcdText { get; set; }
        public long InTime { get; set; }
        public long? OutTime { get; set; }
        public long? ClinicalInTime { get; set; }
        public bool IsNotCheckLhmp { get; set; }
        public bool IsNotCheckLhsp { get; set; }
        public decimal? TreatmentDayCount { get; set; }
        public long? TreatmentOrder { get; set; }
        public short? NeedSickLeaveCert { get; set; }
        public long? FundId { get; set; }
        public decimal? FundBudget { get; set; }
        public string FundCompanyName { get; set; }
        public string FundCustomerName { get; set; }
        public long? FundFromTime { get; set; }
        public long? FundIssueTime { get; set; }
        public string FundNumber { get; set; }
        public long? FundPayTime { get; set; }
        public long? FundToTime { get; set; }
        public string FundTypeName { get; set; }
        public long? OweTypeId { get; set; }
        public string InCode { get; set; }
        public string EndCode { get; set; }
        public long? OtherPaySourceId { get; set; }
        public bool UpdateOtherPaySourceIdForSereServ { get; set; }
        public string DoctorLoginName { get; set; }
        public string DoctorUserName { get; set; }
        public bool IsEmergency { get; set; }
        public string InIcdCode { get; set; }
        public string InIcdName { get; set; }
        public string InIcdSubCode { get; set; }
        public string InIcdText { get; set; }


    }
}
