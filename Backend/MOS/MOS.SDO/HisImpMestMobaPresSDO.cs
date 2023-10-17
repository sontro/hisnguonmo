using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMobaPresSDO
    {
        public List<HisMobaPresMedicineSDO> MobaPresMedicines { get; set; }
        public List<HisMobaPresMaterialSDO> MobaPresMaterials { get; set; }

        public long ExpMestId { get; set; }
        public long RequestRoomId { get; set; }
        public long ImpMediStockId { get; set; }
        public string Description { get; set; }
        public long? TrackingId { get; set; }

        public HisImpMestMobaPresSDO()
        {
        }

        public HisImpMestMobaPresSDO(long expMestId, long requestRoomId, List<HisMobaPresMedicineSDO> mobaPresMedicines, List<HisMobaPresMaterialSDO> mobaPresMaterials)
        {
            this.MobaPresMedicines = mobaPresMedicines;
            this.MobaPresMaterials = mobaPresMaterials;
            this.ExpMestId = expMestId;
            this.RequestRoomId = requestRoomId;
        }
    }
}
