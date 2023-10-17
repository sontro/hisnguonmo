using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.SDO
{
    public class HisTreatmentUpdatePatiSDO
    {
        public HIS_TREATMENT HisTreatment { get; set; }
        public bool? IsUpdateTreatmentUnLocked { get; set; }
        public bool? IsUpdateAllOtherTreatements { get; set; }
        public bool? IsUpdateEmr { get; set; }
    }
}
