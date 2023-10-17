using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class EbProduct
    {
        /// <summary>
        /// Tên hàng hóa, dịch vụ
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Tên đơn vị tính hàng hóa, dịch vụ
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// Số lượng của hàng hóa, luôn là số dương
        /// </summary>
        public double quantity { get; set; }

        /// <summary>
        /// Đơn giá của hàng hóa, không có số âm
        /// </summary>
        public decimal price { get; set; }

        /// <summary>
        /// Là tổng tiền chưa bao gồm VAT của hàng hóa/dịch vụ
        /// </summary>
        public decimal amount { get; set; }

        /// <summary>
        /// Thuế suất của hàng hóa, dịch vụ
        /// </summary>
        public int tax { get; set; }

        /// <summary>
        /// Là tổng tiền đã bao gồm VAT của hàng hóa/dịch vụ. Tổng tiền không có số âm
        /// </summary>
        public decimal total { get; set; }
    }
}
