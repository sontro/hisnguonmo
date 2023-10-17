using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.SAFECERT.ADO
{
    class EInvoice
    {
        public string idMaster { get; set; }
        public string sodonhang { get; set; }
        public string ngaydonhang { get; set; }
        public string tenkhachhang { get; set; }
        public string tendonvi { get; set; }
        public string masothue { get; set; }//mã số thuế người mua
        public string diachi { get; set; }
        public string sotaikhoan { get; set; }
        public string noimotaikhoan { get; set; }
        public string hinhthuctt { get; set; }
        public string dienthoainguoimua { get; set; }
        public string faxnguoimua { get; set; }
        public string emailnguoimua { get; set; }
        public string macnCh { get; set; }
        public string tencnCh { get; set; }
        public string idCompany { get; set; }
        public string sohopdong { get; set; }
        public string ngayhopdong { get; set; }
        public string sovandon { get; set; }
        public string socontainer { get; set; }
        public int? idMasterRef { get; set; }
        public string tongkhongchiuvat { get; set; }
        public string tongtruocvat05 { get; set; }
        public string tongtruocvat10 { get; set; }
        public string tongvat05 { get; set; }
        public string tongvat10 { get; set; }
        public string tongsauvat05 { get; set; }
        public string tongsauvat10 { get; set; }
        public string tongtienchuathue { get; set; }
        public string tongtienthue { get; set; }
        public string tongtienckgg { get; set; }
        public string tienchiphikhac { get; set; }
        public string tongtientt { get; set; }
        public string sotienbangchu { get; set; }
        public string hoaDonMoRong { get; set; }
        public List<EinvoiceLine> listEinvoiceLine { get; set; }

        public string maKhachHang { get; set; }

        public string ngayKyNguoiban { get; set; }
        public string loaiTienTe { get; set; }
        public decimal tyGia { get; set; }
        public string soHieuBangKe { get; set; }
        public string thongTinChungHoaDon { get; set; }
        public int tinhTrangKyNguoiBan { get; set; }
        public string mcCQT { get; set; }
        public string moTaKetQua { get; set; }
    }
}
