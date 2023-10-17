using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceType;
using SDA.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisServiceTypeCFG
    {
        private static List<HIS_SERVICE_TYPE> data;
        public static List<HIS_SERVICE_TYPE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisServiceTypeGet().Get(new HisServiceTypeFilterQuery());
                }
                return data;
            }
        }

        //Dich vu can lam sang
        public static List<long> SUBCLINICAL_TYPE_IDs = new List<long>(){
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__CDHA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__GPBL,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__NS,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__SA,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__TDCN,
                IMSys.DbConfig.HIS_RS.HIS_SERVICE_TYPE.ID__XN,
            };

        public static void Reload()
        {
            var tmp = new HisServiceTypeGet().Get(new HisServiceTypeFilterQuery());
            data = tmp;
        }
    }
}
