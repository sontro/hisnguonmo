using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoom;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.UTILITY;

namespace MOS.MANAGER.Config
{
    class HisRoomCFG
    {
        private const string UPDATE_RESPONSIBILITY_USER_OF_ROOM_CFG = "MOS.HIS_ROOM.UPDATE_RESPONSIBILITY_USER_OF_ROOM";

        private static bool? updateResponsibilityUserOfRoom;
        public static bool UPDATE_RESPONSIBILITY_USER_OF_ROOM
        {
            get
            {
                if (!updateResponsibilityUserOfRoom.HasValue)
                {
                    updateResponsibilityUserOfRoom = ConfigUtil.GetIntConfig(UPDATE_RESPONSIBILITY_USER_OF_ROOM_CFG) == 1;
                }
                return updateResponsibilityUserOfRoom.Value;
            }
        }

        private static List<V_HIS_ROOM> data;
        public static List<V_HIS_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisRoomGet().GetView(new HisRoomViewFilterQuery());
                }
                return data;
            }
        }

        private static List<long> blockNumOrderRoomIds;
        public static List<long> BLOCK_NUM_ORDER_ROOM_IDs
        {
            get
            {
                if (blockNumOrderRoomIds == null)
                {
                    blockNumOrderRoomIds = DATA != null ? data.Where(o => o.IS_BLOCK_NUM_ORDER == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
                }
                return blockNumOrderRoomIds;
            }
        }

        public static void Reload()
        {
            updateResponsibilityUserOfRoom = ConfigUtil.GetIntConfig(UPDATE_RESPONSIBILITY_USER_OF_ROOM_CFG) == 1;
            var tmp = new HisRoomGet().GetView(new HisRoomViewFilterQuery());
            data = tmp;

            blockNumOrderRoomIds = DATA != null ? data.Where(o => o.IS_BLOCK_NUM_ORDER == Constant.IS_TRUE).Select(o => o.ID).ToList() : null;
        }
    }
}
