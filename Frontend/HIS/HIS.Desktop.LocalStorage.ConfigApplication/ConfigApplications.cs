using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.ConfigApplication
{
    public class ConfigApplications
    {
        public static long NumPageSize = ConfigApplicationWorker.DEFAULT_NUM_PAGESIZE;
        /// <summary>
        /// Định dạng hiển thị số tiền tệ trong phần mềm, cấu hình số chữ số sau dấu phẩy, giá trị mặc định là 0
        /// </summary>
        public static int NumberSeperator = 0;
        /// <summary>
        /// Đặt là 1 nếu chọn chế độ hiển thị để xem xong rồi in, đặt là 2 nếu chọn chế độ in ngay không cần xem
        /// </summary>
        public static long CheDoInChoCacChucNangTrongPhanMem = 0;
    }
}
