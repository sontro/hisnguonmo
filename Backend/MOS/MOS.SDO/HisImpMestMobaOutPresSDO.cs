using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestMobaOutPresSDO
    {
        public List<HisMobaPresSereServSDO> MobaPresMedicines { get; set; }
        public List<HisMobaPresSereServSDO> MobaPresMaterials { get; set; }

        public long ServiceReqId { get; set; }
        public long RequestRoomId { get; set; }        
        public string Description { get; set; }
        public long? TrackingId { get; set; }

        public HisImpMestMobaOutPresSDO()
        {
        }

        public HisImpMestMobaOutPresSDO(long serviceReqId, long requestRoomId, List<HisMobaPresSereServSDO> mobaPresMedicines, List<HisMobaPresSereServSDO> mobaPresMaterials)
        {
            this.MobaPresMedicines = mobaPresMedicines;
            this.MobaPresMaterials = mobaPresMaterials;
            this.ServiceReqId = serviceReqId;
            this.RequestRoomId = requestRoomId;
        }
    }
}
