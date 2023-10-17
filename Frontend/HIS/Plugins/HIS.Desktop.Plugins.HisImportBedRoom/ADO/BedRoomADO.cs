using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisImportBedRoom.ADO
{
    public class BedRoomADO : MOS.EFMODEL.DataModels.HIS_BED_ROOM
    {
        public string DEPARTMENT_CODE { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public long DEPARTMENT_ID { get; set; }
        public string IS_SURGERY_STR { get; set; }
        public bool IS_SURGERY_DISPLAY { get; set; }
        public string ERROR { get; set; }
    }
}
