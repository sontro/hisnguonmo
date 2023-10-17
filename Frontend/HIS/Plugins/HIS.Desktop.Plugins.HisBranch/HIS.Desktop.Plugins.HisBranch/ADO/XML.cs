using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.HisBranch.ADO
{
    [XmlRoot("CO_SO_XA_PHUONG", IsNullable = true)]
    public class XML
    {
        [XmlElement(Order = 1)]
        public int STT { get; set; }

        [XmlElement(Order = 2)]
        public string MA_CSKCB { get; set; }

        [XmlElement(Order = 3)]
        public XmlCDataSection TEN_CSKCB { get; set; }

        [XmlElement(Order = 4)]
        public XmlCDataSection LOAI_HINH { get; set; }

        [XmlElement(Order = 5)] 
        public string PHAN_TUYEN { get; set; }

        [XmlElement(Order = 6)]
        public XmlCDataSection HINH_THUC_TC { get; set; }

        [XmlElement(Order = 7)]
        public string DANH_MUC_KHOA { get; set; }

        [XmlElement(Order = 8)]
        public string GIUONG_PD { get; set; }

        [XmlElement(Order = 9)]
        public string GIUONG_TK { get; set; }

        [XmlElement(Order = 10)]
        public string GIUONG_HSTC { get; set; }

        [XmlElement(Order = 11)]
        public string GIUONG_HSCC { get; set; }

        [XmlElement(Order = 12)]
        public XmlCDataSection LDLK { get; set; }

        [XmlElement(Order = 13)]
        public string MA_TINH { get; set; }

        [XmlElement(Order = 14)]
        public string MA_HUYEN { get; set; }

        [XmlElement(Order = 15)]
        public string MA_XA { get; set; }

        [XmlElement(Order = 16)]
        public string DIEN_THOAI { get; set; }
    }
}
