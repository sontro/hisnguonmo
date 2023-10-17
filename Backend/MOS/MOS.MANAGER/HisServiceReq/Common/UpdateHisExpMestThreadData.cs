using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceReq.Common
{
    class UpdateHisExpMestThreadData
    {
        public List<HIS_EXP_MEST> expMests { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> expMestMedicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> expMestMaterials { get; set; }
        public UpdateHisExpMestThreadData()
        {
        }
    }
}
