using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisExpMestResultSDO
    {
        public HIS_EXP_MEST ExpMest { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> ExpMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> ExpMaterials { get; set; }
        public List<HIS_EXP_MEST_BLOOD> ExpBloods { get; set; }
        public List<HIS_EXP_MEST_MATY_REQ> ExpMatyReqs { get; set; }
        public List<HIS_EXP_MEST_METY_REQ> ExpMetyReqs { get; set; }
        public List<HIS_EXP_MEST_BLTY_REQ> ExpBltyReqs { get; set; }
    }
}