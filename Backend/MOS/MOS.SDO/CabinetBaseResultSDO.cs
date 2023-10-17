using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class CabinetBaseResultSDO
    {
        public HIS_EXP_MEST ExpMest { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> ExpMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> ExpMaterials { get; set; }

        public HIS_IMP_MEST ImpMest { get; set; }
        public List<HIS_IMP_MEST_MEDICINE> ImpMedicines { get; set; }
        public List<HIS_IMP_MEST_MATERIAL> ImpMaterials { get; set; }
    }
}
