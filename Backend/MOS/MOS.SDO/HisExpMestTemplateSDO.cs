using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisExpMestTemplateSDO
    {
        public HIS_EXP_MEST_TEMPLATE ExpMestTemplate { get; set; }
        public List<HIS_EMTE_MEDICINE_TYPE> EmteMedicineTypes { get; set; }
        public List<HIS_EMTE_MATERIAL_TYPE> EmteMaterialTypes { get; set; }

        public HisExpMestTemplateSDO()
        {
        }

        public HisExpMestTemplateSDO(HIS_EXP_MEST_TEMPLATE expMestTemplate,
            List<HIS_EMTE_MEDICINE_TYPE> emteMedicineTypes,
            List<HIS_EMTE_MATERIAL_TYPE> emteMaterialTypes)
        {
            this.ExpMestTemplate = expMestTemplate;
            this.EmteMedicineTypes = emteMedicineTypes;
            this.EmteMaterialTypes = emteMaterialTypes;
        }
    }
}
