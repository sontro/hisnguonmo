using SAR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarRetyFofi.ADO
{
    internal class RetyFofiExportADO : V_SAR_RETY_FOFI
    {
        public RetyFofiExportADO() { }

        public string NUM_ORDER_STR { get; set; }
        public string IS_REQUIRE_STR { get; set; }
    }
}
