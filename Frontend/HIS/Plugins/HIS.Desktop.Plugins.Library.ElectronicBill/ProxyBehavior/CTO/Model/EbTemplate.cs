using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class EbTemplate
    {
        /// <summary>
        /// Mã loại hóa đơn. ví dụ: 02GTTT0
        /// 02GTTT
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Là ký hiệu hóa đơn + số hóa đơn vd: AA/16E0000001, tuân theo chuẩn của cục thuế
        /// AB/20E
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// Mã mẫu hóa đơn
        /// 02GTTT0/001
        /// </summary>
        public string id { get; set; }
    }
}
