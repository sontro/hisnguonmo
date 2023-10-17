using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.AssignService.ADO
{
    public class MissingIcdADO
    {
        public long ID { get; set; }
        public long SERVICE_ID { get; set; }
        public string SERVICE_NAME { get; set; }
        public string ICD_CODE { get; set; }
        public string ICD_NAME { get; set; }
        public bool ICD_CAUSE_CHECK { get; set; }
        public bool ICD_MAIN_CHECK { get; set; }
    }
}
