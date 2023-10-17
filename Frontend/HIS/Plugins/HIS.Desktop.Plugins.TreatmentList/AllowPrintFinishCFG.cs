using HIS.Desktop.LocalStorage.HisConfig;
//using HIS.Desktop.LocalStorage.SdaConfigKey;
using Inventec.Common.LocalStorage.SdaConfig;
using Inventec.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.TreatmentList
{
    public class AllowPrintFinishCFG
    {
        public const string KEY_IN_CHUYEN_VIEN = "HIS.Desktop.IsAllowPrintTransferHospitalAfterLockFee";
        public const string KEY_IN_RA_VIEN = "HIS.Desktop.IsAllowPrintOutHospitalAfterLockFee";

        private static long CheckInRaVien;
        public static long ALLOW_PRINT_RA_VIEN
        {
            get
            {
                if (CheckInRaVien == 0)
                {
                    CheckInRaVien = GetValue(HisConfigs.Get<string>(KEY_IN_RA_VIEN));
                }
                return CheckInRaVien;
            }
            set
            {
                CheckInRaVien = value;
            }
        }

        private static long CheckInChuyenVien;
        public static long ALLOW_PRINT_CHUYEN_VIEN
        {
            get
            {
                if (CheckInChuyenVien == 0)
                {
                    CheckInChuyenVien = GetValue(HisConfigs.Get<string>(KEY_IN_CHUYEN_VIEN));
                }
                return CheckInChuyenVien;
            }
            set
            {
                CheckInChuyenVien = value;
            }
        }

        private static long GetValue(string code)
        {
            long result = 0;
            try
            {
                result = Inventec.Common.TypeConvert.Parse.ToInt64(code);
            }
            catch (Exception ex)
            {
                LogSystem.Debug(ex);
                result = 0;
            }
            return result;
        }
    }
}
