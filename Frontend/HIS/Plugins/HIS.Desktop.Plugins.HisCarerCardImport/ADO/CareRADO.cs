using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisCarerCardImport.ADO
{
    public class CareRADO : MOS.EFMODEL.DataModels.HIS_CARER_CARD
    {
        public string SERVICE_CODE { get; set; }
        public string ERROR { get; set; }
    }
}
