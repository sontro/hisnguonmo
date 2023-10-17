using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ImportHisBedBsty.ADO
{
    public class ImportADO : MOS.EFMODEL.DataModels.HIS_BED_BSTY
    {
        public string BED_CODE { get; set; }
        public string BED_NAME { get; set; }
        public string SERVICE_CODE { get; set; }
        public string SERVICE_NAME { get; set; }
        public long DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string ERROR { get; set; }
    }
}
