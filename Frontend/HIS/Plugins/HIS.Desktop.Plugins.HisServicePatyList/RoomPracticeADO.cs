using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServicePatyList
{
    public class RoomPracticeADO : V_HIS_ROOM
    {
        public bool Check { get; set; }

        public RoomPracticeADO()
        {
        }

        public RoomPracticeADO(V_HIS_ROOM roomPractice)
        {
            if (roomPractice != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoomPracticeADO>(this, roomPractice);
            }
        }

        public RoomPracticeADO(V_HIS_ROOM roomPractice, List<long> roomPraticeIds)
        {
            if (roomPractice != null)
            {
                Inventec.Common.Mapper.DataObjectMapper.Map<RoomPracticeADO>(this, roomPractice);
                if (roomPraticeIds.Contains(roomPractice.ID))
                    this.Check = true;
            }
        }
    }
}
