using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestProposeResultSDO
    {
        public List<V_HIS_IMP_MEST_PROPOSE> ImpMestProposes { get; set; }
        public List<V_HIS_IMP_MEST> HisImpMests { get; set; }
        public List<V_HIS_IMP_MEST_PAY> HisImpMestPays { get; set; }

    }
}
