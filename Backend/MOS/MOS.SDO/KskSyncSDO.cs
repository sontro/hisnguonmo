using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MOS.SDO
{
    public class KskSyncSDO
    {
        public string SO { get; set; }
        public string HOTEN { get; set; }
        public string GIOITINHVAL { get; set; }
        public string NGAYSINH { get; set; }
        public string DIACHITHUONGTRU { get; set; }
        public string MATINH_THUONGTRU { get; set; }
        public string MAHUYEN_THUONGTRU { get; set; }
        public string MAXA_THUONGTRU { get; set; }
        public string SOCMND_PASSPORT { get; set; }
        [XmlElement("NGAYTHANGNAMCAPCMD")]
        public string NGAYTHANGNAMCAPCMND { get; set; }
        public string NOICAP { get; set; }
        public string IDBENHVIEN { get; set; }
        public string BENHVIEN { get; set; }
        public string NGAYKETLUAN { get; set; }
        public string BACSYKETLUAN { get; set; }
        public string KETLUAN { get; set; }
        public string HANGBANGLAI { get; set; }
        public string NGAYKHAMLAI { get; set; }
        public string LYDO { get; set; }
        public string TINHTRANGBENH { get; set; }
        public string STATE { get; set; }
        public string NONGDOCON { get; set; }
        public string DVINONGDOCON { get; set; }
        public string MATUY { get; set; }
        public string SIGNDATA { get; set; }
    }
}
