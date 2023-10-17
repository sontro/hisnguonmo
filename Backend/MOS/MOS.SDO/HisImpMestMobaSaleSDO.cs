using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMobaSaleSDO
    {
        public List<HisMobaMedicineSDO> MobaMedicines { get; set; }
        public List<HisMobaMaterialSDO> MobaMaterials { get; set; }
        public long ExpMestId { get; set; }
        public long RequestRoomId { get; set; }
        public string Description { get; set; }

        public HisImpMestMobaSaleSDO()
        {
        }

        public HisImpMestMobaSaleSDO(long expMestId, long requestRoomId, List<HisMobaMedicineSDO> mobaMedicines, List<HisMobaMaterialSDO> mobaMaterials)
        {
            this.MobaMedicines = mobaMedicines;
            this.MobaMaterials = mobaMaterials;
            this.ExpMestId = expMestId;
            this.RequestRoomId = requestRoomId;
        }
    }
}
