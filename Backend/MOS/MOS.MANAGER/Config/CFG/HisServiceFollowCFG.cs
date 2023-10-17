using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceFollow;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceFollowCFG
    {
        private static List<HIS_SERVICE_FOLLOW> data;
        public static List<HIS_SERVICE_FOLLOW> DATA
        {
            get
            {
                if (data == null)
                {
                    HisServiceFollowFilterQuery filter = new HisServiceFollowFilterQuery();
                    filter.IS_ACTIVE = Constant.IS_TRUE;
                    data = new HisServiceFollowGet().Get(filter);
                }
                return data;
            }
        }

        public static void Reload()
        {
            HisServiceFollowFilterQuery filter = new HisServiceFollowFilterQuery();
            filter.IS_ACTIVE = Constant.IS_TRUE;
            var tmp = new HisServiceFollowGet().Get(filter);

            data = tmp;
        }
    }
}
