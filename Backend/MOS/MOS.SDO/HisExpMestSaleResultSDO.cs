using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisExpMestSaleResultSDO
    {
        public V_HIS_EXP_MEST ExpMest { get; set; }
        public List<V_HIS_EXP_MEST_MEDICINE> ExpMedicines { get; set; }
        public List<V_HIS_EXP_MEST_MATERIAL> ExpMaterials { get; set; }
    }
}