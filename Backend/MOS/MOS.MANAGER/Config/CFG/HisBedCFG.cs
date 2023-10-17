using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisBed;
using MOS.MANAGER.HisBedRoom;
using MOS.MANAGER.HisExecuteRoom;
using MOS.MANAGER.HisMestRoom;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.UTILITY;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisBedCFG
    {
        private static List<HIS_BED> data;
        public static List<HIS_BED> DATA
        {
            get
            {
                if (data == null)
                {
                    HisBedFilterQuery filter = new HisBedFilterQuery();
                    filter.IS_ACTIVE = Constant.IS_TRUE;
                    data = new HisBedGet().Get(filter);
                }
                return data;
            }
        }

        public static void Reload()
        {
            HisBedFilterQuery filter = new HisBedFilterQuery();
            filter.IS_ACTIVE = Constant.IS_TRUE;
            data = new HisBedGet().Get(filter);
        }
    }
}
