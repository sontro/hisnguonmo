using MOS.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExportXmlQD130
{
    public class TreatmentImportADO
    {
        public string IN_TIME_STR { get; set; }
        public string OUT_TIME_STR { get; set; }

        public long? IN_TIME { get; set; }
        public long? OUT_TIME { get; set; }
        public string TDL_HEIN_CARD_NUMBER { get; set; }
        public string TDL_PATIENT_CODE { get; set; }
        public string TDL_PATIENT_NAME { get; set; }
        public string TREATMENT_CODE { get; set; }

        public string EXPORT_HEIN_SERVICE_CODE { get; set; }
    }
}
