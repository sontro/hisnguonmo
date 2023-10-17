using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterExamKiosk.ADO
{
    public class CmktADO
    {
        public long ID { get; set; }
        public string MA_CMKT { get; set; }
        public string TEN_CMKT { get; set; }
        public CmktADO(long id, string code, string name)
        {
            this.ID = id;
            this.TEN_CMKT = name;
            this.MA_CMKT = code;
        }
    }
}
