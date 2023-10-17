using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class OutPatientPresSDO : PrescriptionSDO
    {
        public PrescriptionType PrescriptionTypeId { get; set; }
        public string ClientSessionKey { get; set; }
        public long InstructionTime { get; set; }
        public long? RemedyCount { get; set; }

        public bool IsCabinet { get; set; } //la don tu truc
        public long? DrugStoreId { get; set; } //medi_stock_id
        public long? PresGroup { get; set; } //nhom thuoc (gay nghien, huong than, ho tro, thuong)

        //Thong tin ket thuc dieu tri, trong truong hop ke don ket thuc dieu tri
        public HisTreatmentFinishSDO TreatmentFinishSDO { get; set; }

        public long? ExpMestReasonId { get; set; }
    }
}
