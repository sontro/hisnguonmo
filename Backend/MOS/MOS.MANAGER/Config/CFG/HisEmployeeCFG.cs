using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisDataStore;
using MOS.MANAGER.HisEmployee;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    public class HisEmployeeCFG
    {
        private static List<HIS_EMPLOYEE> data;
        public static List<HIS_EMPLOYEE> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisEmployeeGet().Get(new HisEmployeeFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisEmployeeGet().Get(new HisEmployeeFilterQuery());
            data = tmp;
        }
    }
}
