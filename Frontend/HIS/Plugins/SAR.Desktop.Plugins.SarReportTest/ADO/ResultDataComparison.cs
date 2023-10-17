using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarReportTest.ADO
{
    class ResultDataComparison:SAR.EFMODEL.DataModels.V_SAR_REPORT
    {
        public string Difference { get; set; }
        public string Status { get; set; }
        public bool Result { get; set; }
        public long ID_Des { get; set; }

        public List<ImportDataADO> DataFiles { get; set; }
        public List<ImportDataADO> DataFileDestinations { get; set; }
    }
}
