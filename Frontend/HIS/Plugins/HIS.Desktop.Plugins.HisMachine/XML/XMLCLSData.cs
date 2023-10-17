using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HIS.Desktop.Plugins.HisMachine.XML
{
    [XmlRoot("DanhSachMayCls")]
    public class XMLCLSData
    {
        [XmlElement("MayCls")]
        public List<XMLCLSDetailData> MayCls { get; set; }
    }
}
