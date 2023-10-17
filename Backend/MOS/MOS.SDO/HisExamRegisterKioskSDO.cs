using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExamRegisterKioskSDO
    {
        public HisCardSDO CardSDO { get; set; }
        public long PatientTypeId { get; set; }
        public long ServiceId { get; set; }
        public long RoomId { get; set; }
        public long RequestRoomId { get; set; }
        public string RightRouteTypeCode { get; set; }
        public string TransferInMediOrgCode { get; set; }
        public string TransferInMediOrgName { get; set; }
        public string TransferInCode { get; set; }
        public string TransferInIcdCode { get; set; }
        public string TransferInIcdName { get; set; }
        public bool? IsTransferIn { get; set; }
        public bool IsPriority { get; set; }
        public bool IsNotRequireFee { get; set; }
        public bool? IsChronic { get; set; }
        public string RightRouteCode { get; set; }
        public string CardServiceCode { get; set; }
        public List<ServiceReqDetailSDO> AdditionalServices { get; set; }
        public string ProvinceCode { get; set; }
        public string DistrictCode { get; set; }
        public long? PrimaryPatientTypeId { get; set; }

        public long? TransferInCmkt { get; set; }
        public long? TransferInFormId { get; set; }
        public long? TransferInReasonId { get; set; }
        public long? TransferInTimeFrom { get; set; }
        public long? TransferInTimeTo { get; set; }

    }
}
