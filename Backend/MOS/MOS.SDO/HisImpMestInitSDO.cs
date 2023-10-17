using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestInitSDO
    {
        public HIS_IMP_MEST ImpMest { get; set; }
        public List<HisMedicineWithPatySDO> InitMedicines { get; set; }
        public List<HisMaterialWithPatySDO> InitMaterials { get; set; }
        public List<HIS_BLOOD> InitBloods { get; set; }

        public HisImpMestInitSDO()
        {
        }

        public HisImpMestInitSDO(HIS_IMP_MEST impMest, List<HisMedicineWithPatySDO> medicines, List<HisMaterialWithPatySDO> materials, List<HIS_BLOOD> bloods)
        {
            this.ImpMest = impMest;
            this.InitMedicines = medicines;
            this.InitMaterials = materials;
            this.InitBloods = bloods;
        }
    }
}
