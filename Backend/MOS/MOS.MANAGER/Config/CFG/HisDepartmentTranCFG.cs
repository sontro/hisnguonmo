

namespace MOS.MANAGER.Config
{
    class HisDepartmentTranCFG
    {
        /// <summary>
        /// Yeu cau chon giuong khi tiep nhan BN
        /// </summary>
        public enum RequiredBedWhenReceivePatientOption
        {
            /// <summary>
            /// Bat buoc voi ho so dieu tri noi tru
            /// </summary>
            IN_PATIENT = 1,
            /// <summary>
            /// Khong bat buoc
            /// </summary>
            NON = 0
        }

        private const string REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION_CFG = "MOS.HIS_DEPARTMENT_TRAN.REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION";
        private const string DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG = "MOS.HIS_DEPARTMENT_TRAN.DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME";

        private static RequiredBedWhenReceivePatientOption? requiredBedWhenReceivePatientOption;
        public static RequiredBedWhenReceivePatientOption REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION
        {
            get
            {
                if (!requiredBedWhenReceivePatientOption.HasValue)
                {
                    requiredBedWhenReceivePatientOption = (RequiredBedWhenReceivePatientOption)ConfigUtil.GetIntConfig(REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION_CFG);
                }
                return requiredBedWhenReceivePatientOption.Value;
            }
        }

        private static bool? isDepartmentInTimeNotGreaterThanCurrentTime;
        public static bool IS_DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG
        {
            get
            {
                if (!isDepartmentInTimeNotGreaterThanCurrentTime.HasValue)
                {
                    isDepartmentInTimeNotGreaterThanCurrentTime = ConfigUtil.GetIntConfig(DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG) == 1;
                }
                return isDepartmentInTimeNotGreaterThanCurrentTime.Value;
            }
        }

        public static void Reload()
        {
            requiredBedWhenReceivePatientOption = (RequiredBedWhenReceivePatientOption)ConfigUtil.GetIntConfig(REQUIRED_BED_WHEN_RECEIVE_PATIENT_OPTION_CFG);
            isDepartmentInTimeNotGreaterThanCurrentTime = ConfigUtil.GetIntConfig(DEPARTMENT_IN_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG) == 1;
        }
    }
}
