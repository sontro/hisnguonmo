using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.HisMachine.XML
{
    [Serializable]
    public class XMLCLSDetailData
    {
        [XmlElement(Order = 1)]
        public int STT { get; set; }

        [XmlElement(Order = 2)]
        public string MA_CSKCB { get; set; }

        [XmlElement(Order = 3)]
        public XmlCDataSection TEN_TB { get; set; }

        [XmlElement(Order = 4)]
        public string KY_HIEU { get; set; }

        [XmlElement(Order = 5)]
        public XmlCDataSection CONGTY_SX { get; set; }

        [XmlElement(Order = 6)]
        public XmlCDataSection NUOC_SX { get; set; }

        [XmlElement(Order = 7)]
        public string NAM_SX { get; set; }

        [XmlElement(Order = 8)]
        public string NAM_SD { get; set; }

        [XmlElement(Order = 9)]
        public string MA_MAY { get; set; }

        [XmlElement(Order = 10)]
        public string SO_LUU_HANH { get; set; }
    }
}
