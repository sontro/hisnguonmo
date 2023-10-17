using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCashierRoom;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisCashierRoomCFG
    {
        private static List<V_HIS_CASHIER_ROOM> data;
        public static List<V_HIS_CASHIER_ROOM> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisCashierRoomGet().GetView(new HisCashierRoomViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisCashierRoomGet().GetView(new HisCashierRoomViewFilterQuery());
            data = tmp;
        }
    }
}
