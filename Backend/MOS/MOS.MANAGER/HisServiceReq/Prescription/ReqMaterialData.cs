using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription
{
    class ReqMaterialData : PresMaterialSDO
    {
        public int Id { get; set; } //định danh dữ liệu
        public long ExpMestId { get; set; }
        public long ServiceReqId { get; set; }
        public long TreatmentId { get; set; }
        public long InstructionTime { get; set; }

        public ReqMaterialData(PresMaterialSDO sdo, long expMestId, long serviceReqId, long treatmentId, int id, long instructionTime)
            : base(sdo.MediStockId, sdo.SereServParentId, sdo.MaterialTypeId, sdo.PatientTypeId, sdo.NumOrder, sdo.Amount, sdo.IsExpend, sdo.IsOutParentFee, sdo.MaterialBeanIds, sdo.EquipmentSetId, sdo.IsBedExpend, sdo.MaterialId, sdo.InstructionTimes, sdo.FailedAmount, sdo.Tutorial, sdo.IsNotPres, sdo.ServiceConditionId, sdo.OtherPaySourceId, sdo.PresAmount, sdo.ExceedLimitInPresReason, sdo.ExceedLimitInDayReason)
        {
            this.ExpMestId = expMestId;
            this.Id = id;
            this.ServiceReqId = serviceReqId;
            this.TreatmentId = treatmentId;
            this.InstructionTime = instructionTime;
        }

    }
}
