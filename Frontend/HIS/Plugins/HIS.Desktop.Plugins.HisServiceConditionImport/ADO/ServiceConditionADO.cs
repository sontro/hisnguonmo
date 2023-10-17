using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceConditionImport.ADO
{
    public class ServiceConditionADO :  MOS.EFMODEL.DataModels.HIS_SERVICE_CONDITION
    {
        public string ERROR { get; set; }
        public string SERVICE_CODE { get; set; }
        public string HEIN_RATIO_STR { get; set; }
        public string HEIN_PRICE_STR { get; set; }
    }
}
