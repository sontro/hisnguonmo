using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestManuSDO
    {
        public HIS_IMP_MEST ImpMest { get; set; }
        public List<HisMedicineWithPatySDO> ManuMedicines { get; set; }
        public List<HisMaterialWithPatySDO> ManuMaterials { get; set; }
        public List<HIS_BLOOD> ManuBloods { get; set; }
        
    }
}
