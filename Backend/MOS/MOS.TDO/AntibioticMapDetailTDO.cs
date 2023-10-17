using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.TDO
{
    public class AntibioticMapDetailTDO
    {
        public string TestIndexCode { get; set; }//ServiceCode
        public string MaViKhuan { get; set; }
        public string TenViKhuan { get; set; }
        public string GhiChuViKhuan { get; set; }
        public decimal? SoLuongViKhuan { get; set; }
        public decimal? MatDoViKhuan { get; set; }
        public List<ResitanceResultTDO> DanhSachDeKhang { get; set; }
        public List<AntibioticResultTDO> DanhSachKhangSinh { get; set; }
    }
}
