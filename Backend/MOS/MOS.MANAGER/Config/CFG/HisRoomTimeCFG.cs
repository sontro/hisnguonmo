using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoomTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisRoomTimeCFG
    {
        private static List<V_HIS_ROOM_TIME> data;
        public static List<V_HIS_ROOM_TIME> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisRoomTimeGet().GetView(new HisRoomTimeViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisRoomTimeGet().GetView(new HisRoomTimeViewFilterQuery());
            data = tmp;
        }
    }
}
