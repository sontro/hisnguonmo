using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisTrackingList.ADO
{
    public class DocumentTrackingADO
    {
        public DocumentTrackingADO() { }
        public long TRACKING_TIME { get; set; }
        public MemoryStream DocumentFile { get; set; }
        public string FullTemplateFileName { get; set; }
        public string saveFilePath { get; set; }
    }
}
