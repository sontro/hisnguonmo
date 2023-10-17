using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisHeinServiceType;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisHeinServiceTypeCFG
    {
        private static List<HIS_HEIN_SERVICE_TYPE> data;
        public static List<HIS_HEIN_SERVICE_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisHeinServiceTypeGet().Get(new HisHeinServiceTypeFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisHeinServiceTypeGet().Get(new HisHeinServiceTypeFilterQuery());
            data = tmp;
        }
    }
}
