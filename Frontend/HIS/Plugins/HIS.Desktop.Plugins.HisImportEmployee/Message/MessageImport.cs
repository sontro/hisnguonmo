using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImport.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá maxlength|";
        internal const string KhongHopLe = "{0} không hợp lệ|";
        internal const string ThieuTruongDL = "Thiếu trường dữ liệu bắt buộc {0}|";
        internal const string DaTonTai = " {0} đã tồn tại dữ liệu nhân viên |";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại {0} trùng nhau trong file import|";
        internal const string CoThiPhaiNhap = "Có {0} thì phải nhập {1}|";
        internal const string MaKhoaDaKhoa = "Mã khoa {0} đã bị khóa| ";
        internal const string FileImportDaTonTai = "File import tồn tại tên đăng nhập giống nhau {0} | ";
        internal const string SaiDinhDang = " {0} sai định dạng (X)| ";
        internal const string KhongtonTai = " {0} không tồn tại trong danh sách người dùng( ACS_USER) |";
    }
}
