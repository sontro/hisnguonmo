namespace MOS.MANAGER.Config
{
    class HisPrescriptionCFG
    {
        /// <summary>
        /// Option su dung cho 2 key:
        /// - MOS.HIS_PRESCRIPTION.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT
        /// - MOS.HIS_PRESCRIPTION.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT
        /// </summary>
        public enum AppliedOption
        {
            /// <summary>
            /// Ko chan
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Áp dụng chặn cho cả điều trị nội trú và điều trị ngoại trú
            /// </summary>
            FOR_ALL = 1,
            /// <summary>
            /// Chỉ áp dụng cho điều trị nội trú
            /// </summary>
            FOR_IN = 2,
            /// <summary>
            /// Bắt buộc xuất hết thuốc trước khi kết thúc điều trị đối với BN điều trị nội trú/điều trị ngoại trú/điều trị ban ngày
            /// </summary>
            FOR_EX = 3,
            /// <summary>
            /// Bắt buộc xuất hết thuốc trước khi kết thúc điều trị đối với BN điều trị nội trú/điều trị ngoại trú ngoại trừ trường hợp “Cấp cứu”
            /// </summary>
            FOR_NN = 4,
            /// <summary>
            /// điều trị nội trú thì bắt buộc xuất hết thuốc trước khi kết thúc điều trị
            /// </summary>
            FOR_NT = 5,


        }

        private const string MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT_CFG = "MOS.HIS_PRESCRIPTION.MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT";

        private const string MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT_CFG = "MOS.HIS_PRESCRIPTION.MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT";

        private static AppliedOption? mustExportBeforeOutOfDepartmentWithStayInPatient;
        public static AppliedOption MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT
        {
            get
            {
                if (!mustExportBeforeOutOfDepartmentWithStayInPatient.HasValue)
                {
                    mustExportBeforeOutOfDepartmentWithStayInPatient = (AppliedOption) ConfigUtil.GetIntConfig(MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT_CFG);
                }
                return mustExportBeforeOutOfDepartmentWithStayInPatient.Value;
            }
        }

        private static AppliedOption? mustExportBeforeFinishingWithStayInPatient;
        public static AppliedOption MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT
        {
            get
            {
                if (!mustExportBeforeFinishingWithStayInPatient.HasValue)
                {
                    mustExportBeforeFinishingWithStayInPatient = (AppliedOption) ConfigUtil.GetIntConfig(MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT_CFG);
                }
                return mustExportBeforeFinishingWithStayInPatient.Value;
            }
        }

        public static void Reload()
        {
            mustExportBeforeOutOfDepartmentWithStayInPatient = (AppliedOption) ConfigUtil.GetIntConfig(MUST_EXPORT_BEFORE_OUT_OF_DEPARTMENT_WITH_STAY_IN_PATIENT_CFG);

            mustExportBeforeFinishingWithStayInPatient = (AppliedOption) ConfigUtil.GetIntConfig(MUST_EXPORT_BEFORE_FINISHING_WITH_STAY_IN_PATIENT_CFG);
        }
    }
}
