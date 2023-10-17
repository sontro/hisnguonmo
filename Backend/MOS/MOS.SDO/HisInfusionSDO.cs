using MOS.EFMODEL.DataModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisInfusionSDO
    {
        public HIS_INFUSION HisInfusion { get; set; }
        public List<HIS_MIXED_MEDICINE> HisMixedMedicines { get; set; }
    }
}
