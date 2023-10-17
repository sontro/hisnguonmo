using System.Collections.Generic;

namespace MOS.SDO
{
    public class ServiceReqDetailSDO
    {
        public long? DummyId { get; set; } //id "fake" backend tao ra de xu ly nghiep vu gan thong tin "dich vu dinh kem"
        public long? AttachedDummyId { get; set; } //id "fake" cua dich vu cha ma dich vu nay kem theo
        public long PatientTypeId { get; set; }
        public long ServiceId { get; set; }
        public long? ParentId { get; set; }
        public long? RoomId { get; set; }
        public long? EkipId { get; set; }
        public decimal Amount { get; set; }
        public short? IsExpend { get; set; } //hao phi hay khong
        public short? IsOutParentFee { get; set; }//chi phi ngoai goi dich vu
        public long? ShareCount { get; set; } //so luong BN chia se cung 1 dich vu
        public bool IsNoHeinDifference { get; set; } //co tinh tien chenh lech doi voi dich vu BHYT hay khong
        public string InstructionNote { get; set; } //luu y cua bac sy khi chi dinh
        public long? SereServId { get; set; }
        public long? PrimaryPatientTypeId { get; set; }
        public decimal? UserPrice { get; set; }
        public decimal? UserPackagePrice { get; set; }
        public long? PackageId { get; set; }
        public string AssignedExecuteLoginName { get; set; }
        public string AssignedExecuteUserName { get; set; }
        public long? ServiceConditionId { get; set; }
        public long? OtherPaySourceId { get; set; }
        public long? NumOrderBlockId { get; set; }
        public long? NumOrderIssueId { get; set; }
        public long? NumOrder { get; set; }
        public long? BedStartTime { get; set; }
        public long? BedFinishTime { get; set; }
        public long? BedId { get; set; }
        public bool IsNotUseBhyt { get; set; }
        public string SampleTypeCode { get; set; }  //Mã loại mẫu xét nghiệm
        
        public List<EkipSDO> EkipInfos { get; set; }
        
        public ServiceReqDetailSDO()
        {
        }

        public ServiceReqDetailSDO(long patientTypeId, long serviceId, long? parentId, long? roomId, decimal amount, short? isExpend, short? isOutParentFee, long? ekipId, long? shareCount, bool isNoHeinDifference, string instructionNote, long? primaryPatientTypeId, decimal? userPrice, decimal? userPackagePrice, long? packageId, string assignedExecuteLoginName, string assignedExecuteUserName, long? serviceConditionId, long? otherPaySourceId, long? numOrderBlockId, long? numOrderIssueId, long? numOrder, long? bedStartTime, long? bedFinishTime, long? bedId, List<EkipSDO> ekipInfos)
        {
            this.PatientTypeId = patientTypeId;
            this.ServiceId = serviceId;
            this.ParentId = parentId;
            this.RoomId = roomId;
            this.Amount = amount;
            this.IsExpend = isExpend;
            this.IsOutParentFee = isOutParentFee;
            this.EkipId = ekipId;
            this.ShareCount = shareCount;
            this.IsNoHeinDifference = isNoHeinDifference;
            this.InstructionNote = instructionNote;
            this.PrimaryPatientTypeId = primaryPatientTypeId;
            this.UserPrice = userPrice;
            this.UserPackagePrice = userPackagePrice;
            this.PackageId = packageId;
            this.AssignedExecuteLoginName = assignedExecuteLoginName;
            this.AssignedExecuteUserName = assignedExecuteUserName;
            this.ServiceConditionId = serviceConditionId;
            this.OtherPaySourceId = otherPaySourceId;
            this.NumOrder = numOrder;
            this.NumOrderBlockId = numOrderBlockId;
            this.NumOrderIssueId = numOrderIssueId;
            this.BedFinishTime = bedFinishTime;
            this.BedStartTime = bedStartTime;
            this.BedId = bedId;
            this.EkipInfos = ekipInfos;
        }
    }
}
