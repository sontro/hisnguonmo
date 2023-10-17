using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class DuLieuDonThuocQlpkSDO
    {
        public string mabn { get; set; }

        public string hoten { get; set; }

        public string namsinh { get; set; }

        public long phai { get; set; }

        public string cmnd { get; set; }

        public string dantoc { get; set; }

        public string tentt { get; set; }

        public string tenquan { get; set; }

        public string tenpxa { get; set; }

        public string thon { get; set; }

        public string sothe { get; set; }

        public DateTime ngaykham { get; set; }

        public string doituong { get; set; }

        public string chandoan { get; set; }

        public string ghichu { get; set; }

        public string bacsy { get; set; }

        public string mabacsy { get; set; }

        public List<ThuocDieuTriQlpkSDO> ThuocDieuTri { get; set; }
    }
}
