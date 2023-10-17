using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrDocument
{
    public class StreamToPdfADO
    {
        public StreamToPdfADO() { }
        public MemoryStream DocumentFile { get; set; }
        public string Url { get; set; }
    }
}
