using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisMaterialWithPatySDO
    {
        public HIS_MATERIAL Material { get; set; }
        public List<HIS_MATERIAL_PATY> MaterialPaties { get; set; }
        public List<ImpMestMaterialReusableSDO> SerialNumbers { get; set; }
        public HisMaterialWithPatySDO()
        {
        }

        public HisMaterialWithPatySDO(HIS_MATERIAL material, List<HIS_MATERIAL_PATY> materialPaties)
        {
            this.Material = material;
            this.MaterialPaties = materialPaties;
        }
    }
}
