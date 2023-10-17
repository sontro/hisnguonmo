using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SetupMedicineTypeWithAcin.ADO
{
    public class StatusADO
    {
        public long id { get; set; }
        public string statusName { get; set; }

        public StatusADO(long id, string statusName)
        {
            this.id = id;
            this.statusName = statusName;
        }
    }
}
