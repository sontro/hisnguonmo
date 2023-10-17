using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientExam.ADO
{
    public class RoomADO : MOS.EFMODEL.DataModels.HIS_EXECUTE_ROOM
    {
        public bool IsCheck { get; set; }
        public int CategoryChoose { get; set; }
    }
}
