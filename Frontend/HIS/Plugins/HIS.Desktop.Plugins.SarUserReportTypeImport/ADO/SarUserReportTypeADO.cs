using MOS.EFMODEL.DataModels;
using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SarUserReportTypeImport.ADO
{
    class SarUserReportTypeADO : SAR_USER_REPORT_TYPE
    {
        //public long ROOM_TYPE_ID { get; set; }
        //public long? ROOM_GROUP_ID { get; set; }
        //public string ROOM_TYPE_CODE { get; set; }
        public string ERROR { get; set; }
        public string REPORT_TYPE_CODE { get; set; }
        public string REPORT_TYPE_NAME { get; set; }
        
    }
}
