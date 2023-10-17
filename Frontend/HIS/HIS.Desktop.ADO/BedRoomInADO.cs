using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class BedRoomInADO
    {
        public long TreatmentId { get; set; }
        public BedRoomInADO(long treatmentId)
        {
            this.TreatmentId = treatmentId;
        }
    }
}
