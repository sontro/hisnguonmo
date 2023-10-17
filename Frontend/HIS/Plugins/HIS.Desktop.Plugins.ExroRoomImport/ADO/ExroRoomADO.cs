using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ExroRoomImport.ADO
{
    public class ExroRoomADO : MOS.EFMODEL.DataModels.HIS_EXRO_ROOM
    {
        public string EXECUTE_ROOM_CODE { get; set; }
        public string ROOM_CODE { get; set; }
        public string ERROR { get; set; }
    }
}
