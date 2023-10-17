using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.BedRoomPartial.ADO
{
    class PatientFilterADO
    {
        public long ID { get; set; }

        public string PatientFilter { get; set; }

        public PatientFilterADO(long id, string filterPatient)
        {
            this.ID = id;
            this.PatientFilter = filterPatient;
        }
    }
}
