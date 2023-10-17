using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV3.ADO
{
    public class ServiceReqADO
    {
        public ServiceReqADO() { }

        public long? NUMBER_ORDER { get; set; }
        public string EXCUTE_ROOM_NAME { get; set; }
        public string SERVICE_NAME { get; set; }
        public string INTRUCTION_DATE { get; set; }
        public string INTRUCTION_TIME { get; set; }
    }
}
