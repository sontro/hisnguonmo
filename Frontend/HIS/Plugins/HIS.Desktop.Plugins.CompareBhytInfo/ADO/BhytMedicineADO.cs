using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CompareBhytInfo.ADO
{
    class BhytMedicineADO
    {
        public string ACTIVE_INGR_BHYT_CODE { get; set; }
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        public string HEIN_SERVICE_TYPE_NAME { get; set; }
        public string SERVICE_UNIT_NAME { get; set; }
        public string CONCENTRA { get; set; }
        public string REGISTER_NUMBER { get; set; }
        public string BID_NUMBER { get; set; }
        public string BID_GROUP_CODE { get; set; }
        public string BID_PACKAGE_CODE { get; set; }
        public string HST_BHYT_CODE { get; set; }
        public decimal PRICE { get; set; }
    }
}
