using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceRereTime;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceRereTimeCFG
    {
        private static List<HIS_SERVICE_RERE_TIME> data;
        public static List<HIS_SERVICE_RERE_TIME> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisServiceRereTimeGet().Get(new HisServiceRereTimeFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisServiceRereTimeGet().Get(new HisServiceRereTimeFilterQuery());
            data = tmp;
        }
    }
}
