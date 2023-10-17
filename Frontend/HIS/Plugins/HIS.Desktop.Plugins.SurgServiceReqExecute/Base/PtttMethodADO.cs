using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SurgServiceReqExecute.Base
{
    public class PtttMethodADO
    {
        public long ID { get; set; }
        public string PTTT_METHOD_CODE { get; set; }
        public string PTTT_METHOD_NAME { get; set; }
        public decimal? AMOUNT { get; set; }
        public bool IS_SELECTION { get; set; }
        public bool IS_COMBO { get; set; }
        public long? PTTT_GROUP_ID { get; set; }
        public string PTTT_GROUP_NAME { get; set; }
        public long SERE_SERV_ID { get; set; }
        public long? EKIP_ID { get; set; }
        public long SERVICE_REQ_ID { get; set; }
        public EkipUsersADO EkipUsersADO { get; set; }
    }
}
