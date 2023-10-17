using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisRoomSaro;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisRoomSaroCFG
    {
        private static List<HIS_ROOM_SARO> data;
        public static List<HIS_ROOM_SARO> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisRoomSaroGet().Get(new HisRoomSaroFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisRoomSaroGet().Get(new HisRoomSaroFilterQuery());
            data = tmp;
        }
    }
}
