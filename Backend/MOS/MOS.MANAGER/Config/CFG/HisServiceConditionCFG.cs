using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceCondition;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceConditionCFG
    {
        private static List<HIS_SERVICE_CONDITION> data;
        public static List<HIS_SERVICE_CONDITION> DATA
        {
            get
            {
                if (data == null)
                {
                    HisServiceConditionFilterQuery filter = new HisServiceConditionFilterQuery();
                    filter.IS_ACTIVE = Constant.IS_TRUE;
                    data = new HisServiceConditionGet().Get(filter);
                }
                return data;
            }
        }

        public static void Reload()
        {
            HisServiceConditionFilterQuery filter = new HisServiceConditionFilterQuery();
            filter.IS_ACTIVE = Constant.IS_TRUE;
            var tmp = new HisServiceConditionGet().Get(filter);

            data = tmp;
        }
    }
}
