using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisUserRoomImport.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá maxlength";
        internal const string KhongHopLe = "{0} không hợp lệ";
        internal const string ThieuTruongDL = "Thiếu trường {0}";
        internal const string DaTonTai = " {0} đã tồn tại trong phòng";
        internal const string PhongKhongTonTai = " Phòng không tồn tại";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại phòng có cùng giá trị trong file import";
        internal const string CoThiPhaiNhap = "Có {0} thì phải nhập {1}";
        internal const string PhongDaKhoa = "Phòng đã bị khóa";
        internal const string MaLoaiPhongDaKhoa = "Mã loại phòng đã bị khóa";
        internal const string DBDaTonTai = "Phòng này đã được gán với tài khoản {0} ";
    }
}
