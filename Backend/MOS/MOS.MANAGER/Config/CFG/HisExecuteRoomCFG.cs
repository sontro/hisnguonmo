using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisExecuteRoomCFG
    {
        private static List<V_HIS_EXECUTE_ROOM> data;
        public static List<V_HIS_EXECUTE_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisExecuteRoomGet().GetView(new HisExecuteRoomViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisExecuteRoomGet().GetView(new HisExecuteRoomViewFilterQuery());
            data = tmp;
        }
    }
}
