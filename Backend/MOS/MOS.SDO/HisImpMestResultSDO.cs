using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestResultSDO
    {
        public V_HIS_IMP_MEST ImpMest { get; set; }
        public List<V_HIS_IMP_MEST_MEDICINE> ImpMedicines { get; set; }
        public List<V_HIS_IMP_MEST_MATERIAL> ImpMaterials { get; set; }
        public List<V_HIS_IMP_MEST_BLOOD> ImpBloods { get; set; }

        public HisImpMestResultSDO()
        {
        }

        public HisImpMestResultSDO(V_HIS_IMP_MEST impMest, List<V_HIS_IMP_MEST_MEDICINE> impMedicines, List<V_HIS_IMP_MEST_MATERIAL> impMaterials,List<V_HIS_IMP_MEST_BLOOD> impBloods)
        {
            this.ImpMest = impMest;
            this.ImpMedicines = impMedicines;
            this.ImpMaterials = impMaterials;
            this.ImpBloods = impBloods;
        }
    }
}
