using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class SubclinicalPresSDO : PrescriptionSDO
    {
        public string ClientSessionKey { get; set; }
        public long InstructionTime { get; set; }
        public long? ExpMestReasonId { get; set; }
    }
}
