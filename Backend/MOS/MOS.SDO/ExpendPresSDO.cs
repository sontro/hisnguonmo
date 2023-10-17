using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class ExpendPresSDO
    {
        public long RequestRoomId { get; set; }
        public long MediStockId { get; set; }
        public long ServiceReqId { get; set; }
        public long? SereServId { get; set; }
    }
}
