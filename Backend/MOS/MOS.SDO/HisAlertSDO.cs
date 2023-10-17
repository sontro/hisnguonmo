using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisAlertSDO
    {
        public long RequestRoomId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<long> ReceiveDepartmentIds { get; set; }
    }
}
