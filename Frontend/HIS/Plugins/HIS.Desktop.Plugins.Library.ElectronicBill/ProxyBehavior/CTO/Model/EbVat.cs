using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class EbVat
    {
        /// <summary>
        /// Thuế suất của hóa đơn
        /// </summary>
        public int rate { get; set; }

        /// <summary>
        /// Tổng tiền thuế của hóa đơn
        /// </summary>
        public decimal amount { get; set; }
    }
}
