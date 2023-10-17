using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.PublicMedicineByPhased.ADO
{
    public class HisPatientTypeADO
    {
        public long ID { get; set; }
        public string NAME { get; set; }

        public HisPatientTypeADO(long _ID, string _NAME)
        {
            this.ID = _ID;
            this.NAME = _NAME;
        }
        public HisPatientTypeADO() { }
    }
}
