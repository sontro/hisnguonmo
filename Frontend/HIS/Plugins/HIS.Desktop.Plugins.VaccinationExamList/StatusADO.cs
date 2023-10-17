using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.VaccinationExamList
{
    class StatusADO
    {
        public long ID { get; set; }
        public string StatusName { get; set; }

        public StatusADO(long id, string StatusName)
        {
            this.ID = id;
            this.StatusName = StatusName;
        }
    }
}
