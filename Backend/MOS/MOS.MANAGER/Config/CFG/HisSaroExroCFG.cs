using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisSaroExro;
using MOS.MANAGER.HisService;
using MOS.MANAGER.HisTestIndex;
using MOS.MANAGER.HisTestIndexRange;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisSaroExroCFG
    {
        private static List<HIS_SARO_EXRO> data;
        public static List<HIS_SARO_EXRO> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisSaroExroGet().Get(new HisSaroExroFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisSaroExroGet().Get(new HisSaroExroFilterQuery());
            data = tmp;
        }
    }
}
