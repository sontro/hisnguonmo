using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExecuteBedRoomSummary.SDO
{
    public class HisBedRoomCouterSDO : V_HIS_BED_ROOM
    {
        public long DEPARTMENT_ID { get; set; }
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public decimal TOTAL_TREATMENT_BED_ROOM { get; set; }
    }

}
