using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisCashierAddConfig;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisCashierAddConfigCFG
    {
        private static List<HIS_CASHIER_ADD_CONFIG> data;
        public static List<HIS_CASHIER_ADD_CONFIG> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisCashierAddConfigGet().Get(new HisCashierAddConfigFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisCashierAddConfigGet().Get(new HisCashierAddConfigFilterQuery());
            data = tmp;
        }
    }
}
