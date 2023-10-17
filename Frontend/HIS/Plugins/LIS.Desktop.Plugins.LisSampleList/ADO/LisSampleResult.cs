using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS.Desktop.Plugins.LisSampleList.ADO
{
    class LisSampleResult
    {
        /// <summary>
        /// Biểu diễn cho các mã lỗi hoặc thành công.
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// Decription mã lỗi
        /// </summary>
        public string message { get; set; }
        
        public bool success { get; set; }
        /// <summary>
        /// Link download file lỗi (csv), file log chi tiết lỗi dữ liệu nếu file không hợp lệ
        /// </summary>
        public string linkFileError { get; set; }
        /// <summary>
        /// Link trỏ vào page sau khi upload file thành công. Sẽ thực hiện thao tác ký tại page này. Vui long redirect sang link trên để thực hiện thao tác ký
        /// </summary>
        public string linkSignPage { get; set; }
    }
}
