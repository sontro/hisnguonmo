using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTestIndexRange;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisTestIndexRangeCFG
    {
        private static List<HIS_TEST_INDEX_RANGE> data;
        public static List<HIS_TEST_INDEX_RANGE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisTestIndexRangeGet().Get(new HisTestIndexRangeFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisTestIndexRangeGet().Get(new HisTestIndexRangeFilterQuery());
            data = tmp;
        }
    }
}
