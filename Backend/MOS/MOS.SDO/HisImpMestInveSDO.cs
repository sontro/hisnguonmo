using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisImpMestInveSDO
    {
        public HIS_IMP_MEST ImpMest { get; set; }
        public List<HisMedicineWithPatySDO> InveMedicines { get; set; }
        public List<HisMaterialWithPatySDO> InveMaterials { get; set; }
        public List<HIS_BLOOD> InveBloods { get; set; }
        
        public HisImpMestInveSDO()
        {
        }

        public HisImpMestInveSDO(HIS_IMP_MEST impMest, List<HisMedicineWithPatySDO> medicines, List<HisMaterialWithPatySDO> materials, List<HIS_BLOOD> bloods)
        {
            this.ImpMest = impMest;
            this.InveMedicines = medicines;
            this.InveMaterials = materials;
            this.InveBloods = bloods;
        }
    }
}
