using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisCoTreatmentFinishSDO
    {
        public long Id { get; set; }
        public long FinishTime { get; set; }
        public long RequestRoomId { get; set; }
        public string IcdSubCode {get;set;}
        public string IcdText {get;set;}
    }
}
