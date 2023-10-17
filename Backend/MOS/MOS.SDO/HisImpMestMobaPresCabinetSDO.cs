using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMobaPresCabinetSDO
    {
        public List<HisMobaPresMedicineSDO> MobaPresMedicines { get; set; }
        public List<HisMobaPresMaterialSDO> MobaPresMaterials { get; set; }

        public long ExpMestId { get; set; }
        public long WorkingRoomId { get; set; }
        public long? ImpMediStockId { get; set; }
        public string Description { get; set; }
        public long? TrackingId { get; set; }

        public HisImpMestMobaPresCabinetSDO()
        {
        }
    }
}
