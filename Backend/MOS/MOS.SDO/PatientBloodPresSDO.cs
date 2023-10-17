using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class PatientBloodPresSDO : HisServiceReqSDO
    {
        public long InstructionTime { get; set; }
        public long MediStockId { get; set; }
        public List<HIS_EXP_MEST_BLTY_REQ> ExpMestBltyReqs { get; set; }
    }
}
