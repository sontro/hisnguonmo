using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.DispenseMedicine.ADO
{
    class MetyProductADO:MOS.EFMODEL.DataModels.V_HIS_METY_PRODUCT
    {
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public string MANUFACTURER_NAME { get; set; }
        public string NATIONAL_NAME { get; set; }
        public long SERVICE_ID { get; set; }
    }
}
