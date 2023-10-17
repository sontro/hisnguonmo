using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UploadFileAUP
{
    public class NodeADO
    {
        public NodeADO() { }
        public string Path { get; set; }
        public int ImageIndex { get; set; }
        public string ParentPath { get; set; }
        public string Name { get; set; }
        public bool IsFolder { get; set; }
    }
}
