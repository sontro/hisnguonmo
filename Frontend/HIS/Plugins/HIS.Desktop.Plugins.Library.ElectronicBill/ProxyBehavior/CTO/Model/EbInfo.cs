using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class EbInfo
    {
        /// <summary>
        /// Tên khách hàng (tên công ty) ghi trên hóa đơn
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// Địa chỉ xuất hóa đơn của người mua
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Mã số thuế người mua, có thể là mã số thuế Việt Nam hoặc mã số thuế nước ngoài
        /// </summary>
        public string tax { get; set; }
    }
}
