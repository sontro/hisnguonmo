using Inventec.Common.LocalStorage.SdaConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.LocalStorage.SdaConfigKey.Config
{
    public class HisBillMustFinishTreatmentCFG
    {
        //cấu hình hồ sơ điều trị phải kết thúc mới cho thanh toán dịch vụ bhyt
        private const string HIS_BILL__MUST_FINISH_TREATMENT = "MOS.HIS_BILL.BHYT.MUST_FINISH_TREATMENT_BEFORE_BILLING";
        private const string IsFinishBeforeBill = "1";

        private static bool? mustFinishTreatmentForBill;
        public static bool MustFinishTreatmentForBill
        {
            get
            {
                if (!mustFinishTreatmentForBill.HasValue)
                {
                    mustFinishTreatmentForBill = Get(SdaConfigs.Get<string>(HIS_BILL__MUST_FINISH_TREATMENT));
                }
                return mustFinishTreatmentForBill.Value;
            }
        }

        static bool Get(string code)
        {
            bool result = false;
            try
            {
                if (!String.IsNullOrEmpty(code))
                {
                    result = (code == IsFinishBeforeBill);
                }
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = false;
            }
            return result;
        }
    }
}
