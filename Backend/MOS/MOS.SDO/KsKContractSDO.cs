using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class KsKContractSDO
    {
        public HIS_KSK_CONTRACT KskContract { get; set; }
        public long RequestRoomId { get; set; }
    }
}
