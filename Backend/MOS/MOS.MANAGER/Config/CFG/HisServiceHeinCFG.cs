using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceHein;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceHeinCFG
    {
        private static List<HIS_SERVICE_HEIN> data;
        public static List<HIS_SERVICE_HEIN> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisServiceHeinGet().Get(new HisServiceHeinFilterQuery());
                }
                return data;
            }
        }

        public static void Reset()
        {
            data = null;
        }

        public static void Reload()
        {
            var tmp = new HisServiceHeinGet().Get(new HisServiceHeinFilterQuery());
            data = tmp;
        }
    }
}
