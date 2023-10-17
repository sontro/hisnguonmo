

namespace MOS.MANAGER.Config
{
    class HisSereServCFG
    {
        /// <summary>
        /// Chon cac he thong LIS tich hop
        /// </summary>
        public enum SetPrimaryPatientType
        {
            /// <summary>
            /// Ko cho phep set
            /// </summary>
            NO = 0,
            /// <summary>
            /// Cho phep set nhung ko tu dong
            /// </summary>
            YES = 1,
            /// <summary>
            /// Tu dong set
            /// </summary>
            AUTO = 2
        }

        /// <summary>
        /// Cho phep tick "khong thuc hien" doi voi cac dich vu da thanh toan
        /// </summary>
        public enum AllowNoExecuteForPaidServiceOption
        {
            /// <summary>
            /// Khong cho phep
            /// </summary>
            DO_NOT_ALLOW = 0,
            /// <summary>
            /// Chi cho phep trong truong hop tam thu
            /// </summary>
            ALLOW_FOR_DIPOSIT = 1
        }

        /// <summary>
        /// Cho phep tick "khong thuc hien" doi voi cac dich vu da thanh toan
        /// </summary>
        public enum ApplyBhytExamPolictyForNonBhytOption
        {
            /// <summary>
            /// Ap dung ngoai tru cau hinh cong kham thu 2 cho BN noi tru
            /// </summary>
            EXCEPT_2TH_EXAM_IN_PATIENT_POLICY = 1,

            /// <summary>
            /// Ap dung tat ca
            /// </summary>
            APPLY_ALL = 2,

            /// <summary>
            /// Khong ap dung
            /// </summary>
            NONE = 0
        }

        /// <summary>
        /// Cac tuy chon doi voi nghiep vu xu ly nguon chi tra khac doi voi 
        /// cac dich vu/thuoc co cau hinh "co nguon chi tra khac" trong danh muc
        /// </summary>
        public enum OtherSourcePaidServiceOption
        {
            /// <summary>
            /// Chi thanh toan cho BHYT
            /// </summary>
            BHYT = 1,

            /// <summary>
            /// Thanh toan cho ca BHYT va khong phai BHYT
            /// </summary>
            ALL = 2,

            /// <summary>
            /// Thanh toan cua ca BHYT va ko phai BHYT va luon thanh toan 100% (chu ko chi moi phan dong chi tra)
            /// </summary>
            PAID_ALL = 3
        }

        public enum SetExpendForAutoExpendPresOption
        {
            /// <summary>
            /// Chi set hao phi neu doi tuong thanh toan cua dich vu la BHYT
            /// </summary>
            BHYT = 1,

            /// <summary>
            /// Luon set hao phi voi moi doi tuong thanh toan cua dich vu
            /// </summary>
            ALL = 2,
        }

        public enum SetExpendForAutoSetNoExecuteForRepayOption
        {
            /// <summary>
            /// Chi xet cac dich vu la thuoc/ vat tu/ mau
            /// </summary>
            NO_EXECUTE_FOR_TH_VT_AND_MAU = 1,

            /// <summary>
            /// Chi xet cac dich vu la mau
            /// </summary>
            NO_EXECUTE_FOR_MAU = 2,
        }

        public enum IsApplyArisingSurgPricePolicyForNonBhytOption
        {
            /// <summary>
            ///  Dịch vụ cha có đối tượng thanh toán không phải là BHYT
            /// </summary>
            BHYT = 1,

            /// <summary>
            /// Áp dụng với tất cả đối tượng thanh toán của dịch vụ cha.
            /// </summary>
            ALL = 2,
        }

        private const string AUTO_SET_NO_EXECUTE_FOR_REPAY_CFG = "MOS.HIS_SERE_SERV.AUTO_SET_NO_EXECUTE_FOR_REPAY";
        //Ti le tinh tien cong kham thu 2 doi voi BN vien phi (dich vu)
        private const string RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE_CFG = "MOS.HIS_SERE_SERV.RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE";
        //Chi tinh tien 1 cong kham doi voi BN nhap vien vao noi tru
        private const string CALC_1_EXAM_FOR_IN_PATIENT_CFG = "MOS.HIS_SERE_SERV.CALC_1_EXAM_FOR_IN_PATIENT";
        private const string TEST_SERVICE__EXECUTING__IS_ALLOW_CHECK_IS_NO_EXECUTE = "HIS.Desktop.Plugins.Bordereau.AllowCheckIsNoExecute";
        //Su dung doi tuong phu thu
        private const string SET_PRIMARY_PATIENT_TYPE_CFG = "MOS.HIS_SERE_SERV.IS_SET_PRIMARY_PATIENT_TYPE";
        //Su dung chi phi giuong tam tinh
        private const string IS_USING_BED_TEMP_CFG = "MOS.HIS_SERE_SERV.IS_USING_BED_TEMP";
        //Co ap dung chinh sach tinh gia cho PTTT phat sinh cho doi tuong thanh toan ko phai la BHYT hay ko
        private const string IS_APPLY_ARISING_SURG_PRICE_POLICY_FOR_NON_BHYT_CFG = "MOS.HIS_SERE_SERV.IS_APPLY_ARISING_SURG_PRICE_POLICY_FOR_NON_BHYT";
        //Tuy chon ap dung chinh sach tinh tinh gia kham cho doi tuong ko phai BHYT tuong tu nhu doi tuong BHYT hay khong
        private const string APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION_CFG = "MOS.HIS_SERE_SERV.APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION";

        private const string ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION_CFG = "MOS.HIS_SERE_SERV.ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION";
        //Bat buoc bs dong y truoc khi chuyen trang thai "khong thuc hien"
        private const string MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE_CFG = "MOS.HIS_SERE_SERV.MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE";

        //Bat buoc bs dong y truoc khi chuyen trang thai "khong thuc hien"
        private const string OTHER_SOURCE_PAID_SERVICE_OPTION_CFG = "MOS.HIS_SERE_SERV.OTHER_SOURCE_PAID_SERVICE_OPTION";

        //Tuy chon tu dong set "hao phi" trong truong hop tu dong ke don thuoc/vat tu tieu hao khi xu ly dich vu
        private const string AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION_CFG = "MOS.HIS_SERE_SERV.AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION";
        private const string IS_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED_CFG = "MOS.HIS_SERE_SERV.DO_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED";
        private const string IS_VACCINE_EXP_PRICE_OPTION_CFG = "MOS.HIS_SERE_SERV.VACCINE_EXP_PRICE_OPTION";
        //Cấu hình cho phép sửa bảng kê thanh toán với thuốc/vật tư
        //1: Chỉ cho phép sửa khi phiếu xuất chưa thực xuất
        private const string DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH_CFG = "MOS.HIS_SERE_SERV.DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH";

        private static SetExpendForAutoExpendPresOption? autoSetExpendForAutoExpendPresOption;
        public static SetExpendForAutoExpendPresOption AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION
        {
            get
            {
                if (!autoSetExpendForAutoExpendPresOption.HasValue)
                {
                    autoSetExpendForAutoExpendPresOption = (SetExpendForAutoExpendPresOption)ConfigUtil.GetIntConfig(AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION_CFG);
                }
                return autoSetExpendForAutoExpendPresOption.Value;
            }
        }

        private static SetExpendForAutoSetNoExecuteForRepayOption? autoSetNoExecuteForRepay;
        public static SetExpendForAutoSetNoExecuteForRepayOption AUTO_SET_NO_EXECUTE_FOR_REPAY
        {
            get
            {
                if (!autoSetNoExecuteForRepay.HasValue)
                {
                    autoSetNoExecuteForRepay = (SetExpendForAutoSetNoExecuteForRepayOption)ConfigUtil.GetIntConfig(AUTO_SET_NO_EXECUTE_FOR_REPAY_CFG);
                }
                return autoSetNoExecuteForRepay.Value;
            }
        }

        private static bool? mustBeAcceptedBeforeSettingNoExecute;
        public static bool MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE
        {
            get
            {
                if (!mustBeAcceptedBeforeSettingNoExecute.HasValue)
                {
                    mustBeAcceptedBeforeSettingNoExecute = ConfigUtil.GetIntConfig(MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE_CFG) == 1;
                }
                return mustBeAcceptedBeforeSettingNoExecute.Value;
            }
        }

        private static AllowNoExecuteForPaidServiceOption? allowNoExecuteForPaidServiceOption;
        public static AllowNoExecuteForPaidServiceOption ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION
        {
            get
            {
                if (!allowNoExecuteForPaidServiceOption.HasValue)
                {
                    allowNoExecuteForPaidServiceOption = (AllowNoExecuteForPaidServiceOption)ConfigUtil.GetIntConfig(ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION_CFG);
                }
                return allowNoExecuteForPaidServiceOption.Value;
            }
        }

        private static OtherSourcePaidServiceOption? otherSourcePaidServiceOption;
        public static OtherSourcePaidServiceOption OTHER_SOURCE_PAID_SERVICE_OPTION
        {
            get
            {
                if (!otherSourcePaidServiceOption.HasValue)
                {
                    otherSourcePaidServiceOption = (OtherSourcePaidServiceOption)ConfigUtil.GetIntConfig(OTHER_SOURCE_PAID_SERVICE_OPTION_CFG);
                }
                return otherSourcePaidServiceOption.Value;
            }
        }

        private static SetPrimaryPatientType? setPrimaryPatientType;
        public static SetPrimaryPatientType? SET_PRIMARY_PATIENT_TYPE
        {
            get
            {
                if (!setPrimaryPatientType.HasValue)
                {
                    setPrimaryPatientType = (SetPrimaryPatientType)ConfigUtil.GetIntConfig(SET_PRIMARY_PATIENT_TYPE_CFG);
                }
                return setPrimaryPatientType;
            }
        }

        private static bool? calc1ExamForInPatient;
        public static bool CALC_1_EXAM_FOR_IN_PATIENT
        {
            get
            {
                if (!calc1ExamForInPatient.HasValue)
                {
                    calc1ExamForInPatient = ConfigUtil.GetIntConfig(CALC_1_EXAM_FOR_IN_PATIENT_CFG) == 1;
                }
                return calc1ExamForInPatient.Value;
            }
        }

        private static IsApplyArisingSurgPricePolicyForNonBhytOption? isApplyArisingSurgPricePolicyForNonBhyt;
        public static IsApplyArisingSurgPricePolicyForNonBhytOption IS_APPLY_ARISING_SURG_POLICY_FOR_NON_BHYT
        {
            get
            {
                if (!isApplyArisingSurgPricePolicyForNonBhyt.HasValue)
                {
                    isApplyArisingSurgPricePolicyForNonBhyt = (IsApplyArisingSurgPricePolicyForNonBhytOption)ConfigUtil.GetIntConfig(IS_APPLY_ARISING_SURG_PRICE_POLICY_FOR_NON_BHYT_CFG);
                }
                return isApplyArisingSurgPricePolicyForNonBhyt.Value;
            }
        }

        private static ApplyBhytExamPolictyForNonBhytOption? applyBhytExamPolictyForNonBhytOption;
        public static ApplyBhytExamPolictyForNonBhytOption APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION
        {
            get
            {
                if (!applyBhytExamPolictyForNonBhytOption.HasValue)
                {
                    applyBhytExamPolictyForNonBhytOption = (ApplyBhytExamPolictyForNonBhytOption)ConfigUtil.GetIntConfig(APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION_CFG);
                }
                return applyBhytExamPolictyForNonBhytOption.Value;
            }
        }

        private static bool? isUsingBedTemp;
        public static bool IS_USING_BED_TEMP
        {
            get
            {
                if (!isUsingBedTemp.HasValue)
                {
                    isUsingBedTemp = ConfigUtil.GetIntConfig(IS_USING_BED_TEMP_CFG) == 1;
                }
                return isUsingBedTemp.Value;
            }
        }

        private static decimal? ratioFor2ndExamForHospitalFee;
        public static decimal? RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE
        {
            get
            {
                if (!ratioFor2ndExamForHospitalFee.HasValue)
                {
                    ratioFor2ndExamForHospitalFee = ConfigUtil.GetDecimalConfig(RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE_CFG);
                }
                return ratioFor2ndExamForHospitalFee.Value;
            }
        }

        private static bool? isAllowCheckIsNoExecute;
        public static bool TEST__IS_ALLOW_CHECK_IS_NO_EXECUTE
        {
            get
            {
                if (!isAllowCheckIsNoExecute.HasValue)
                {
                    isAllowCheckIsNoExecute = ConfigUtil.GetIntConfig(TEST_SERVICE__EXECUTING__IS_ALLOW_CHECK_IS_NO_EXECUTE) == 1;
                }
                return isAllowCheckIsNoExecute.Value;
            }
        }

        private static bool? isNotAllowToUpdateWhenTreatmentIsFinished;
        public static bool IS_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED
        {
            get
            {
                if (!isNotAllowToUpdateWhenTreatmentIsFinished.HasValue)
                {
                    isNotAllowToUpdateWhenTreatmentIsFinished = ConfigUtil.GetIntConfig(IS_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED_CFG) == 1;
                }
                return isNotAllowToUpdateWhenTreatmentIsFinished.Value;
            }
        }

        private static bool? isVaccineExpPriceOption;
        public static bool IS_VACCINE_EXP_PRICE_OPTION
        {
            get
            {
                if (!isVaccineExpPriceOption.HasValue)
                {
                    isVaccineExpPriceOption = ConfigUtil.GetIntConfig(IS_VACCINE_EXP_PRICE_OPTION_CFG) == 1;
                }
                return isVaccineExpPriceOption.Value;
            }
        }

        private static bool? doNotAllowToUpdateWhenExpMestFinish;
        public static bool DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH
        {
            get
            {
                if (!doNotAllowToUpdateWhenExpMestFinish.HasValue)
                {
                    doNotAllowToUpdateWhenExpMestFinish = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH_CFG) == 1;
                }
                return doNotAllowToUpdateWhenExpMestFinish.Value;
            }
        }

        public static void Reload()
        {
            autoSetNoExecuteForRepay = (SetExpendForAutoSetNoExecuteForRepayOption)ConfigUtil.GetIntConfig(AUTO_SET_NO_EXECUTE_FOR_REPAY_CFG);
            ratioFor2ndExamForHospitalFee = ConfigUtil.GetDecimalConfig(RATIO_FOR_2ND_EXAM_FOR_HOSPITAL_FEE_CFG);
            isAllowCheckIsNoExecute = ConfigUtil.GetIntConfig(TEST_SERVICE__EXECUTING__IS_ALLOW_CHECK_IS_NO_EXECUTE) == 1;
            setPrimaryPatientType = (SetPrimaryPatientType)ConfigUtil.GetIntConfig(SET_PRIMARY_PATIENT_TYPE_CFG);
            calc1ExamForInPatient = ConfigUtil.GetIntConfig(CALC_1_EXAM_FOR_IN_PATIENT_CFG) == 1;
            isUsingBedTemp = ConfigUtil.GetIntConfig(IS_USING_BED_TEMP_CFG) == 1;
            isApplyArisingSurgPricePolicyForNonBhyt = (IsApplyArisingSurgPricePolicyForNonBhytOption)ConfigUtil.GetIntConfig(IS_APPLY_ARISING_SURG_PRICE_POLICY_FOR_NON_BHYT_CFG);
            applyBhytExamPolictyForNonBhytOption = (ApplyBhytExamPolictyForNonBhytOption)ConfigUtil.GetIntConfig(APPLY_BHYT_EXAM_POLICY_FOR_NON_BHYT_OPTION_CFG);
            allowNoExecuteForPaidServiceOption = (AllowNoExecuteForPaidServiceOption)ConfigUtil.GetIntConfig(ALLOW_NO_EXECUTE_FOR_PAID_SERVICE_OPTION_CFG);
            mustBeAcceptedBeforeSettingNoExecute = ConfigUtil.GetIntConfig(MUST_BE_ACCEPTED_BEFORE_SETING_NO_EXECUTE_CFG) == 1;
            otherSourcePaidServiceOption = (OtherSourcePaidServiceOption)ConfigUtil.GetIntConfig(OTHER_SOURCE_PAID_SERVICE_OPTION_CFG);
            autoSetExpendForAutoExpendPresOption = (SetExpendForAutoExpendPresOption)ConfigUtil.GetIntConfig(AUTO_SET_EXPEND_FOR_AUTO_EXPEND_PRES_OPTION_CFG);
            isNotAllowToUpdateWhenTreatmentIsFinished = ConfigUtil.GetIntConfig(IS_NOT_ALLOW_TO_UPDATE_WHEN_TREATMENT_IS_FINISHED_CFG) == 1;
            isVaccineExpPriceOption = ConfigUtil.GetIntConfig(IS_VACCINE_EXP_PRICE_OPTION_CFG) == 1;
            doNotAllowToUpdateWhenExpMestFinish = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_UPDATE_WHEN_EXP_MEST_FINISH_CFG) == 1;
        }
    }
}