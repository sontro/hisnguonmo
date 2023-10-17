using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ChooseRoom
{
    public class RoomADO : V_HIS_ROOM
    {
        public bool IsChecked { get; set; }
        public long? DESK_ID { get; set; }
        public RoomADO()
            : base()
        {
            IsChecked = false;
        }

        public RoomADO(V_HIS_USER_ROOM userRoom)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<RoomADO>(this, userRoom);
            this.ID = userRoom.ROOM_ID;
            this.IsChecked = false;
        }

        public RoomADO(V_HIS_ROOM userRoom)
        {
            Inventec.Common.Mapper.DataObjectMapper.Map<RoomADO>(this, userRoom);
            this.IsChecked = false;
        }
    }
}
