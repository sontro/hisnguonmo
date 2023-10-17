using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisUserRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisUserRoomCFG
    {
        private static List<HIS_USER_ROOM> data;
        public static List<HIS_USER_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisUserRoomGet().Get(new HisUserRoomFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisUserRoomGet().Get(new HisUserRoomFilterQuery());
            data = tmp;
        }
    }
}
