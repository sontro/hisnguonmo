using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportTreatmentRoom.ADO
{
    public class TreatmentADO : MOS.EFMODEL.DataModels.HIS_TREATMENT_ROOM
    {
        public string BED_ROOM_CODE { get; set; }
        public string TREATMENT_ROOM_NAME { get; set; }
        public string ERROR { get; set; }
    }
}
