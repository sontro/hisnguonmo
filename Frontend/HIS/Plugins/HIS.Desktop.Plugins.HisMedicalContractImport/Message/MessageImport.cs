using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisMedicalContractImport.Message
{
    class MessageImport
    {
        internal const string Maxlength = "{0} vượt quá độ dài cho phép";
        internal const string KhongHopLe = "{0} không hợp lệ";
        internal const string ThieuTruongDL = "Thiếu trường {0}";
        internal const string TonTaiTrungMaThuocTrongFileImport = "Hợp đồng {0} tồn tại trùng mã thuốc {1} trong file import";
        internal const string TonTaiTrungMaVatTuTrongFileImport = "Hợp đồng {0} tồn tại trùng mã vật tư {1} trong file import";
        internal const string DuLieuDaKhoa = "{0} đã bị khóa";
        internal const string DulieuHopDongKhongHopLe = "Đã tồn tại dữ liệu hợp đồng với mã {0} nhưng khác dữ liệu: {1}";
        internal const string ThauKhongCoMaThuocVT = "Quyết định thầu {0} không có dữ liệu {1}";
        internal const string DBDaTonTai = "Số hợp đồng {0} đã tồn tại";
    }
}
