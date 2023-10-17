using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.ADO
{
    public class BedLogADO
    {
        public L_HIS_TREATMENT_BED_ROOM vHisTreatmentBedRoom { get; set; }
        public V_HIS_BED_LOG vHisBedLog { get; set; }

        public BedLogADO(L_HIS_TREATMENT_BED_ROOM _treatmentBedRoom, V_HIS_BED_LOG _bedLog)
        {
            this.vHisTreatmentBedRoom = _treatmentBedRoom;
            this.vHisBedLog = _bedLog;
        }
    }
}
