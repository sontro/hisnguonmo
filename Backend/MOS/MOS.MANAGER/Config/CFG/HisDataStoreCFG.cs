using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDataStore;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisDataStoreCFG
    {
        private static List<V_HIS_DATA_STORE> data;
        public static List<V_HIS_DATA_STORE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisDataStoreGet().GetView(new HisDataStoreViewFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisDataStoreGet().GetView(new HisDataStoreViewFilterQuery());
            data = tmp;
        }
    }
}
