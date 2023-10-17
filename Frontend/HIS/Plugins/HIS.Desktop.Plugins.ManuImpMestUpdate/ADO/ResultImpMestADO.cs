using MOS.SDO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ManuImpMestUpdate.ADO
{
    class ResultImpMestADO
    {
        public HisImpMestManuSDO hisManuImpMestSDO { get; set; }

        public HisImpMestInitSDO hisInitImpMestSDO { get; set; }

        public HisImpMestInveSDO hisInveImpMestSDO { get; set; }

        public HisImpMestOtherSDO hisOtherImpMestSDO { get; set; }

        public List<HisMedicineWithPatySDO> HisMedicineSDOs { get; set; }
        public List<HisMaterialWithPatySDO> HisMaterialSDOs { get; set; }

        public long ImpMestTypeId { get; set; }

        public ResultImpMestADO() { }

        public ResultImpMestADO(HisImpMestOtherSDO otherSDO)
        {
            this.hisOtherImpMestSDO = otherSDO;
            this.HisMaterialSDOs = otherSDO.OtherMaterials;
            this.HisMedicineSDOs = otherSDO.OtherMedicines;
            this.ImpMestTypeId = otherSDO.ImpMest.IMP_MEST_TYPE_ID;
        }

        public ResultImpMestADO(HisImpMestManuSDO manuSDO)
        {
            this.hisManuImpMestSDO = manuSDO;
            this.HisMaterialSDOs = manuSDO.ManuMaterials;
            this.HisMedicineSDOs = manuSDO.ManuMedicines;
            this.ImpMestTypeId = manuSDO.ImpMest.IMP_MEST_TYPE_ID;
        }

        public ResultImpMestADO(HisImpMestInitSDO initSDO)
        {
            this.hisInitImpMestSDO = initSDO;
            this.HisMaterialSDOs = initSDO.InitMaterials;
            this.HisMedicineSDOs = initSDO.InitMedicines;
            this.ImpMestTypeId = initSDO.ImpMest.IMP_MEST_TYPE_ID;
        }

        public ResultImpMestADO(HisImpMestInveSDO inveSDO)
        {
            this.hisInveImpMestSDO = inveSDO;
            this.HisMaterialSDOs = inveSDO.InveMaterials;
            this.HisMedicineSDOs = inveSDO.InveMedicines;
            this.ImpMestTypeId = inveSDO.ImpMest.IMP_MEST_TYPE_ID;
        }
    }
}
