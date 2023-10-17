using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisSampleRoom;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisSampleRoomCFG
    {
        private static List<V_HIS_SAMPLE_ROOM> data;
        public static List<V_HIS_SAMPLE_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisSampleRoomGet().GetView(new HisSampleRoomViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisSampleRoomGet().GetView(new HisSampleRoomViewFilterQuery());
            data = tmp;
        }
    }
}
