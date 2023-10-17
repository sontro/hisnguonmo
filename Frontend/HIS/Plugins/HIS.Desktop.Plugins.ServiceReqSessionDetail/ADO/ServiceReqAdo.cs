using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqSessionDetail.ADO
{
    public class ServiceReqAdo : MOS.EFMODEL.DataModels.HIS_SERVICE_REQ
    {
        public string MEDI_STOCK_CODE { get; set; }
        public string MEDI_STOCK_NAME { get; set; }
        public long MEDI_STOCK_ID { get; set; }
        public long EXP_MEST_ID { get; set; }
    }
}
