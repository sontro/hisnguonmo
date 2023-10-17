using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class FileRequest
    {
        /// <summary>
        /// Mã duy nhất từ hệ thống tích hợp để xác định hóa đơn, chính là id trong phát hành hóa đơn
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Mã số thuế của doanh nghiệp/chi nhánh phát hành hóa đơn.
        /// </summary>
        public string supplierTaxCode { get; set; }

        /// <summary>
        /// Số hóa đơn, bao gồm ký hiệu hóa đơn và số thứ tự hóa đơn
        /// </summary>
        public string invoiceNo { get; set; }

        /// <summary>
        /// Mã mẫu hóa đơn
        /// </summary>
        public string templateCode { get; set; }

        /// <summary>
        /// Loại file muốn tải về, các định dạng được phép: PDF
        /// </summary>
        public string fileType { get; set; }
    }
}
