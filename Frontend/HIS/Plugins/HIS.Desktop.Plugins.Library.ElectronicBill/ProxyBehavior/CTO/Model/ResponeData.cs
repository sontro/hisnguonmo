using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProxyBehavior.CTO.Model
{
    class ResponeData
    {
        /// <summary>
        /// Mã trả về khi gọi api
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Nội dung thông điệp trả về khi gọi api
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Trạng thái trả về. nếu thành công: “success”, có lỗi: “error”, ngoại lệ: “unknow”, dữ liệu đầu vào không hơp lệ : “” (null)
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Nội dung trả về tùy vào từng trường hợp api
        /// </summary>
        public object value { get; set; }
    }
}
