using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.DDO
{
    public class ServiceReqGeneralByDepaDDO
    {

        public string ExecuteRoomCode { get; set; }
        public string ExecuteRoomName { get; set; }

        public decimal NoProcessAmount { get; set; }
        public decimal ProcessingAmount { get; set; }
        public decimal ResultSubclinicalAmount { get; set; }
        public decimal DoneAmount { get; set; }
    }
}
