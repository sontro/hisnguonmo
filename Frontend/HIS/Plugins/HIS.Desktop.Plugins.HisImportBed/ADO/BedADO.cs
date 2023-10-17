using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportBed.ADO
{
    public class BedADO : MOS.EFMODEL.DataModels.HIS_BED
    {
        public string BED_ROOM_CODE { get; set; }
        public string BED_TYPE_CODE { get; set; }
        public string ERROR { get; set; }
        public string MAX_CAPACITY_STR { get; set; }
        public string TREATMENT_ROOM_CODE { get; set; }
    }
}
