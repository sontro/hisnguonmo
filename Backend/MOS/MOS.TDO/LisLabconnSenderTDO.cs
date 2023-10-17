using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class LisLabconnSenderTDO
    {
        public string MaHS { get; set; }
        public string MaBN { get; set; }
        public string HoTen { get; set; }
        public long NamSinh { get; set; }
        public string NgayChiDinh { get; set; }
        public string GioiTinh { get; set; }
        public long SLDichVu { get; set; }
    }

    public class UpdatePatientInfoTDO
    {
        public string MaHoSo { get; set; }
        public string NgayChiDinh { get; set; }
        public string SoBH { get; set; }
        public string MaBN { get; set; }
        public string HoTen { get; set; }
        public long NamSinh { get; set; }
        public string GioiTinh { get; set; }
        public string DiaChi { get; set; }
        public string ChanDoan { get; set; }
        public bool CapCuu { get; set; }
    }

    public class LabconnResponseTDO
    {
        public string ErrorMessage { get; set; }
        public bool IsSuccess { get; set; }
    }
}
