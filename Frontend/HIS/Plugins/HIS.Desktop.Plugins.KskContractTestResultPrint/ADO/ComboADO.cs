using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.KskContractTestResultPrint.ADO
{
    public class ComboADO
    {
        public long id { get; set; }
        public string statusName { get; set; }

        public ComboADO() { }
        public ComboADO(long id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }
    }
}
