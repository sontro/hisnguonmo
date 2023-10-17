using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using MOS.MANAGER.HisStation;

namespace MOS.MANAGER.Config
{
    class HisStationCFG
    {
        private static List<V_HIS_STATION> data;
        public static List<V_HIS_STATION> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisStationGet().GetView(new HisStationViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisStationGet().GetView(new HisStationViewFilterQuery());
            data = tmp;
        }
    }
}
