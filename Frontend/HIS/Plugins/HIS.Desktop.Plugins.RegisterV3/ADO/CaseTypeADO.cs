using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV3.ADO
{
    class CaseTypeADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public CaseTypeADO(long Id, string name)
        {
            this.ID = Id;
            this.NAME = name;
        }
    }
}
