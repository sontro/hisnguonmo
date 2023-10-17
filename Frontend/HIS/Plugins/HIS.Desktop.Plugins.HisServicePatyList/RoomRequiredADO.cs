using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServicePatyList
{
    public class RoomRequiredADO : V_HIS_ROOM
    {
        public bool Check { get; set; }

        public RoomRequiredADO()
        {
        }

        public RoomRequiredADO(V_HIS_ROOM roomRequired)
        {
            if (roomRequired != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoomRequiredADO>(this, roomRequired);
            }
        }

        public RoomRequiredADO(V_HIS_ROOM roomRequired, List<long> roomRequiredIds)
        {
            if (roomRequired != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoomRequiredADO>(this, roomRequired);
                if (roomRequiredIds.Contains(roomRequired.ID))
                    this.Check = true;
            }
        }
    }
}
