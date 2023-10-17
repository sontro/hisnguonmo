using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarImportRetyFofi.ADO
{
    public class RetyFofiADO :SAR.EFMODEL.DataModels.SAR_RETY_FOFI
    {
        public string REPORT_TYPE_CODE { get; set; }
        public string REPORT_TYPE_NAME { get; set; }

        public string FORM_FIELD_CODE { get; set; }
        public string ERROR { get; set; }
        public string IS_REQUIRE_STR { get; set; }
        public bool IS_REQUIRE_DISPLAY { get; set; }
        public string NUM_ORDER_STR { get; set; }

        
    }
}
