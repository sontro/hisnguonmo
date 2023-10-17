using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.InImpMestEdit.ADO
{
    class ResultImpMestADO
    {
        public HisInitImpMestSDO HisInitSDO { get; set; }
        //public HisInveImpMestSDO HisInveSDO { get; set; }
        //public HisOtherImpMestSDO HisOtherSDO { get; set; }

        public List<HisMedicineWithPatySDO> HisMedicineSDOs { get; set; }
        public List<HisMaterialWithPatySDO> HisMaterialSDOs { get; set; }

        public long ImpMestTypeId { get; set; }

        public ResultImpMestADO() { }

        public ResultImpMestADO(HisInitImpMestSDO initSDO)
        {
            this.HisInitSDO = initSDO;
            this.HisMaterialSDOs = initSDO.Materials;
            this.HisMedicineSDOs = initSDO.Medicines;
            this.ImpMestTypeId = initSDO.ImpMest.IMP_MEST_TYPE_ID;
        }

        //public ResultImpMestADO(HisInveImpMestSDO inveSDO)
        //{
        //    this.HisInveSDO = inveSDO;
        //    this.HisMaterialSDOs = inveSDO.Materials;
        //    this.HisMedicineSDOs = inveSDO.Medicines;
        //    this.ImpMestTypeId = inveSDO.ImpMest.IMP_MEST_TYPE_ID;
        //}

        //public ResultImpMestADO(HisOtherImpMestSDO otherSDO)
        //{
        //    this.HisOtherSDO = otherSDO;
        //    this.HisMaterialSDOs = otherSDO.Materials;
        //    this.HisMedicineSDOs = otherSDO.Medicines;
        //    this.ImpMestTypeId = otherSDO.ImpMest.IMP_MEST_TYPE_ID;
        //}
    }
}
