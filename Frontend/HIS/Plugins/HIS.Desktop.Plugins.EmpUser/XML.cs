using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.EmpUser
{
    [XmlRoot("TAI_KHOAN_NHAN_VIEN", IsNullable = true)]
    public class XML
    {
        [XmlElement(Order = 1)]
        public int STT { get; set; }

        [XmlElement(Order = 2)]
        public string MA_CSKCB { get; set; }

        [XmlElement(Order = 3)]
        public XmlCDataSection HO_TEN { get; set; }

        [XmlElement(Order = 4)]
        public string GIOI_TINH { get; set; }

        [XmlElement(Order = 5)] 
        public string MA_DANTOC { get; set; }

        [XmlElement(Order = 6)]
        public string NGAY_SINH { get; set; }

        [XmlElement(Order = 7)]
        public string SO_CCCD { get; set; }

        [XmlElement(Order = 8)]
        public string CHUCDANH_NN { get; set; }

        [XmlElement(Order = 9)]
        public string VI_TRI { get; set; }

        [XmlElement(Order = 10)]
        public XmlCDataSection MA_CCHN { get; set; }

        [XmlElement(Order = 11)]
        public string NGAYCAP_CCHN { get; set; }

        [XmlElement(Order = 12)]
        public XmlCDataSection NOICAP_CCHN { get; set; }

        [XmlElement(Order = 13)]
        public string PHAMVI_CM { get; set; }

        [XmlElement(Order = 14)]
        public string THOIGIAN_DK { get; set; }

        [XmlElement(Order = 15)]
        public string CSKCB_KHAC { get; set; }
    }
}
