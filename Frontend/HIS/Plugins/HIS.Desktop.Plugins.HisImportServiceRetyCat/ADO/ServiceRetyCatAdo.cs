using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportServiceRetyCat.ADO
{
    class ServiceRetyCatAdo 
    {
        public long IdRow { get; set; }

        public string REPORT_TYPE_CODE { get; set; }
        public string REPORT_TYPE_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_TYPE_CODE { get; set; }
        public string CATEGORY_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public string SERVICE_TYPE_NAME { get; set; }
        public string CATEGORY_NAME { get; set; }
        public string ERROR { get; set; }
    }
}
