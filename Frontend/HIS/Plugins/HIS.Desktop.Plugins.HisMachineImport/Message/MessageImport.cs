using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMachineImport.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá maxlength";
        internal const string KhongHopLe = "{0} không hợp lệ";
        internal const string ThieuTruongDL = "Thiếu trường {0}";
        internal const string DaTonTai = " {0} đã tồn tại trong danh sách máy CLS";
        internal const string DaTonTaiLoaiMayCLS = " {0} đã tồn tại trong loại máy CLS";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại máy CLS có cùng các thông số trong file import";
        internal const string CoThiPhaiNhap = "Có {0} thì phải nhập {1}";
        internal const string MaMayCLSDaKhoa = "Mã máy CLS đã bị khóa";
        internal const string MaLoaiMayCLSDaKhoa = "Mã loại máy CLS đã bị khóa";
        internal const string DBDaTonTai = "Mã máy CLS \"{0}\" đã tồn tại trong cơ sở dữ liệu";
    }
}
