using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CallPatientDepartment.ADO
{
    public class RoomADO : MOS.EFMODEL.DataModels.V_HIS_ROOM
    {
        public bool IsCheck { get; set; }
        public int CategoryChoose { get; set; }
    }
}
