using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TestServiceExecute.PACS
{
    class SeriesADO
    {
        public SeriesADO() { }
        public string ModalityName { get; set; }
        public string MachineName { get; set; }
        public string BodyPart { get; set; }
        public string ViewPosition { get; set; }
        public string SeriesDateTime { get; set; }
        public int SeriesInstances { get; set; }
        public int SeriesNumber { get; set; }
        public List<ImagesADO> Images { get; set; }
    }
}
