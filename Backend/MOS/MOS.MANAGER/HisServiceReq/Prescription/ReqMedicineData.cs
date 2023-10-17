using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Prescription
{
    class ReqMedicineData : PresMedicineSDO
    {
        public int Id { get; set; } //định danh dữ liệu
        public long InstructionTime { get; set; }
        public long ExpMestId { get; set; }
        public long ServiceReqId { get; set; }
        public long TreatmentId { get; set; }

        public ReqMedicineData(PresMedicineSDO sdo, long expMestId, long serviceReqId, long treatmentId, long instructionTime, int id)
            : base (sdo)
        {
            this.ExpMestId = expMestId;
            this.InstructionTime = instructionTime;
            this.Id = id;
            this.ServiceReqId = serviceReqId;
            this.TreatmentId = treatmentId;
        }
    }
}
