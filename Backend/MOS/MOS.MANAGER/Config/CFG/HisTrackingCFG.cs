using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    class HisTrackingCFG
    {
        private const string UpdateTreatmentIcdCFG = "HIS.Desktop.Plugins.TrackingCreate.UpdateTreatmentIcd";
        private const string SERVICE_REQ_ICD_OPTION_CFG = "HIS.HIS_TRACKING.SERVICE_REQ_ICD_OPTION";

        private static bool? updateTreatmentIcd;
        public static bool UpdateTreatmentIcd
        {
            get
            {
                if (!updateTreatmentIcd.HasValue)
                {
                    updateTreatmentIcd = ConfigUtil.GetIntConfig(UpdateTreatmentIcdCFG) == 1;
                }
                return updateTreatmentIcd.Value;
            }
            set
            {
                updateTreatmentIcd = value;
            }
        }

        private static bool? ServiceReqIcdOption;
        public static bool SERVICE_REQ_ICD_OPTION
        {
            get
            {
                if (!ServiceReqIcdOption.HasValue)
                {
                    ServiceReqIcdOption = ConfigUtil.GetIntConfig(SERVICE_REQ_ICD_OPTION_CFG) == 1;
                }
                return ServiceReqIcdOption.Value;
            }
            set
            {
                ServiceReqIcdOption = value;
            }
        }
        public static void Reload()
        {
            updateTreatmentIcd = ConfigUtil.GetIntConfig(UpdateTreatmentIcdCFG) == 1;
            ServiceReqIcdOption = ConfigUtil.GetIntConfig(SERVICE_REQ_ICD_OPTION_CFG) == 1;
        }
    }
}
