using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteRoleUserImport
{
    class Message
    {
        internal const string Maxlength = "{0} vượt quá maxlength|";
        internal const string KhongHopLe = "{0} không hợp lệ|";
        internal const string KhongTonTai = "{0} không tồn tại|";
        internal const string ThieuTruongDL = "Thiếu trường dữ liệu bắt buộc {0}|";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại dòng dữ liệu trùng nhau trong file import|";
        internal const string DuLieuDaTonTai = "Dữ liệu đã tồn tại|";
    }
}
