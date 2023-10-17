using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MedicineType.ADO
{
    class MedicineImportADO : V_HIS_MEDICINE_TYPE
    {
        public string ADDICTIVE { get; set; }
        public string ALLOW_EXPORT_ODD { get; set; }
        public string ALLOW_ODD { get; set; }
        public string ANTIBIOTIC { get; set; }
        public string BUSINESS { get; set; }
        public string FUNCTIONAL_FOOD { get; set; }
        public string OUT_PARENT_FEE { get; set; }
        public string REQUIRE_HSD { get; set; }
        public string SALE_EQUAL_IMP_PRICE { get; set; }
        public string NEUROLOGICAL { get; set; }
        public string STOP_IMP { get; set; }
        public string STAR_MARK { get; set; }
        public string HEIN_LIMIT_PRICE_IN_TIME_STR { get; set; }
        public string HEIN_LIMIT_PRICE_INTR_TIME_STR { get; set; }
        public decimal? COGS { get; set; }
        public decimal? ESTIMATE_DURATION { get; set; }
    }
}
