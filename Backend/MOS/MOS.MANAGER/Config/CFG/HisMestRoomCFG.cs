using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisMestRoomCFG
    {
        private static List<HIS_MEST_ROOM> data;
        public static List<HIS_MEST_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisMestRoomGet().Get(new HisMestRoomFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisMestRoomGet().Get(new HisMestRoomFilterQuery());
            data = tmp;
        }
    }
}
