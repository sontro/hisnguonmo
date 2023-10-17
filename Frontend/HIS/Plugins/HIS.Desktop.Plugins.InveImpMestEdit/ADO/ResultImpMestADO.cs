using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InveImpMestEdit.ADO
{
    class ResultImpMestADO
    {
        public HisInveImpMestSDO HisInveSDO { get; set; }

        public List<HisMedicineWithPatySDO> HisMedicineSDOs { get; set; }
        public List<HisMaterialWithPatySDO> HisMaterialSDOs { get; set; }

        public long ImpMestTypeId { get; set; }

        public ResultImpMestADO() { }

        public ResultImpMestADO(HisInveImpMestSDO inveSDO)
        {
            this.HisInveSDO = inveSDO;
            this.HisMaterialSDOs = inveSDO.Materials;
            this.HisMedicineSDOs = inveSDO.Medicines;
            this.ImpMestTypeId = inveSDO.ImpMest.IMP_MEST_TYPE_ID;
        }
    }
}
