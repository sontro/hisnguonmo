using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MaterialType.ADO
{
    class MaterialImportADO: V_HIS_MATERIAL_TYPE
    {
        public string CHEMICAL_SUBSTANCE { get; set; }
        public string ALLOW_EXPORT_ODD { get; set; }
        public string ALLOW_ODD { get; set; }
        public string AUTO_EXPEND { get; set; }
        public string BUSINESS { get; set; }
        public string IN_KTC_FEE { get; set; }
        public string OUT_PARENT_FEE { get; set; }
        public string REQUIRE_HSD { get; set; }
        public string SALE_EQUAL_IMP_PRICE { get; set; }
        public string STENT { get; set; }
        public string STOP_IMP { get; set; }
        public string HEIN_LIMIT_PRICE_IN_TIME_STR { get; set; }
        public string HEIN_LIMIT_PRICE_INTR_TIME_STR { get; set; }
        public decimal? COGS { get; set; }
    }
}
