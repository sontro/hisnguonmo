using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestUserSDO
    {
        public long ImpMestId { get; set; }
        public List<HIS_IMP_MEST_USER> ImpMestUsers { get; set; }
    }
}
