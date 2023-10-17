using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class TextLibraryInfoADO
    {
        public bool IsNotSaveTemplate { get; set; }
        public bool IsFindTemplate { get; set; }
        public bool IsFillHashtag { get; set; }
        public bool IsFillContent { get; set; }
        public string Content { get; set; }
        public string Hashtag { get; set; }
    }
}
