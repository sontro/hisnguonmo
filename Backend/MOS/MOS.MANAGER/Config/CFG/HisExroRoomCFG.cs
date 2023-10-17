using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExroRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisExroRoomCFG
    {
        private static List<V_HIS_EXRO_ROOM> data;
        public static List<V_HIS_EXRO_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisExroRoomGet().GetView(new HisExroRoomViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisExroRoomGet().GetView(new HisExroRoomViewFilterQuery());
            data = tmp;
        }
    }
}
