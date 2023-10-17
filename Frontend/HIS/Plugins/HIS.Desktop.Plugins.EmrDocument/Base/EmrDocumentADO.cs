using EMR.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrDocument.Base
{
    public class EmrDocumentADO : V_EMR_DOCUMENT
    {
        public string DOCUMENT_DISPLAY { get; set; }
        public long CUSTOM_NUM_ORDER { get; set; }
        public string CUSTOM_BY_GROUP_NUM_ORDER { get; set; }
        public string PARENT_KEY { get; set; }
        public string CHILD_KEY { get; set; }
        public bool IsChecked { get; set; }
    }
}
