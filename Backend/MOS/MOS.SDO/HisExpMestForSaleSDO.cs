using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisExpMestForSaleSDO
    {
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<V_HIS_EXP_MEST> ExpMests { get; set; }
        public List<HIS_SERVICE_REQ_METY> ServiceReqMetys { get; set; }
        public List<HIS_SERVICE_REQ_MATY> ServiceReqMatys { get; set; }
        public List<V_HIS_EXP_MEST_MEDICINE> ViewMedicines { get; set; }
        public List<V_HIS_EXP_MEST_MATERIAL> ViewMaterials { get; set; }
        public List<HIS_MEDICINE_BEAN> MedicineBeans { get; set; }
        public List<HIS_MATERIAL_BEAN> MaterialBeans { get; set; }
        public List<HIS_EXP_MEST_MEDICINE> Medicines { get; set; }
        public List<HIS_EXP_MEST_MATERIAL> Materials { get; set; }
    }
}
