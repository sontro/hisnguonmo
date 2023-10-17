using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Data
{
    public class ProductBasePlus : ProductBase
    {
        //public decimal? Discount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? AmountWithoutTax { get; set; }
        /// <summary>
        /// null: không thuế
        /// 0: 0%
        /// 1: 5%
        /// 2: 10%
        /// 3: 8%
        /// </summary>
        public int? TaxPercentage { get; set; }
        public decimal TaxConvert { get; set; }
    }
}
