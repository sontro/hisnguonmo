using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class HisTreatmentClinicalDetailForEmrTDO
    {
        public List<PhauThuatThuThuatEmrTDO> PhauThuatThuThuat { get; set; }
        public List<ChiSoXetNghiemEmrTDO> ChiSoXetNghiem { get; set; }
    }

    public class PhauThuatThuThuatEmrTDO
    {
        public string BacSyChiDinh { get; set; }
        public string BacSyGayMe { get; set; }
        public string BacSyGayMeHoVaTen { get; set; }
        public string BacSyPhauThuat { get; set; }
        public string BacSyPhauThuatHoVaTen { get; set; }
        public string CachThucPhauThuatThuThuat { get; set; }
        public string ChanDoanChinh { get; set; }
        public string ChanDoanSauPhauThuatThuThuat { get; set; }
        public string ChanDoanTruocPhauThuatThuThuat { get; set; }
        public List<string> EkipThucHien { get; set; }
        public long IDPhauThuatThuThuat { get; set; }
        public int Loai { get; set; }
        public string MaBacSyChiDinh { get; set; }
        public string MaQuanLy { get; set; }
        public string MaYLenh { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayPhauThuatThuThuat { get; set; }
        public DateTime? NgayPhauThuatThuThuat_Gio { get; set; }
        public string PhuongPhapPhauThuatThuThuat { get; set; }
        public string PhuongPhapPhauThuatThuThuat2 { get; set; }
        public string PhuongPhapVoCam { get; set; }
        public string TenPhauThuatThuThuat { get; set; }
        public string ThoiGianXyLy { get; set; }
        public List<ThuocVatTuEmrTDO> VatTuTieuHaos { get; set; }
        public long IDChiTietYLenh { get; set; }
        public long? ThoiGianThuchien { get; set; }
        public long? ThoiGianKetThuc { get; set; }
        public long? ThoiGianYLenh { get; set; }
        public long? IdNhomPTTT { get; set; }
        public string MaNhomPTTT { get; set; }
        public string TenNhomPTTT { get; set; }
    }

    public class ThuocVatTuEmrTDO
    {
        public string DonVi { get; set; }
        public string GhiChu { get; set; }
        public string HangSanXuat { get; set; }
        public string MaThuocVatTu { get; set; }
        public decimal SoLuong { get; set; }
        public string TenThuocVatTu { get; set; }
    }

    public class ChiSoXetNghiemEmrTDO
    {
        public string Description { get; set; }
        public DateTime? ExecuteTime { get; set; }
        public bool IsAbsAg { get; set; }
        public bool IsBloodABO { get; set; }
        public bool IsBloodRH { get; set; }
        public bool IsHCV { get; set; }
        public bool IsHIV { get; set; }
        public string TestIndexCode { get; set; }
        public string TestIndexName { get; set; }
        public string Value { get; set; }
    }
}
