using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceChangeReqList.ADO
{
    class RoomADO
    {
        public long ID { get; set; }
        public string CODE { get; set; }
        public string NAME { get; set; }

        public RoomADO(HIS_EXECUTE_ROOM data)
        {
            if (data != null)
            {
                this.CODE = data.EXECUTE_ROOM_CODE;
                this.ID = data.ROOM_ID;
                this.NAME = data.EXECUTE_ROOM_NAME;
            }
        }

        public RoomADO(HIS_BED_ROOM data)
        {
            if (data != null)
            {
                this.CODE = data.BED_ROOM_CODE;
                this.ID = data.ROOM_ID;
                this.NAME = data.BED_ROOM_NAME;
            }
        }
    }
}
