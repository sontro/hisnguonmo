using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class ServiceReqSessionDetailADO
    {
        public long? ServiceReqId { get; set; }
        public long IntructionTime { get; set; }
        public string SessionCode { get; set; }

        public ServiceReqSessionDetailADO(long? serviceReqId)
        {
            this.ServiceReqId = serviceReqId;
        }

        public ServiceReqSessionDetailADO(string sessionCode, long intructionTime)
        {
            this.SessionCode = sessionCode;
            this.IntructionTime = intructionTime;
        }
    }
}
