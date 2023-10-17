using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.Data
{
    public class ProductBase
    {
        public string ProdCode { get; set; }

        public string ProdName { get; set; }

        public string ProdUnit { get; set; }

        public decimal? ProdQuantity { get; set; }

        public decimal? ProdPrice { get; set; }

        public decimal Amount { get; set; }

        public int Type { get; set; }

        /// <summary>
        /// Loai thue suat hoa don. 1 - 0%, 2 - 5%, 3 - 10%, 4 - khong chiu thue, 5 - khong ke khai thue, 6 - khac
        /// </summary>
        public int TaxRateID { get; set; }

        public long Stt { get; set; }
    }
}
