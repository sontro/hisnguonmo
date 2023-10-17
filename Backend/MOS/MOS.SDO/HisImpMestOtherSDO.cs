using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestOtherSDO
    {
        public HIS_IMP_MEST ImpMest { get; set; }
        public List<HisMedicineWithPatySDO> OtherMedicines { get; set; }
        public List<HisMaterialWithPatySDO> OtherMaterials { get; set; }
        public List<HIS_BLOOD> OtherBloods { get; set; }

        public HisImpMestOtherSDO()
        {
        }

        public HisImpMestOtherSDO(HIS_IMP_MEST impMest, List<HisMedicineWithPatySDO> medicines, List<HisMaterialWithPatySDO> materials, List<HIS_BLOOD> bloods)
        {
            this.ImpMest = impMest;
            this.OtherMedicines = medicines;
            this.OtherMaterials = materials;
            this.OtherBloods = bloods;
        }
    }
}
