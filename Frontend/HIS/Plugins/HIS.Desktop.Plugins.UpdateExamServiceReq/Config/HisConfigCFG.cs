using HIS.Desktop.LocalStorage.BackendData;
using HIS.Desktop.LocalStorage.HisConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.UpdateExamServiceReq.Config
{
    class HisConfigCFG
    {
        private const string CONFIG_KEY__ALLOW_EDIT_WHEN_BILLED = "MOS.HIS_SERVICE_REQ.ALLOW_CHANGING_PAID_EXAM_IF_NOT_BHYT_AND_SAME_PRICE";
        private const string CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";

        internal static bool IS_ALLOW_EDIT_EXAM_HAS_BILL;
        internal static string IS_SET_PRIMARY_PATIENT_TYPE;

        internal static void LoadConfig()
        {
            try
            {
                IS_ALLOW_EDIT_EXAM_HAS_BILL = HisConfigs.Get<string>(CONFIG_KEY__ALLOW_EDIT_WHEN_BILLED) == "1";
                IS_SET_PRIMARY_PATIENT_TYPE = HisConfigs.Get<string>(CONFIG_KEY__IS_SET_PRIMARY_PATIENT_TYPE);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
        }

    }
}
