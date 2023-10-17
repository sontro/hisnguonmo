using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAR.Desktop.Plugins.SarImportRetyFofi.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá maxlength|";
        internal const string KhongHopLe = "{0} không hợp lệ|";
        internal const string ThieuTruongDL = "Thiếu trường {0}|";
        internal const string DaTonTai = " {0} trùng nhau|";
        internal const string DaTonTaiLoaiGiuong = " {0} đã tồn tại trong loại giường|";
        internal const string TonTaiTrungNhauTrongFileImport = "Tồn tại {0} trùng nhau trong file import|";
        internal const string CoThiPhaiNhap = "Có {0} thì phải nhập {1}|";
        internal const string MaLoaiBaoCao = "Mã loại báo cáo {0} đã bị khóa| ";
        internal const string MaTruongLoc = "Mã trường lọc {0} đã bị khóa| ";
        internal const string FileImportDaTonTai = "File import tồn tại Biểu báo cáo - Trường lọc báo cáo giống nhau {0} | ";
    }
}
