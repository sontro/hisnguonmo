using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.Config
{
    class HisPatientTypeAllowCFG
    {
        private static List<HIS_PATIENT_TYPE_ALLOW> data;
        public static List<HIS_PATIENT_TYPE_ALLOW> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisPatientTypeAllowGet().Get(new HisPatientTypeAllowFilterQuery());
                }
                return data;
            }
        }

        public static void Reload()
        {
            var tmp = new HisPatientTypeAllowGet().Get(new HisPatientTypeAllowFilterQuery());
            data = tmp;
        }
    }
}
