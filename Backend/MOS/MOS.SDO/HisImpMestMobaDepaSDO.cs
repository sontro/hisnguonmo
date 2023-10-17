using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisImpMestMobaDepaSDO
    {
        public List<HisMobaMedicineSDO> MobaMedicines { get; set; }
        public List<HisMobaMaterialSDO> MobaMaterials { get; set; }
        public long ExpMestId { get; set; }
        public long RequestRoomId { get; set; }
        public string Description { get; set; }

        public HisImpMestMobaDepaSDO()
        {
        }

        public HisImpMestMobaDepaSDO(long expMestId,long requestRoomId, List<HisMobaMedicineSDO> mobaMedicines, List<HisMobaMaterialSDO> mobaMaterials)
        {
            this.MobaMedicines = mobaMedicines;
            this.MobaMaterials = mobaMaterials;
            this.ExpMestId = expMestId;
            this.RequestRoomId = requestRoomId;
        }
    }
}
