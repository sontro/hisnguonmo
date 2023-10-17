using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.ElectronicBill.ProviderBehavior.SAFECERT.ADO
{
    class EinvoiceLine
    {
        public string idMaster { get; set; }
        public string sothutu { get; set; }
        public int sothutuIdx { get; set; }
        public string loaihhdv { get; set; }
        public string mahang { get; set; }
        public string tenhang { get; set; }
        public string donvitinh { get; set; }
        public string soluong { get; set; }
        public string dongia { get; set; }
        public string thanhtien { get; set; }
        public string thuesuat { get; set; }
        public string tienthue { get; set; }
        public string tongtien { get; set; }
        //public int khuyenmai { get; set; }
        public string thuettdb { get; set; }
        public int khonghienthi { get; set; }

        public string hoaDonMoRongCT { get; set; }
        public int? tinhchat { get; set; }
        public string tileckgg { get; set; }
        public string thanhtienckgg { get; set; }
        public int? loaidieuchinh { get; set; }
    }
}
