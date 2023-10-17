using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTestIndex;
using MOS.MANAGER.HisTestIndexRange;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisTestIndexCFG
    {
        private static List<V_HIS_TEST_INDEX> dataView;
        public static List<V_HIS_TEST_INDEX> DATA_VIEW
        {
            get
            {
                if (dataView == null)
                {
                    dataView = new HisTestIndexGet().GetView(new HisTestIndexViewFilterQuery());
                }
                return dataView;
            }
        }

        public static void Reload()
        {
            var tmp = new HisTestIndexGet().GetView(new HisTestIndexViewFilterQuery());
            dataView = tmp;
        }
    }
}
