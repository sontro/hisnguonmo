using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisReceptionRoom;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisReceptionRoomCFG
    {
        private static List<V_HIS_RECEPTION_ROOM> data;
        public static List<V_HIS_RECEPTION_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisReceptionRoomGet().GetView(new HisReceptionRoomViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisReceptionRoomGet().GetView(new HisReceptionRoomViewFilterQuery());
            data = tmp;
        }
    }
}
