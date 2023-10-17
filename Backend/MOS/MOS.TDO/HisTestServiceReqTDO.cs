using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTestServiceReqTDO
    {
        public string PatientCode { get; set; }
        public string PatientName { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string ServiceReqCode { get; set; }
        public string OriginalServiceReqCode { get; set; }
        public long DateOfBirth { get; set; }
        public long CreateTime { get; set; }
        public long InstructionTime { get; set; }
        public string HeinCardNumber { get; set; }
        public string IcdCode { get; set; }
        public string IcdName { get; set; }
        public string HeinMediOrgCode { get; set; }
        public string HeinMediOrgName { get; set; }
        public string RequestLoginName { get; set; }
        public string RequestUserName { get; set; }
        public string RequestRoomName { get; set; }
        public string RequestRoomCode { get; set; }
        public string RequestDepartmentName { get; set; }
        public string RequestDepartmentCode { get; set; }
        public string PatientTypeName { get; set; }
        public string PatientTypeCode { get; set; }
        public string TreatmentTypeCode { get; set; }
        public string TreatmentTypeName { get; set; }
        public string TreatmentCode { get; set; }
        public string TurnCode { get; set; }
        public string ExecuteRoomName { get; set; }
        public string ExecuteRoomCode { get; set; }
        public string ExecuteDepartmentName { get; set; }
        public string ExecuteDepartmentCode { get; set; }
        public long? SamplingNumOrder { get; set; }
        public string PhoneNumber { get; set; }
        public string TestSampleTypeCode { get; set; }
        public string TestSampleTypeName { get; set; }
        public string CmndNumber { get; set; }
        public string CccdNumber { get; set; }
        public string PassportNumber { get; set; }

        public string NationalName { get; set; }
        public long Priority { get; set; }
        public string KskContractCode { get; set; }

        public string OriginalBarcode { get; set; }
        public long? SampleTime { get; set; }
        public string SampleLoginName { get; set; }
        public string SampleUserName { get; set; }
        public long? ReceiveSampleTime { get; set; }
        public string ReceiveSampleLoginname { get; set; }
        public string ReceiveSampleUsername { get; set; }

        public string IcdSubCode { get; set; }
        public string IcdText { get; set; }
        public long? HeinCardFromTime { get; set; }
        public long? HeinCardToTime { get; set; }

        public string PaanPositionCode { get; set; }//ma vi tri sinh thiet
        public string PaanPositionName { get; set; }

        public List<HisTestServiceTypeTDO> TestServiceTypeList { get; set; }
    }
}
