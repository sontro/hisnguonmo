using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.XMLViewer.XML.XML4
{
    [Serializable]
    public class XML5DetailData
    {
        [XmlElement(Order = 1)]
        public string MA_LK { get; set; }

        [XmlElement(Order = 2)]
        public string STT { get; set; }

        [XmlElement(Order = 3)]
        public string DIEN_BIEN { get; set; }

        [XmlElement(Order = 4)]
        public string HOI_CHAN { get; set; }

        [XmlElement(Order = 5)]
        public string PHAU_THUAT { get; set; }

        [XmlElement(Order = 6)]
        public string NGAY_YL { get; set; }
    }
}
