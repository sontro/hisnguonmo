using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class ImpMestAggrApprovalResultSDO
    {
        public V_HIS_IMP_MEST ImpMest { get; set; }
        public List<V_HIS_IMP_MEST_MEDICINE> ImpMestMedicines { get; set; }
        public List<V_HIS_IMP_MEST_MATERIAL> ImpMestMaterials { get; set; }
    }
}
