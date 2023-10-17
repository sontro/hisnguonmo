using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisServiceReq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MOS.MANAGER.Config
{
    class HisServiceReqCFG
    {
        /// <summary>
        /// Chon cac he thong LIS tich hop
        /// </summary>
        public enum ManyDaysPrescriptionOption
        {
            /// <summary>
            /// Theo ca don
            /// </summary>
            BY_PRES = 1,
            /// <summary>
            /// Theo tung thuoc/vat tu trong don
            /// </summary>
            BY_ITEM = 2,
        }

        /// <summary>
        /// Cac tuy chon tach don thuoc theo nhom/loai thuoc/vat tu
        /// </summary>
        public enum SpitPresByByGroupOption
        {
            /// <summary>
            /// Tach thanh 4 loai: 
            /// - Gay nghien
            /// - Huong than
            /// - San pham ho tro (bao gom Vat tu + Thuc pham chuc nang)
            /// - Thuoc thuong (thuoc con lai)
            /// </summary>
            OPT1 = 1,
            /// <summary>
            /// Tach thanh 4 loai: 
            /// - Gay nghien
            /// - Huong than
            /// - San pham ho tro (Thuc pham chuc nang)
            /// - Thuoc thuong (thuoc con lai) + Vat tu
            /// </summary>
            OPT2 = 2,
        }

        /// <summary>
        ///1: Gay nghien, 2: Huong than, 3: san pham ho tro (thuc pham chuc nang + vat tu), 4: Thuoc thuong
        /// </summary>
        public enum PresGroup
        {
            GAY_NGHIEN = 1,
            HUONG_THAN = 2,
            HO_TRO = 3,
            THUONG = 4
        }


        /// <summary>
        ///1: Khong chan theo doi tuong BN,
        ///2: Khong chan theo doi tuong thanh toan la BHYT va co dong chi tra
        ///4: khong chan doi tuong dieu trị ngoai tru va noi tru
        ///Khac: chan
        /// </summary>
        public enum NotRequireFeeOption
        {
            BY_PATIENT_TYPE = 1,
            BY_PAYMENT_PATIENT_TYPE = 2,
            BY_PAYMENT_PATIENT_TYPE_AND_NOT_OUT_PATIENT = 3,//ko chan ca doi tuong dieu tri ngoai tru
            BY_IN_PATIENT_AND_OUT_PATIENT = 4,
        }

        public enum AllowModifyingStartedOption
        {
            ALL = 1,
            JUST_EXAM = 2
        }

        public enum ChangingExamOption
        {
            NO_CREATE_NEW_IF_PAID_SAME_PRICE_AND_NON_BHYT = 1,
            NO_CREATE_NEW_IF_UNPAID = 2,
            CREATE_NEW = 0
        }

        public enum NumOrderIssueOption
        {
            /// <summary>
            /// Theo thu tu tao, tao truoc thi co so thu tu nho
            /// </summary>
            BY_CREATING_ORDER = 1,

            /// <summary>
            /// Theo tung block thoi gian chon
            /// </summary>
            BY_BLOCK_TIME = 2
        }

        public enum GenrateBarcodeOption
        {
            NUMBER = 0,
            DAY_WITH_NUMBER = 1,
        }

        public enum AssignRoomByExecutedOption
        {
            PRIORITIZED_PREVIOUSLY_EXECUTED_ROOM = 1,
            EXACTLY_PREVIOUSLY_EXECUTED_ROOM = 2
        }

        public enum TestStartTimeOption
        {
            /// <summary>
            /// thoi gian bat dau y lenh(START_TIME) bang thoi gian lay mau SampleTime
            /// </summary>
            BY_SAMPLE_TIME = 1,

            /// <summary>
            /// thoi gian bat dau y lenh(START_TIME) bang thoi gian tiep nhan mau ReceiveSampleTime
            /// </summary>
            BY_RECEIVE_SAMPLE_TIME = 2
        }
        private const string NOT_REQUIRE_FEE_FOR_BHYT_CFG = "MOS.HIS_SERVICE_REQ.NOT_REQUIRE_FEE_FOR_BHYT";
        private const string ALLOW_MODIFYING_OF_STARTED_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_MODIFYING_OF_STARTED";
        private const string SPLIT_BY_PATIENT_TYPE_CFG = "MOS.HIS_SERVICE_REQ.SPLIT_BY_PATIENT_TYPE";
        private const string NUM_ORDER_ISSUE_OPTION_CFG = "MOS.HIS_SERVICE_REQ.NUM_ORDER_ISSUE_OPTION";

        //Tach rieng don ke ngoai kho
        private const string PRESCRIPTION_SPLIT_OUT_MEDISTOCK_CFG = "MOS.HIS_SERVICE_REQ.PRESCRIPTION_SPLIT_OUT_MEDISTOCK";
        //Tuy chon cach ke don nhieu ngay
        private const string MANY_DAYS_PRESCRIPTION_OPTION_CFG = "MOS.HIS_SERVICE_REQ.MANY_DAYS_PRESCRIPTION_OPTION";

        //Tu dong tao phieu xuat ban khi ke don ngoai kho
        private const string IS_AUTO_CREATE_SALE_EXP_MEST_CFG = "MOS.HIS_SERVICE_REQ.IS_AUTO_CREATE_SALE_EXP_MEST";

        //1: Không cho phòng khám xử lý vượt quá số lượng BN quy định (không tính BN nội trú chuyển khám chuyên khoa).
        //0: Không kiểm tra
        private const string CHECK_EXAM_ROOM_LIMIT_CFG = "MOS.HIS_SERVICE_REQ.CHECK_EXAM_ROOM_LIMIT";

        //1: Cho phep kham phu co the chuyen khoa, tich kham them nhu kham chinh
        private const string ALLOW_SUB_EXAM_CHANGE_DEPARTMENT_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_SUB_EXAM_CHANGE_DEPARTMENT";

        //1: Chi cho phep bac si chi dinh dich vu kỹ thuật (không tin giường, khám , khác)
        private const string JUST_ALLOW_DOCTOR_ASSIGN_SERVICE_CFG = "MOS.HIS_SERVICE_REQ.JUST_ALLOW_DOCTOR_ASSIGN_SERVICE";

        //1: Chi cho phep bac si ke don
        public const string JUST_ALLOW_DOCTOR_PRESCRIPTION_CFG = "MOS.HIS_SERVICE_REQ.JUST_ALLOW_DOCTOR_PRESCRIPTION";

        //1: Chi cho phep tai khoan co thong tin chung chi hanh nghe ra y lenh cho BN
        private const string REQ_USER_MUST_HAVE_DIPLOMA_CFG = "MOS.HIS_SERVICE_REQ.REQ_USER_MUST_HAVE_DIPLOMA";

        private const string INTEGRATED_DAY_NUMBER_SYNC_CFG = "MOS.INTEGRATED_SYSTEM.DAY_NUMBER.SYNC";

        //Cho phep xu ly cac y lenh can lam sang sau khi khoa ho so dieu tri hay khong
        private const string IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT_CFG = "MOS.HIS_SERVICE_REQ.IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT";

        //Cau hinh tuy chon che do sua cong kham
        private const string CHANGING_EXAM_OPTION_CFG = "MOS.HIS_SERVICE_REQ.CHANGING_EXAM_OPTION";

        /// <summary>
        /// Cho phep sua thong tin xu ly PTTT sau khi da khoa ho so dieu tri
        /// </summary>
        private const string ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT";

        /// <summary>
        /// Chi cho phep nguoi dung admin duoc phep huy ket thuc doi voi cac y/c kham 
        /// co thoi gian chi dinh truoc thoi gian nhap vien
        /// </summary>
        private const string ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION";

        /// <summary>
        /// Co kiem tra quyen cap nhat ket qua cua nguoi khac tra hay ko
        /// Neu co, chi cho phep nguoi duoc gan quyen duoc phep cap nhat ket qua dich vu cua nguoi khac tra
        /// </summary>
        private const string IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL_CFG = "MOS.HIS_SERVICE_REQ.IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL";

        /// <summary>
        /// Doi tuong benh nhan mac dinh khi tu dong tao phieu xuat ban khi ke don nha thuoc (ke don ngoai kho)
        /// </summary>
        private const string DEFAULT_DRUG_STORE_PATIENT_TYPE_CODE_CFG = "MOS.HIS_SERVICE_REQ.DEFAULT_DRUG_STORE_PATIENT_TYPE_CODE";

        /// <summary>
        /// Các STT "để dành" ngăn cách nhau bằng dấu phẩy. Hệ thống khi tự sinh STT sẽ không bỏ qua các STT này
        /// </summary>
        private const string RESERVED_NUM_ORDER_CFG = "MOS.HIS_SERVICE_REQ.RESERVED_NUM_ORDER";

        /// <summary>
        /// Su dung STT khac doi voi cac chi dinh "uu tien"
        /// </summary>
        private const string IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED_CFG = "MOS.HIS_SERVICE_REQ.IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED";

        /// <summary>
        /// Cau hinh tach don theo nhom (GN, HT, TPCN, ...) (chi su dung doi voi don phong kham)
        /// </summary>
        private const string SPLIT_PRES_BY_GROUP_OPTION_CFG = "MOS.HIS_SERVICE_REQ.SPLIT_PRES_BY_GROUP_OPTION";

        /// <summary>
        /// Tu dong chuyen doi tuong thanh toan tu BHYT sang vien phi neu thuoc duoc ke vuot qua han thau 1 so ngay quy dinh
        /// </summary>
        private const string AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED_CFG = "MOS.HIS_SERVICE_REQ.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED";

        /// <summary>
        /// So ngay quy dinh ap dung cho key cau hinh MOS.HIS_SERVICE_REQ.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED
        /// </summary>
        private const string AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY_CFG = "MOS.HIS_SERVICE_REQ.AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED.NUMBER_OF_DAY";

        private const string MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION_CFG = "MOS.HIS_SERVICE_REQ.MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION";

        /// <summary>
        /// Bat buoc phai co "chung chi hanh nghe" moi duoc phep xu ly kham
        /// </summary>
        private const string EXAM_USER_MUST_HAS_DIPLOMA_CFG = "MOS.HIS_SERVICE_REQ.EXAM_USER_MUST_HAS_DIPLOMA";

        private const string DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER_CFG = "MOS.DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER";

        private const string AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE_CFG = "MOS.HIS_SERVICE_REQ.AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE";

        /// <summary>
        /// 1:Tự động cập nhật công khám xử trí ra viện là công khám chính
        /// </summary>
        private const string AUTO_SET_MAIN_EXAM_WHICH_FINISH_CFG = "MOS.HIS_SERVICE_REQ.AUTO_SET_MAIN_EXAM_WHICH_FINISH";

        private const string DO_NOT_FINISH_TEST_SERVICE_REQ_WHEN_RECEIVING_RESULT_CFG = "MOS.HIS_SERVICE_REQ.DO_NOT_FINISH_TEST_SERVICE_REQ_WHEN_RECEIVING_RESULT";

        private const string LIS_SID_LENGTH_CFG = "MOS.HIS_SERVICE_REQ.LIS_SID_LENGTH";

        private const string DO_NOT_ALLOW_TO_EDIT_IF_PAID_CFG = "MOS.HIS_SERVICE_REQ.DO_NOT_ALLOW_TO_EDIT_IF_PAID";

        private const string UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST_CFG = "MOS.HIS_SERVICE_REQ.UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST";

        private const string IS_USING_SUB_PRESCRIPTION_MECHANISM_CFG = "MOS.HIS_SERVICE_REQ.IS_USING_SUB_PRESCRIPTION_MECHANISM";

        private const string SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ_CFG = "HIS.Desktop.Plugins.SurgServiceReqExecute.TakeIntrucionTimeByServiceReq";
        private const string IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE_CFG = "MOS.HIS_SERVICE_REQ.IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE";
        private const string IS_PRESCRIPTION_MEST_ROOM_OPTION_CFG = "MOS.HIS_SERVICE_REQ.PRESCRIPTION.MEST_ROOM_OPTION";
        private const string IS_ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD_CFG = "MOS.HIS_SERVISE_REQ.ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD";
        private const string IS_ALLOW_ASSIGN_OXYGEN_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_ASSIGN_OXYGEN";
        private const string GENERATE_BARCODE_OPTION_CFG = "MOS.HIS_SERVICE_REQ.GENERATE_BARCODE.OPTION";
        private const string IS_ASSIGN_ROOM_BY_EXECUTED_CFG = "MOS.HIS_SERVICE_REQ.ASSIGN_ROOM_BY_EXECUTED";
        private const string IS_ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR_CFG = "MOS.HIS_SERVICE_REQ.ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR";
        private const string IS_ALLOW_AUTO_ADD_RATION_SUM_CFG = "MOS.HIS_SERVICE_REQ.AUTO_ADD_RATION_SUM";
        private const string AUTO_CREATE_RATION_WITH_TIME_OPTION_CFG = "MOS.HIS_SERVICE_REQ.AUTO_CREATE_RATION.TIME_OPTION";
        private const string IS_CHECK_SIMULTANEITY_OPTION_CFG = "MOS.HIS_SERVICE_REQ.CHECK_SIMULTANEITY_OPTION";
        private const string ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID_CFG = "MOS.HIS_SERVICE_REQ.ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID";
        private const string DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE_CFG = "MOS.HIS_SERVICE_REQ.DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE";

        /// <summary>
        /// Khong cho phep ke don co thoi gian y lenh nam ngoai khoang thoi gian hieu luc cua thau 
        /// </summary>
        private const string DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE_CFG = "MOS.SERVICE_REQ.DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE";

        private const string KIOT_AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT_CFG = "MOS.HIS_SERVICE_REQ.KIOSK.AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT";

        private const string TEST_START_TIME_OPTION_CFG = "MOS.HIS_SERVICE_REQ.TEST_START_TIME_OPTION";

        private const string AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_CODE_CFG = "MOS.HIS_SERVICE_REQ.AUTO_ADD_EXCUTE_ROLE.SERVICE_REQ_TYPE_CODE";

        private static ManyDaysPrescriptionOption manyDaysPrescriptionOption;
        public static ManyDaysPrescriptionOption MANY_DAYS_PRESCRIPTION_OPTION
        {
            get
            {
                if (manyDaysPrescriptionOption == 0)
                {
                    manyDaysPrescriptionOption = (ManyDaysPrescriptionOption)ConfigUtil.GetIntConfig(MANY_DAYS_PRESCRIPTION_OPTION_CFG);
                }
                return manyDaysPrescriptionOption;
            }
        }

        private static SpitPresByByGroupOption spitPresByByGroupOption;
        public static SpitPresByByGroupOption SPLIT_PRES_BY_GROUP_OPTION
        {
            get
            {
                if (spitPresByByGroupOption == 0)
                {
                    spitPresByByGroupOption = (SpitPresByByGroupOption)ConfigUtil.GetIntConfig(SPLIT_PRES_BY_GROUP_OPTION_CFG);
                }
                return spitPresByByGroupOption;
            }
        }

        private static NumOrderIssueOption numOrderIssueOption;
        public static NumOrderIssueOption NUM_ORDER_ISSUE_OPTION
        {
            get
            {
                if (numOrderIssueOption == 0)
                {
                    numOrderIssueOption = (NumOrderIssueOption)ConfigUtil.GetIntConfig(NUM_ORDER_ISSUE_OPTION_CFG);
                }
                return numOrderIssueOption;
            }
        }

        private static long defaultDrugStorePatientTypeId;
        public static long DEFAULT_DRUG_STORE_PATIENT_TYPE_ID
        {
            get
            {
                if (defaultDrugStorePatientTypeId == 0)
                {
                    defaultDrugStorePatientTypeId = GetPatientTypeId(DEFAULT_DRUG_STORE_PATIENT_TYPE_CODE_CFG);
                }
                return defaultDrugStorePatientTypeId;
            }
        }

        private static int maxSuspendingDayAllowedForInPatientPrescription;
        public static int MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION
        {
            get
            {
                if (maxSuspendingDayAllowedForInPatientPrescription == 0)
                {
                    maxSuspendingDayAllowedForInPatientPrescription = ConfigUtil.GetIntConfig(MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION_CFG);
                }
                return maxSuspendingDayAllowedForInPatientPrescription;
            }
        }

        private static int lisSidLength;
        public static int LIS_SID_LENGTH
        {
            get
            {
                if (lisSidLength == 0)
                {
                    lisSidLength = ConfigUtil.GetIntConfig(LIS_SID_LENGTH_CFG);
                }
                return lisSidLength;
            }
        }

        private static bool? isUsingSubPrescriptionMechanism;
        public static bool IS_USING_SUB_PRESCRIPTION_MECHANISM
        {
            get
            {
                if (!isUsingSubPrescriptionMechanism.HasValue)
                {
                    isUsingSubPrescriptionMechanism = ConfigUtil.GetIntConfig(IS_USING_SUB_PRESCRIPTION_MECHANISM_CFG) == 1;
                }
                return isUsingSubPrescriptionMechanism.Value;
            }
        }

        private static bool? autoSetMainExamWhichHospitalize;
        public static bool AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE
        {
            get
            {
                if (!autoSetMainExamWhichHospitalize.HasValue)
                {
                    autoSetMainExamWhichHospitalize = ConfigUtil.GetIntConfig(AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE_CFG) == 1;
                }
                return autoSetMainExamWhichHospitalize.Value;
            }
        }

        private static bool? autoSetMainExamWhichFinish;
        public static bool AUTO_SET_MAIN_EXAM_WHICH_FINISH
        {
            get
            {
                if (!autoSetMainExamWhichFinish.HasValue)
                {
                    autoSetMainExamWhichFinish = ConfigUtil.GetIntConfig(AUTO_SET_MAIN_EXAM_WHICH_FINISH_CFG) == 1;
                }
                return autoSetMainExamWhichFinish.Value;
            }
        }

        private static bool? allowOnlyAdminUnfinishExamWhichBeforeHospitalization;
        public static bool ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION
        {
            get
            {
                if (!allowOnlyAdminUnfinishExamWhichBeforeHospitalization.HasValue)
                {
                    allowOnlyAdminUnfinishExamWhichBeforeHospitalization = ConfigUtil.GetIntConfig(ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION_CFG) == 1;
                }
                return allowOnlyAdminUnfinishExamWhichBeforeHospitalization.Value;
            }
        }

        private static bool? doNotAllowToEditIfPaid;
        public static bool DO_NOT_ALLOW_TO_EDIT_IF_PAID
        {
            get
            {
                if (!doNotAllowToEditIfPaid.HasValue)
                {
                    doNotAllowToEditIfPaid = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_EDIT_IF_PAID_CFG) == 1;
                }
                return doNotAllowToEditIfPaid.Value;
            }
        }

        private static bool? autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded;
        public static bool AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED
        {
            get
            {
                if (!autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded.HasValue)
                {
                    autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded = ConfigUtil.GetIntConfig(AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED_CFG) == 1;
                }
                return autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded.Value;
            }
        }

        private static int autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded_NumberOfDay;
        public static int AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY
        {
            get
            {
                if (autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded_NumberOfDay == 0)
                {
                    autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded_NumberOfDay = ConfigUtil.GetIntConfig(AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY_CFG);
                }
                return autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded_NumberOfDay;
            }
        }

        private static bool? isCheckingPermissionOfResultingSubclinical;
        public static bool IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL
        {
            get
            {
                if (!isCheckingPermissionOfResultingSubclinical.HasValue)
                {
                    isCheckingPermissionOfResultingSubclinical = ConfigUtil.GetIntConfig(IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL_CFG) == 1;
                }
                return isCheckingPermissionOfResultingSubclinical.Value;
            }
        }

        private static bool? isAutoCreateSaleExpMest;
        public static bool IS_AUTO_CREATE_SALE_EXP_MEST
        {
            get
            {
                if (!isAutoCreateSaleExpMest.HasValue)
                {
                    isAutoCreateSaleExpMest = ConfigUtil.GetIntConfig(IS_AUTO_CREATE_SALE_EXP_MEST_CFG) == 1;
                }
                return isAutoCreateSaleExpMest.Value;
            }
        }

        private static bool? updateStatusAlongWithSaleExpMest;
        public static bool UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST
        {
            get
            {
                if (!updateStatusAlongWithSaleExpMest.HasValue)
                {
                    updateStatusAlongWithSaleExpMest = ConfigUtil.GetIntConfig(UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST_CFG) == 1;
                }
                return updateStatusAlongWithSaleExpMest.Value;
            }
        }

        private static bool? isUsingOtherNumOrderForPrioritized;
        public static bool IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED
        {
            get
            {
                if (!isUsingOtherNumOrderForPrioritized.HasValue)
                {
                    isUsingOtherNumOrderForPrioritized = ConfigUtil.GetIntConfig(IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED_CFG) == 1;
                }
                return isUsingOtherNumOrderForPrioritized.Value;
            }
        }

        private static ChangingExamOption? changeExamOption;
        public static ChangingExamOption CHANGING_EXAM_OPTION
        {
            get
            {
                if (!changeExamOption.HasValue)
                {
                    changeExamOption = (ChangingExamOption)ConfigUtil.GetIntConfig(CHANGING_EXAM_OPTION_CFG);
                }
                return changeExamOption.Value;
            }
        }

        private static NotRequireFeeOption? notRequireFeeForBhyt;
        public static NotRequireFeeOption NOT_REQUIRE_FEE_FOR_BHYT
        {
            get
            {
                if (!notRequireFeeForBhyt.HasValue)
                {
                    notRequireFeeForBhyt = (NotRequireFeeOption)ConfigUtil.GetIntConfig(NOT_REQUIRE_FEE_FOR_BHYT_CFG);
                }
                return notRequireFeeForBhyt.Value;
            }
        }

        private static bool? reqUserMustHaveDiploma;
        public static bool REQ_USER_MUST_HAVE_DIPLOMA
        {
            get
            {
                if (!reqUserMustHaveDiploma.HasValue)
                {
                    reqUserMustHaveDiploma = ConfigUtil.GetIntConfig(REQ_USER_MUST_HAVE_DIPLOMA_CFG) == 1;
                }
                return reqUserMustHaveDiploma.Value;
            }
        }

        private static bool? splitByPatientType;
        public static bool SPLIT_BY_PATIENT_TYPE
        {
            get
            {
                if (!splitByPatientType.HasValue)
                {
                    splitByPatientType = ConfigUtil.GetIntConfig(SPLIT_BY_PATIENT_TYPE_CFG) == 1;
                }
                return splitByPatientType.Value;
            }
        }

        private static bool? prescriptionSplitOutMediStock;
        public static bool PRESCRIPTION_SPLIT_OUT_MEDISTOCK
        {
            get
            {
                if (!prescriptionSplitOutMediStock.HasValue)
                {
                    prescriptionSplitOutMediStock = ConfigUtil.GetIntConfig(PRESCRIPTION_SPLIT_OUT_MEDISTOCK_CFG) == 1;
                }
                return prescriptionSplitOutMediStock.Value;
            }
        }

        private static bool? checkExamRoomLimit;
        public static bool CHECK_EXAM_ROOM_LIMIT
        {
            get
            {
                if (!checkExamRoomLimit.HasValue)
                {
                    checkExamRoomLimit = ConfigUtil.GetIntConfig(CHECK_EXAM_ROOM_LIMIT_CFG) == 1;
                }
                return checkExamRoomLimit.Value;
            }
        }

        private static AllowModifyingStartedOption? allowModifyingOfStarted;
        public static AllowModifyingStartedOption ALLOW_MODIFYING_OF_STARTED
        {
            get
            {
                if (!allowModifyingOfStarted.HasValue)
                {
                    allowModifyingOfStarted = (AllowModifyingStartedOption)ConfigUtil.GetIntConfig(ALLOW_MODIFYING_OF_STARTED_CFG);
                }
                return allowModifyingOfStarted.Value;
            }
        }

        private static bool? allowSubExamChangeDepartment;
        public static bool ALLOW_SUB_EXAM_CHANGE_DEPARTMENT
        {
            get
            {
                if (!allowSubExamChangeDepartment.HasValue)
                {
                    allowSubExamChangeDepartment = ConfigUtil.GetIntConfig(ALLOW_SUB_EXAM_CHANGE_DEPARTMENT_CFG) == 1;
                }
                return allowSubExamChangeDepartment.Value;
            }
        }

        private static bool? justAllowDoctorAssignService;
        public static bool JUST_ALLOW_DOCTOR_ASSIGN_SERVICE
        {
            get
            {
                if (!justAllowDoctorAssignService.HasValue)
                {
                    justAllowDoctorAssignService = ConfigUtil.GetIntConfig(JUST_ALLOW_DOCTOR_ASSIGN_SERVICE_CFG) == 1;
                }
                return justAllowDoctorAssignService.Value;
            }
        }

        private static int? justAllowDoctorPrecription;
        public static int? JUST_ALLOW_DOCTOR_PRESCRIPTION
        {
            get
            {
                if (!justAllowDoctorPrecription.HasValue)
                {
                    justAllowDoctorPrecription = ConfigUtil.GetIntConfig(JUST_ALLOW_DOCTOR_PRESCRIPTION_CFG);
                }
                return justAllowDoctorPrecription;
            }
        }

        private static int? integrateSystemDayNumSync;
        public static int INTEGRATE_SYSTEM_DAY_NUM_SYNC
        {
            get
            {
                if (!integrateSystemDayNumSync.HasValue)
                {
                    integrateSystemDayNumSync = ConfigUtil.GetIntConfig(INTEGRATED_DAY_NUMBER_SYNC_CFG);
                }
                return integrateSystemDayNumSync.Value;
            }
        }

        private static bool? isAllowingProcessingSubclinicalAfterLockingTreatment;
        public static bool IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT
        {
            get
            {
                if (!isAllowingProcessingSubclinicalAfterLockingTreatment.HasValue)
                {
                    isAllowingProcessingSubclinicalAfterLockingTreatment = ConfigUtil.GetIntConfig(IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT_CFG) == 1;
                }
                return isAllowingProcessingSubclinicalAfterLockingTreatment.Value;
            }
            set
            {
                isAllowingProcessingSubclinicalAfterLockingTreatment = value;
            }
        }

        private static bool? allowUpdateSurgInfoAfterLockingTreatment;
        public static bool ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT
        {
            get
            {
                if (!allowUpdateSurgInfoAfterLockingTreatment.HasValue)
                {
                    allowUpdateSurgInfoAfterLockingTreatment = ConfigUtil.GetIntConfig(ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT_CFG) == 1;
                }
                return allowUpdateSurgInfoAfterLockingTreatment.Value;
            }
            set
            {
                allowUpdateSurgInfoAfterLockingTreatment = value;
            }
        }

        private static bool? examUserMustHasDiploma;
        public static bool EXAM_USER_MUST_HAS_DIPLOMA
        {
            get
            {
                if (!examUserMustHasDiploma.HasValue)
                {
                    examUserMustHasDiploma = ConfigUtil.GetIntConfig(EXAM_USER_MUST_HAS_DIPLOMA_CFG) == 1;
                }
                return examUserMustHasDiploma.Value;
            }
            set
            {
                examUserMustHasDiploma = value;
            }
        }

        private static bool? doNotAllowToProcessExecuteAssignedServiceReqByAnother;
        public static bool DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER
        {
            get
            {
                if (!doNotAllowToProcessExecuteAssignedServiceReqByAnother.HasValue)
                {
                    doNotAllowToProcessExecuteAssignedServiceReqByAnother = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER_CFG) == 1;
                }
                return doNotAllowToProcessExecuteAssignedServiceReqByAnother.Value;
            }
        }

        private static long GetPatientTypeId(string code)
        {
            long result = -1;//de chi thuc hien load 1 lan
            try
            {
                string value = ConfigUtil.GetStrConfig(code);
                if (value != null)
                {
                    var data = HisPatientTypeCFG.DATA.Where(o => o.PATIENT_TYPE_CODE == value).FirstOrDefault();
                    if (data == null) throw new ArgumentNullException(code);
                    result = data.ID;
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static string reservedNumOrder;
        public static string RESERVED_NUM_ORDER
        {
            get
            {
                if (reservedNumOrder == null)
                {
                    reservedNumOrder = ConfigUtil.GetStrConfig(RESERVED_NUM_ORDER_CFG);
                }
                return reservedNumOrder;
            }
        }

        public static bool? DoNotFinishTestServiceReqWhenReceivingResult(long branchId)
        {
            bool result = false;
            try
            {
                List<HIS_CONFIG> configs = Loader.GetConfig(DO_NOT_FINISH_TEST_SERVICE_REQ_WHEN_RECEIVING_RESULT_CFG, branchId);
                if (configs != null && configs.Count > 0)
                {
                    HIS_CONFIG config = configs.Where(o => o.BRANCH_ID == branchId).FirstOrDefault();
                    if (config == null)
                    {
                        config = configs.Where(o => !o.BRANCH_ID.HasValue).FirstOrDefault();
                    }

                    if (config != null)
                    {
                        string configValue = !String.IsNullOrEmpty(config.VALUE) ? config.VALUE : (!String.IsNullOrEmpty(config.DEFAULT_VALUE) ? config.DEFAULT_VALUE : "");
                        result = configValue == "1";
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static bool? surgExecuteTakeIntrucionTimeByServiceReq;
        public static bool SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ
        {
            get
            {
                if (!surgExecuteTakeIntrucionTimeByServiceReq.HasValue)
                {
                    surgExecuteTakeIntrucionTimeByServiceReq = ConfigUtil.GetIntConfig(SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ_CFG) == 1;
                }
                return surgExecuteTakeIntrucionTimeByServiceReq.Value;
            }
            set
            {
                surgExecuteTakeIntrucionTimeByServiceReq = value;
            }
        }

        private static bool? isSplitBloodPrescriptionByType;
        public static bool IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE
        {
            get
            {
                if (!isSplitBloodPrescriptionByType.HasValue)
                {
                    isSplitBloodPrescriptionByType = ConfigUtil.GetIntConfig(IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE_CFG) == 1;
                }
                return isSplitBloodPrescriptionByType.Value;
            }
        }

        private static bool? isPrescriptionMestRoomOption;
        public static bool IS_PRESCRIPTION_MEST_ROOM_OPTION
        {
            get
            {
                if (!isPrescriptionMestRoomOption.HasValue)
                {
                    isPrescriptionMestRoomOption = ConfigUtil.GetIntConfig(IS_PRESCRIPTION_MEST_ROOM_OPTION_CFG) == 1;
                }
                return isPrescriptionMestRoomOption.Value;
            }
        }

        private static bool? isAllowProcessingTestServiceReqWhenApproveBlood;
        public static bool IS_ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD
        {
            get
            {
                if (!isAllowProcessingTestServiceReqWhenApproveBlood.HasValue)
                {
                    isAllowProcessingTestServiceReqWhenApproveBlood = ConfigUtil.GetIntConfig(IS_ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD_CFG) == 1;
                }
                return isAllowProcessingTestServiceReqWhenApproveBlood.Value;
            }
        }

        private static bool? isAllowAssignOxygen;
        public static bool IS_ALLOW_ASSIGN_OXYGEN
        {
            get
            {
                if (!isAllowAssignOxygen.HasValue)
                {
                    isAllowAssignOxygen = ConfigUtil.GetIntConfig(IS_ALLOW_ASSIGN_OXYGEN_CFG) == 1;
                }
                return isAllowAssignOxygen.Value;
            }
        }

        private static GenrateBarcodeOption? genrateBarcodeOption;
        public static GenrateBarcodeOption GENERATE_BARCODE_OPTION
        {
            get
            {
                if (!genrateBarcodeOption.HasValue)
                {
                    genrateBarcodeOption = (GenrateBarcodeOption)ConfigUtil.GetIntConfig(GENERATE_BARCODE_OPTION_CFG);
                }
                return genrateBarcodeOption.Value;
            }
        }

        private static AssignRoomByExecutedOption? assignRoomByExecutedOption;
        public static AssignRoomByExecutedOption ASSIGN_ROOM_BY_EXECUTED_OPTION
        {
            get
            {
                if (!assignRoomByExecutedOption.HasValue)
                {
                    assignRoomByExecutedOption = (AssignRoomByExecutedOption)ConfigUtil.GetIntConfig(IS_ASSIGN_ROOM_BY_EXECUTED_CFG);
                }
                return assignRoomByExecutedOption.Value;
            }
        }

        private static bool? isAllowFinishWhenAccountIsDoctor;
        public static bool IS_ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR
        {
            get
            {
                if (!isAllowFinishWhenAccountIsDoctor.HasValue)
                {
                    isAllowFinishWhenAccountIsDoctor = ConfigUtil.GetIntConfig(IS_ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR_CFG) == 1;
                }
                return isAllowFinishWhenAccountIsDoctor.Value;
            }
        }

        private static bool? isAllowAutoAddRationSum;
        public static bool IS_ALLOW_AUTO_ADD_RATION_SUM
        {
            get
            {
                if (!isAllowAutoAddRationSum.HasValue)
                {
                    isAllowAutoAddRationSum = ConfigUtil.GetIntConfig(IS_ALLOW_AUTO_ADD_RATION_SUM_CFG) == 1;
                }
                return isAllowAutoAddRationSum.Value;
            }
        }

        private static string autoCreateRationWithTimeOption;
        public static string AUTO_CREATE_RATION_WITH_TIME_OPTION
        {
            get
            {
                if (autoCreateRationWithTimeOption == null)
                {
                    autoCreateRationWithTimeOption = ConfigUtil.GetStrConfig(AUTO_CREATE_RATION_WITH_TIME_OPTION_CFG);
                }
                return autoCreateRationWithTimeOption;
            }
        }

        private static bool? isCheckSimultaneity;
        public static bool IS_CHECK_SIMULTANEITY
        {
            get
            {
                if (!isCheckSimultaneity.HasValue)
                {
                    isCheckSimultaneity = ConfigUtil.GetIntConfig(IS_CHECK_SIMULTANEITY_OPTION_CFG) == 1;
                }
                return isCheckSimultaneity.Value;
            }
        }

        private static string icdCodeToPayRestrictPatientTypeByOtherSourcePaid;
        public static string ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID
        {
            get
            {
                if (icdCodeToPayRestrictPatientTypeByOtherSourcePaid == null)
                {
                    icdCodeToPayRestrictPatientTypeByOtherSourcePaid = ConfigUtil.GetStrConfig(ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID_CFG);
                }
                return icdCodeToPayRestrictPatientTypeByOtherSourcePaid;
            }
        }

        private static bool? doNotAllowPresWhenIntructionTimeOutOfBidValidDate;
        public static bool DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE
        {
            get
            {
                if (doNotAllowPresWhenIntructionTimeOutOfBidValidDate == null)
                {
                    doNotAllowPresWhenIntructionTimeOutOfBidValidDate = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE_CFG) == 1;
                }
                return doNotAllowPresWhenIntructionTimeOutOfBidValidDate.Value;
            }
        }

        private static bool? KiotAutoRequireFeeIncaseOfExamHasAttachment;
        public static bool KIOT_AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT
        {
            get
            {
                if (!KiotAutoRequireFeeIncaseOfExamHasAttachment.HasValue)
                {
                    KiotAutoRequireFeeIncaseOfExamHasAttachment = ConfigUtil.GetIntConfig(KIOT_AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT_CFG) == 1;
                }
                return KiotAutoRequireFeeIncaseOfExamHasAttachment.Value;
            }
        }

        private static bool? DoNotCheckMinProcessTimeExamInCaseOfHospitalize;
        public static bool DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE
        {
            get
            {
                if (!DoNotCheckMinProcessTimeExamInCaseOfHospitalize.HasValue)
                {
                    DoNotCheckMinProcessTimeExamInCaseOfHospitalize = ConfigUtil.GetIntConfig(DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE_CFG) == 1;
                }
                return DoNotCheckMinProcessTimeExamInCaseOfHospitalize.Value;
            }
        }
        private static TestStartTimeOption testStartTimeOption;
        public static TestStartTimeOption TEST_START_TIME_OPTION
        {
            get
            {
                if (testStartTimeOption == 0)
                {
                    testStartTimeOption = (TestStartTimeOption)ConfigUtil.GetIntConfig(TEST_START_TIME_OPTION_CFG);
                }
                return testStartTimeOption;
            }
        }

        private static List<long> AutoAddExcuteRoleServiceReqTypeId;
        public static List<long> AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_ID
        {
            get
            {
                if (AutoAddExcuteRoleServiceReqTypeId == null)
                {
                    AutoAddExcuteRoleServiceReqTypeId = GetIds(AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_CODE_CFG);
                }
                return AutoAddExcuteRoleServiceReqTypeId;
            }
        }

        private static List<long> GetIds(string code)
        {
            List<long> result = null;
            try
            {
                List<string> codes = ConfigUtil.GetStrConfigs(code);
                if (codes != null)
                {
                    result = HisServiceReqTypeCFG.DATA.Where(o => codes.Contains(o.SERVICE_REQ_TYPE_CODE)).Select(o => o.ID).ToList();
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public static void Reload()
        {
            examUserMustHasDiploma = ConfigUtil.GetIntConfig(EXAM_USER_MUST_HAS_DIPLOMA_CFG) == 1;
            notRequireFeeForBhyt = (NotRequireFeeOption)ConfigUtil.GetIntConfig(NOT_REQUIRE_FEE_FOR_BHYT_CFG);
            splitByPatientType = ConfigUtil.GetIntConfig(SPLIT_BY_PATIENT_TYPE_CFG) == 1;
            allowModifyingOfStarted = (AllowModifyingStartedOption)ConfigUtil.GetIntConfig(ALLOW_MODIFYING_OF_STARTED_CFG);
            prescriptionSplitOutMediStock = ConfigUtil.GetIntConfig(PRESCRIPTION_SPLIT_OUT_MEDISTOCK_CFG) == 1;
            checkExamRoomLimit = ConfigUtil.GetIntConfig(CHECK_EXAM_ROOM_LIMIT_CFG) == 1;
            justAllowDoctorAssignService = ConfigUtil.GetIntConfig(JUST_ALLOW_DOCTOR_ASSIGN_SERVICE_CFG) == 1;
            justAllowDoctorPrecription = ConfigUtil.GetIntConfig(JUST_ALLOW_DOCTOR_PRESCRIPTION_CFG);
            integrateSystemDayNumSync = ConfigUtil.GetIntConfig(INTEGRATED_DAY_NUMBER_SYNC_CFG);
            isAllowingProcessingSubclinicalAfterLockingTreatment = ConfigUtil.GetIntConfig(IS_ALLOWING_PROCESSING_SUBCLINICAL_AFTER_LOCKING_TREATMENT_CFG) == 1;
            changeExamOption = (ChangingExamOption)ConfigUtil.GetIntConfig(CHANGING_EXAM_OPTION_CFG);
            allowUpdateSurgInfoAfterLockingTreatment = ConfigUtil.GetIntConfig(ALLOW_UPDATE_SURG_INFO_AFTER_LOCKING_TREATMENT_CFG) == 1;
            allowOnlyAdminUnfinishExamWhichBeforeHospitalization = ConfigUtil.GetIntConfig(ALLOW_ONLY_ADMIN_UNFINISH_EXAM_WHICH_BEFORE_HOSPITALIZATION_CFG) == 1;
            reqUserMustHaveDiploma = ConfigUtil.GetIntConfig(REQ_USER_MUST_HAVE_DIPLOMA_CFG) == 1;
            isAutoCreateSaleExpMest = ConfigUtil.GetIntConfig(IS_AUTO_CREATE_SALE_EXP_MEST_CFG) == 1;
            isSplitBloodPrescriptionByType = ConfigUtil.GetIntConfig(IS_SPLIT_BLOOD_PRESCRIPTION_BY_TYPE_CFG) == 1;
            isPrescriptionMestRoomOption = ConfigUtil.GetIntConfig(IS_PRESCRIPTION_MEST_ROOM_OPTION_CFG) == 1;
            manyDaysPrescriptionOption = (ManyDaysPrescriptionOption)ConfigUtil.GetIntConfig(MANY_DAYS_PRESCRIPTION_OPTION_CFG);
            isCheckingPermissionOfResultingSubclinical = ConfigUtil.GetIntConfig(IS_CHECKING_PERMISSION_OF_RESULTING_SUBCINICAL_CFG) == 1;
            reservedNumOrder = ConfigUtil.GetStrConfig(RESERVED_NUM_ORDER_CFG);
            isUsingOtherNumOrderForPrioritized = ConfigUtil.GetIntConfig(IS_USING_OTHER_NUM_ORDER_FOR_PRIORITIZED_CFG) == 1;
            spitPresByByGroupOption = (SpitPresByByGroupOption)ConfigUtil.GetIntConfig(SPLIT_PRES_BY_GROUP_OPTION_CFG);
            autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded_NumberOfDay = ConfigUtil.GetIntConfig(AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED__NUMBER_OF_DAY_CFG);
            autoChangeBhytToHospitalFeeIfBidExpiredDateIsExceeded = ConfigUtil.GetIntConfig(AUTO_CHANGE_BHYT_TO_HOSPITAL_FEE_IF_BID_EXPIRED_DATE_IS_EXCEEDED_CFG) == 1;
            maxSuspendingDayAllowedForInPatientPrescription = ConfigUtil.GetIntConfig(MAX_SUSPENDING_DAY_ALLOWED_FOR_IN_PATIENT_PRESCRIPTION_CFG);
            doNotAllowToProcessExecuteAssignedServiceReqByAnother = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_PROCESS_EXECUTE_ASSIGNED_SERVICE_REQ_BY_ANOTHER_CFG) == 1;
            autoSetMainExamWhichHospitalize = ConfigUtil.GetIntConfig(AUTO_SET_MAIN_EXAM_WHICH_HOSPITALIZE_CFG) == 1;
            autoSetMainExamWhichFinish = ConfigUtil.GetIntConfig(AUTO_SET_MAIN_EXAM_WHICH_FINISH_CFG) == 1;
            lisSidLength = ConfigUtil.GetIntConfig(LIS_SID_LENGTH_CFG);
            doNotAllowToEditIfPaid = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_EDIT_IF_PAID_CFG) == 1;
            updateStatusAlongWithSaleExpMest = ConfigUtil.GetIntConfig(UPDATE_STATUS_ALONG_WITH_SALE_EXP_MEST_CFG) == 1;
            numOrderIssueOption = (NumOrderIssueOption)ConfigUtil.GetIntConfig(NUM_ORDER_ISSUE_OPTION_CFG);
            isUsingSubPrescriptionMechanism = ConfigUtil.GetIntConfig(IS_USING_SUB_PRESCRIPTION_MECHANISM_CFG) == 1;
            surgExecuteTakeIntrucionTimeByServiceReq = ConfigUtil.GetIntConfig(SURG_EXECUTE_TAKE_INTRUCION_TIME_BY_SERVICE_REQ_CFG) == 1;
            isAllowProcessingTestServiceReqWhenApproveBlood = ConfigUtil.GetIntConfig(IS_ALLOW_PROCESSING_TEST_SERVICE_REQ_WHEN_APPROVE_BLOOD_CFG) == 1;
            isAllowAssignOxygen = ConfigUtil.GetIntConfig(IS_ALLOW_ASSIGN_OXYGEN_CFG) == 1;
            genrateBarcodeOption = (GenrateBarcodeOption)ConfigUtil.GetIntConfig(GENERATE_BARCODE_OPTION_CFG);
            assignRoomByExecutedOption = (AssignRoomByExecutedOption)ConfigUtil.GetIntConfig(IS_ASSIGN_ROOM_BY_EXECUTED_CFG);
            isAllowFinishWhenAccountIsDoctor = ConfigUtil.GetIntConfig(IS_ALLOW_FINISH_WHEN_ACCOUNT_IS_DOCTOR_CFG) == 1;
            isAllowAutoAddRationSum = ConfigUtil.GetIntConfig(IS_ALLOW_AUTO_ADD_RATION_SUM_CFG) == 1;
            autoCreateRationWithTimeOption = ConfigUtil.GetStrConfig(AUTO_CREATE_RATION_WITH_TIME_OPTION_CFG);
            isCheckSimultaneity = ConfigUtil.GetIntConfig(IS_CHECK_SIMULTANEITY_OPTION_CFG) == 1;
            icdCodeToPayRestrictPatientTypeByOtherSourcePaid = ConfigUtil.GetStrConfig(ICD_CODE_TO_APPLY_RESTRICT_PATIENT_TYPE_BY_OTHER_SOURCE_PAID_CFG);
            doNotAllowPresWhenIntructionTimeOutOfBidValidDate = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_PRES_WHEN_INTRUCTION_TIME_OUT_OF_BID_VALID_DATE_CFG) == 1;
            KiotAutoRequireFeeIncaseOfExamHasAttachment = ConfigUtil.GetIntConfig(KIOT_AUTO_REQUIRE_FEE_INCASE_OF_EXAM_HAS_ATTACHMENT_CFG) == 1;
            DoNotCheckMinProcessTimeExamInCaseOfHospitalize = ConfigUtil.GetIntConfig(DO_NOT_CHECK_MIN_PROCESS_TIME_EXAM_IN_CASE_OF_HOSPITALIZE_CFG) == 1;
            testStartTimeOption = (TestStartTimeOption)ConfigUtil.GetIntConfig(TEST_START_TIME_OPTION_CFG);
            AutoAddExcuteRoleServiceReqTypeId = GetIds(AUTO_ADD_EXCUTE_ROLE__SERVICE_REQ_TYPE_CODE_CFG);
        }
    }
}
