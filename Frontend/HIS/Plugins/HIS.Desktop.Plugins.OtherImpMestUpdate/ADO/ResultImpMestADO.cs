using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.OtherImpMestUpdate.ADO
{
    class ResultImpMestADO
    {
        public HisOtherImpMestSDO hisOtherImpMestSDO { get; set; }

        public List<HisMedicineWithPatySDO> HisMedicineSDOs { get; set; }
        public List<HisMaterialWithPatySDO> HisMaterialSDOs { get; set; }

        public long ImpMestTypeId { get; set; }

        public ResultImpMestADO() { }

        public ResultImpMestADO(HisOtherImpMestSDO OtherSDO)
        {
            this.hisOtherImpMestSDO = OtherSDO;
            this.HisMaterialSDOs = OtherSDO.Materials;
            this.HisMedicineSDOs = OtherSDO.Medicines;
            this.ImpMestTypeId = OtherSDO.ImpMest.IMP_MEST_TYPE_ID;
        }
    }
}
