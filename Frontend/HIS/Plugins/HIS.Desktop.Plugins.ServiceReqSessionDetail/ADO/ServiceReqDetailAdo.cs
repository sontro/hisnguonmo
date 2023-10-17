using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ServiceReqSessionDetail.ADO
{
    public class ServiceReqDetailAdo 
    {
        public long TYPE { get; set; }// 1 thuốc - 2 vật tư - 3 máu
        public string CODE { get; set; }
        public string NAME { get; set; }
        public decimal AMOUNT { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string TUTORIAL { get; set; }
    }
}
