using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImpKskService.ADO
{
    class ImportADO : MOS.EFMODEL.DataModels.HIS_KSK_SERVICE 
    {

        public string ImpVatRatio { get; set; }
        public string AMOUNT_STR { get; set; }
        public string IMP_PRICE { get; set; }
        public long ROOM_ID { get; set; }
        public string ROOM_CODE { get; set; }
        public string ROOM_NAME { get; set; }
        public string ERROR { get; set; }
        public string KSK_CODE { get; set; }
        public string KSK_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }

        public string ErrorDesc { get; set; }
    }
}
