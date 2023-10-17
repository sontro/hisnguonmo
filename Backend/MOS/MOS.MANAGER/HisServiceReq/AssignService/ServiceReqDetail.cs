using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService
{
    class ServiceReqDetail : ServiceReqDetailSDO
    {
        public string ServiceName { get; set; }
        //Ma phieu in dinh kem
        public string AttachAssignPrintTypeCode { get; set; }
        //Ko cho phep 2 dv co "loai y lenh" khac nhau nam trong cung 1 y lenh
        public long ServiceReqTypeId { get; set; }
        //Ko cho phep 2 dv co "module xu ly" khac nhau nam trong cung 1 y lenh
        public long? ExeServiceModuleId { get; set; }
        //Ko cho phep 2 dv (1 loai la "khang sinh do", 1 loai ko phai la "khang sinh do" nam trong cung 1 y lenh)
        public bool IsAntibioticResistance { get; set; }
        //Cho phep chi dinh gia
        public bool IsEnableAssignPrice { get; set; }
        //Xu ly tach y lenh
        public bool IsSplitServiceReq { get; set; }
        //Xu ly tach dich vu
        public bool IsSplitService { get; set; }
        //Xu ly tach dich vu khong bat buoc hoan thanh
        public bool IsNotRequiredComplete { get; set; }
        //Xu ly tach dich vu theo loai dich vu
        public bool IsAutoSplitReq { get; set; }
        //Được phép gửi sang hệ thống PACS
        public bool AllowSendPacs { get; set; }

        public ServiceReqDetail()
        {
        }

        public ServiceReqDetail(ServiceReqDetailSDO sdo) :
            base(sdo.PatientTypeId, sdo.ServiceId, sdo.ParentId, sdo.RoomId, sdo.Amount, sdo.IsExpend, sdo.IsOutParentFee, sdo.EkipId, sdo.ShareCount, sdo.IsNoHeinDifference, sdo.InstructionNote, sdo.PrimaryPatientTypeId, sdo.UserPrice, sdo.UserPackagePrice, sdo.PackageId, sdo.AssignedExecuteLoginName, sdo.AssignedExecuteUserName, sdo.ServiceConditionId, sdo.OtherPaySourceId, sdo.NumOrderBlockId, sdo.NumOrderIssueId, sdo.NumOrder, sdo.BedStartTime, sdo.BedFinishTime, sdo.BedId, sdo.EkipInfos)
        {
            this.DummyId = sdo.DummyId;
            this.AttachedDummyId = sdo.AttachedDummyId;
            this.SampleTypeCode = sdo.SampleTypeCode;
        }
    }
}
