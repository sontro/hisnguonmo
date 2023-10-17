using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisServiceRoom;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceRoomCFG
    {
        private static List<V_HIS_SERVICE_ROOM> dataView;
        public static List<V_HIS_SERVICE_ROOM> DATA_VIEW
        {
            get
            {
                if (dataView == null)
                {
                    dataView = new HisServiceRoomGet().GetView(new HisServiceRoomViewFilterQuery());
                }
                return dataView;
            }
        }

        public static void Reload()
        {
            var tmp = new HisServiceRoomGet().GetView(new HisServiceRoomViewFilterQuery());
            dataView = tmp;
        }
    }
}
