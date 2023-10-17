using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AggrImpMestPrintFilter.ADO
{
    public class MediMatePrintADO
    {
        public List<HIS_IMP_MEST> _ImpMests_Print { get; set; }
        public List<V_HIS_IMP_MEST_MATERIAL> _ImpMestMaterials { get; set; }
        public List<V_HIS_IMP_MEST_MEDICINE> _ImpMestMedicines { get; set; }
    }
}
