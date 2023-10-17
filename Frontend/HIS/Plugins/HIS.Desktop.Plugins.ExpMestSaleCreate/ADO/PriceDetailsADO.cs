using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExpMestSaleCreate.ADO
{
    public class PriceDetailsADO
    {
        public string PATIENT_NAME { get; set; }
        public decimal PRICE { get; set; }
        public string PRICE_ROUND { get; set; }
        public decimal BASE_VALUE { get; set; }
        public decimal DiscountRatio { get; set; }
    }
}
