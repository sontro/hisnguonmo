using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisPatientType;
using MOS.MANAGER.HisPatientTypeAllow;
using MOS.MANAGER.HisServicePaty;
using SDA.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.Config
{
    class HisServicePatyCFG
    {
        private static List<V_HIS_SERVICE_PATY> data;
        public static List<V_HIS_SERVICE_PATY> DATA
        {
            get
            {
                if (data == null)
                {
                    data = new HisServicePatyGet().GetView(new HisServicePatyViewFilterQuery());
                }
                return data;
            }
        }

        private static bool? hasPatientClassify;
        public static bool HAS_PATIENT_CLASSIFY
        {
            get
            {
                if (!hasPatientClassify.HasValue)
                {
                    hasPatientClassify = DATA != null && DATA.Count > 0 ? DATA.Exists(o => o.PATIENT_CLASSIFY_ID.HasValue) : false;
                }
                return hasPatientClassify.Value;
            }
        }

        public static void Reload()
        {
            var tmp = new HisServicePatyGet().GetView(new HisServicePatyViewFilterQuery());
            hasPatientClassify = DATA != null && DATA.Count > 0 ? DATA.Exists(o => o.PATIENT_CLASSIFY_ID.HasValue) : false;
            data = tmp;
        }
    }
}
