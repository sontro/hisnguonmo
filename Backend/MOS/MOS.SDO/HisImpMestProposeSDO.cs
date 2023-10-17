using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisImpMestProposeSDO
    {
        public long? Id { get; set; }// cho truong hop update
        public long WorkingRoomId { get; set; }
        public long SupplierId { get; set; }

        public List<long> ImpMestIds { get; set; }
    }
}
