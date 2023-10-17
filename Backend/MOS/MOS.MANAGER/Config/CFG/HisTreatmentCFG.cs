using MOS.EFMODEL.DataModels;
using System;
using System.Linq;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.Token;
using Inventec.Common.Logging;
using MOS.MANAGER.Config.CFG;

namespace MOS.MANAGER.Config
{
    public class HisTreatmentCFG
    {
        public class StartCodeConfig
        {
            public long StartNumber { get; set; }
            public long StartYear { get; set; }
        }

        //Cac option sinh so ra vien
        public enum EndCodeOption
        {
            /// <summary>
            /// Mã ra viện có dạng: XXXXXX/YY. Trong đó:
            /// X: số tự tăng;
            /// Y: 2 chữ số cuối của năm
            /// </summary>
            OPTION1 = 1,

            /// <summary>
            /// Mã ra viện có dạng TT/XXXXXX/YY. Trong đó: 
            /// T: mã diện điều trị;
            /// X: số tự tăng
            /// Y: 2 chữ số cuối của năm;
            /// </summary>
            OPTION2 = 2,

            /// <summary>
            /// Mã ra viện có dạng YYTT-XXXXXX. Trong đó: 
            /// T: ma khoa ket thuc dieu tri
            /// X: so tu tang theo nam, theo ma khoa
            /// Y: 2 chữ số cuối của năm;
            /// </summary>
            OPTION3 = 3,
        }

        /// <summary>
        /// Cac option cau hinh cho phep tao nhieu ho so dieu tri trong 1 ngay
        /// </summary>
        public enum ManyTreatmentPerDayOption
        {
            /// <summary>
            /// Cho phep
            /// </summary>
            ALLOW = 1,
            /// <summary>
            /// Chi cho phep voi loai la cap cuu
            /// </summary>
            EMERGENCY = 2,
            /// <summary>
            /// Khong cho phep co 2 ho so la BHYT trong 1 ngay
            /// </summary>
            NO_2_BHYT = 3,
            /// <summary>
            /// Khong cho phep
            /// </summary>
            NOT_ALLOW = 4,
            /// <summary>
            /// Neu ko phai BHYT thi ko check
            /// Neu la BHYT thi bat buoc chi co 1 ho so ko phai la "cap cuu"
            /// </summary>
            NO_2_NON_EMERGENCY_BHYT = 5
        }

        //Cac option sinh ma luu tru
        public enum StoreCodeOption
        {
            /// <summary>
            /// Mã lưu trữ có dạng: XXXXXX/YY. Trong đó:
            /// X: số tự tăng;
            /// Y: 2 chữ số cuối của năm
            /// (sinh theo trigger trong DB)
            /// </summary>
            OPTION1 = 1,

            /// <summary>
            /// Mã lưu trữ có dạng PYYXXXXXX. Trong đó: 
            /// P: mã đối tượng bệnh nhân;
            /// Y: 2 chữ số cuối của năm;
            /// X: số tự tăng
            /// </summary>
            OPTION2 = 2,

            /// <summary>
            /// Mã lưu trữ có dạng: KKKKKTTTTT/YY. Trong đó:
            /// KKKKK: Mã khoa kết thúc điều trị ( Tối đa 5 kí tự)
            /// TTTTT :Thứ tự lưu trữ của hồ sơ đó của khoa (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
            /// YY : Năm lưu trữ 
            /// </summary>
            OPTION3 = 3,

            /// <summary>
            /// Mã lưu trữ có dạng: KKKKKTTTTT/YY. Trong đó:
            /// KKKKK: Mã kho lưu trữ ( Tối đa 5 kí tự)
            /// TTTTT :Thứ tự lưu trữ của hồ sơ đó trong kho (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
            /// YY : Năm lưu trữ 
            /// </summary>
            OPTION4 = 4,

            /// <summary>
            /// Mã lưu trữ có dạng: KKKKK/TTTTT/YY. Trong đó:
            /// KKKKK: Mã kho lưu trữ ( Tối đa 5 kí tự)
            /// TTTTT :Thứ tự lưu trữ của hồ sơ đó trong kho (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
            /// YY : Năm lưu trữ 
            /// </summary>
            OPTION5 = 5,

            /// <summary>
            /// Mã lưu trữ có dạng: KKKKKYY-TTTT. Trong đó:
            /// KKKKK: Mã kho lưu trữ (Tối đa 5 kí tự)
            /// TTTT :Thứ tự lưu trữ của hồ sơ đó trong kho (Trong trường hợp có 1 hồ sơ được lấy đi, hồ sơ mới được lưu vào sẽ chiếm chỗ của hồ sơ cũ)
            /// YY : Năm lưu trữ 
            /// (Sinh theo trigger trong DB)
            /// </summary>
            OPTION6 = 6
        }

        //Cac option lay hau to nam khi sinh ma luu tru
        public enum StoreCodeSeedTimeOption
        {
            //Lay theo thoi gian luu tru (store_time)
            BY_STORE_TIME = 1,
            //Lay theo thoi gian ket thuc dieu tri
            BY_OUT_TIME = 2,
            //Lay theo thoi gian vao vien
            BY_IN_TIME = 3,
        }

        //Cac option sinh ma luu tru
        public enum InCodeFormatOption
        {
            /// <summary>
            /// Mã nhap vien có dạng: XXXXXX/YY. Trong đó:
            /// X: số tự tăng;
            /// Y: 2 chữ số cuối của năm
            /// </summary>
            OPTION1 = 1,

            /// <summary>
            /// Mã nhap vien có dạng: XXXXXX/KK/TT/YY. Trong đó:
            /// X: Số tự tăng;
            /// K: Mã khoa BN nhập vào
            /// T: Mã diện điều trị
            /// Y: 2 chữ số cuối của năm
            /// </summary>
            OPTION2 = 2,
        }

        //Cac option lay hau to nam khi sinh ma luu tru
        public enum InCodeGenerateOption
        {
            /// <summary>
            /// Sinh khi tiep nhan vao noi tru
            /// </summary>
            BY_RECV = 1,
            /// <summary>
            /// Sinh khi yeu cau nhap vien tai phong kham
            /// </summary>
            BY_REQ = 2,
        }

        //Cac option tu dong duyet BHYT sau khi khoa vien phi
        public enum AutoHeinApprovalAfterFeeLockOption
        {
            /// <summary>
            /// Khong thuc hien tu dong duyet
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Tu dong duyet tat ca
            /// </summary>
            ALL = 1,
            /// <summary>
            /// Chi tu dong voi kham va dieu tri ngoai tru
            /// </summary>
            OUT_PATIENT = 2,
        }

        //Cac option tu dong duyet BHYT sau khi khoa vien phi
        public enum AlowManyTreatmentOpeningOption
        {
            /// <summary>
            /// Khong cho phep
            /// </summary>
            NO = 0,
            /// <summary>
            /// Cho phep
            /// </summary>
            YES = 1,
            /// <summary>
            /// Chi cho phep neu ho so dang mo thuoc chi nhanh khac voi chi nhanh dang dang ky
            /// </summary>
            YES_IF_OTHER_OPENING = 2,
            /// <summary>
            /// Khong gioi han voi ho so khong phai la bao hiem y te
            /// </summary>
            UNLIMIT_WITH_NOT_BHYT_TREATMENT = 3,
            /// <summary>
            /// trường hợp nhập viện hoặc chuyển viện
            /// </summary>
            HOSPITALIZED_OR_TRANSFERRED = 4,
        }

        //Cac option co cho phep duyet khoa vien phi truoc khi duyet benh an khong
        public enum ApproveMediRecordBeforeLockFeeOption
        {
            /// <summary>
            /// Khong chan
            /// </summary>
            ALLOW = 0,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi ho so dien kham
            /// </summary>
            EXAM = 1,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi ho so dien ngoai tru
            /// </summary>
            OUT_TREAT = 2,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi ho so dien dieu tri ban ngay
            /// </summary>
            DAY_TREAT = 3,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi ho so dien dieu tri noi tru
            /// </summary>
            IN_TREAT = 4,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi ho so dien dieu tri khac kham
            /// </summary>
            NOT_EXAM = 5,

            /// <summary>
            /// Chan duyet khoa vp khi chua duyet benh an voi tat ca cac dien dieu tri
            /// </summary>
            ALL = 6,
        }


        //Cac option chan ket thuc dieu tri khi suat an chua duyet
        public enum MustApproveRationBeforeFinishTreatment
        {
            /// <summary>
            /// Khong chan
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Chi canh bao neu chua thong hop hoac chua duyet
            /// </summary>
            WARNING = 1,
            /// <summary>
            /// Chan neu chua thong hop hoac chua duyet
            /// </summary>
            BLOCK = 2,
        }

        public enum CheckPreviousDebtOption
        {
            /// <summary>
            /// Hien thi chan
            /// </summary>
            WARNING = 1,
            /// <summary>
            /// Hien thi chan doi voi ho so BHYT con no vien phi
            /// </summary>
            WARNING_WITH_BHYT = 2,
            WARNING_AS_FIRST = 3,
            WARNING_HAVE_TO_PAY_MORE = 4,
        }

        public enum MustFinishAllServicesBeforeFinishTreatment
        {
            // Kiểm tra các dịch vụ đã kết thúc hay chưa khi kết thúc điều trị
            /// <summary>
            /// Không hiển thị
            /// </summary>
            NONE = 0,
            /// <summary>
            /// Không cho kết thúc điều trị và Có hiển thị cảnh báo
            /// </summary>
            BLOCK_AND_WARNING = 1,
            /// <summary>
            /// Không cho kết thúc điều trị và hiển thị cảnh báo đối với diện điều trị nội trú
            /// </summary>
            BLOCK_AND_WARNING_WITH_NOI_TRU = 2,
        }

        public enum UsingExamSubIcdWhenFinishOption
        {
            /// <summary>
            /// Lấy dữ liệu từ client truyền lên
            /// </summary>
            BY_CLIENT = 1,
            /// <summary>
            /// Không cho kết thúc điều trị và hiển thị cảnh báo đối với diện điều trị nội trú
            /// </summary>
            BY_EXAM_REQS = 2,
            /// <summary>
            /// Bệnh phụ của hồ sơ luôn lấy theo trường "CĐ phụ ra viện" ở màn hình "Xử lý khám".
            /// </summary>
            BY_SUB_CODE = 3,

        }

        public enum AutoSetIcdWhenFinishInAdditionExamOption
        {
            /// <summary>
            /// Lấy dữ liệu từ tất cả các y lệnh đã chỉ định cho hồ sơ
            /// </summary>
            BY_REQS_NOT_MAIN = 1,
            /// <summary>
            /// Lấy dữ liệu từ tất cả các y lệnh Khám đã chỉ định cho hồ sơ 
            /// </summary>
            BY_EXAM_REQS_NOT_MAIN = 2,
        }

        public class DeathSyncInfo
        {
            public string HeinMediOrgCode { get; set; }
            public string User { get; set; }
            public string Password { get; set; }
            public string Url { get; set; }
            public string CertificateLink { get; set; }
            public string CertificatePass { get; set; }
        }

        //Tu dong nhan dien khoa don tiep su dung phong xu ly kham dau tien
        private const string IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM_CFG = "MOS.IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM";
        //Chon cau hinh cho phep tao nhieu ho so dieu tri trong 1 ngay tuong ung voi 1 benh nhan hay khong
        private const string MANY_TREATMENT_PER_DAY_OPTION_CFG = "MOS.TREATMENT.MANY_TREATMENT_PER_DAY_OPTION";
        //Cho phep mo nhieu ho so dieu tri cho cung 1 BN hay ko
        private const string ALLOW_MANY_TREATMENT_OPENING_OPTION_CFG = "MOS.TREATMENT.ALLOW_MANY_TREATMENT_OPENING_OPTION";
        private const string MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT_CFG = "MOS.HIS_TREATMENT.MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT";
        private const string MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT_CFG = "MOS.HIS_TREATMENT.MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT";
        private const string AUTO_LOCK_AFTER_BILL_CFG = "MOS.HIS_TREATMENT.AUTO_LOCK_AFTER_BILL";
        private const string AUTO_LOCK_AFTER_REPAY_CFG = "MOS.HIS_TREATMENT.AUTO_LOCK_AFTER_REPAY";
        private const string AUTO_LOCK_AFTER_HEIN_APPROVAL_CFG = "MOS.HIS_TREATMENT.AUTO_LOCK_AFTER_HEIN_APPROVAL";
        private const string AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE_CFG = "MOS.HIS_TREATMENT.AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE";
        //Cac option tu dong duyet BHYT sau khi khoa vien phi
        private const string AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION_CFG = "MOS.HIS_TREATMENT.AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION";

        /// <summary>
        /// Lua chon cau hinh sinh so vao vien
        /// 1: sinh theo cu phap STT/Nam
        /// 2: sinh theo cu phap STT/Ma khoa/NgT - NT/nam
        /// </summary>
        private const string IN_CODE_FORMAT_OPTION_CFG = "MOS.HIS_TREATMENT.IN_CODE_FORMAT_OPTION";
        /// <summary>
        /// 1: sinh khi tiep nhan vao noi tru
        /// 2: sinh khi co yeu cau nhap vien (tu phong kham)
        /// </summary>
        private const string IN_CODE_GENERATE_OPTION_CFG = "MOS.HIS_TREATMENT.IN_CODE_GENERATE_OPTION";

        private const string STORE_CODE_OPTION_CFG = "MOS.HIS_TREATMENT.STORE_CODE_OPTION";
        private const string STORE_CODE_SEED_TIME_OPTION_CFG = "MOS.HIS_TREATMENT.STORE_CODE_SEED_TIME_OPTION";
        private const string CHECK_PREVIOUS_DEBT_OPTION_CFG = "MOS.HIS_TREATMENT.CHECK_PREVIOUS_DEBT_OPTION";
        private const string IS_CHECK_PREVIOUS_PRESCRIPTION_CFG = "MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_PRESCRIPTION";
        private const string IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM_CFG = "MOS.HIS_TREATMENT.IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM";
        private const string IS_CHECK_TODAY_FINISH_TREATMENT_CFG = "MOS.HIS_TREATMENT.IS_CHECK_TODAY_FINISH_TREATMENT";
        private const string IS_MANUAL_END_CODE_CFG = "MOS.HIS_TREATMENT.IS_MANUAL_END_CODE";
        private const string IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM_CFG = "MOS.HIS_TREATMENT.IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM";
        private const string FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG = "MOS.HIS_TREATMENT.FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME";
        private const string IS_USING_LASTEST_ICD_CFG = "MOS.HIS_TREATMENT.IS_USING_LASTEST_ICD";
        private const string DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY = "MOS.HIS_TREATMENT.DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY";
        private const string GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN_CFG = "MOS.HIS_TREATMENT.GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN";

        private static bool? isAutoDetectReceivingDepartmentByFirstExamExecuteRoom;
        public static bool IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM
        {
            get
            {
                if (!isAutoDetectReceivingDepartmentByFirstExamExecuteRoom.HasValue)
                {
                    isAutoDetectReceivingDepartmentByFirstExamExecuteRoom = ConfigUtil.GetIntConfig(IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM_CFG) == 1;
                }
                return isAutoDetectReceivingDepartmentByFirstExamExecuteRoom.Value;
            }
            set
            {
                isAutoDetectReceivingDepartmentByFirstExamExecuteRoom = value;
            }
        }
        /// <summary>
        /// Danh sach ma dich vu se tu dong ket thuc yeu cau khi ho so dieu tri duoc ket thuc
        /// </summary>
        private const string SERVICE_CODE__AUTO_FINISH_WHEN_TREATMENT_FINISH = "MOS.HIS_TREATMENT.AUTO_FINISH_SERVICE_REQ.SERVICE_CODE";
        private const string IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION_CFG = "MOS.HIS_TREATMENT.IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION";
        private const string ALLOWED_APPOINTMENT_DAY_BEFORE_CFG = "MOS.HIS_TREATMENT.ALLOWED_APPOINTMENT_DAY_BEFORE";
        private const string ALLOWED_APPOINTMENT_DAY_AFTER_CFG = "MOS.HIS_TREATMENT.ALLOWED_APPOINTMENT_DAY_AFTER";
        private const string IS_KEEPING_STORE_CODE_CFG = "MOS.HIS_TREATMENT.IS_KEEPING_STORE_CODE";
        private const string END_CODE_OPTION_CFG = "MOS.HIS_TREATMENT.END_CODE_OPTION";
        /// <summary>
        /// Cho phep cau hinh so bat dau khi sinh so ra vien
        /// </summary>
        private const string START_END_CODE_CFG = "MOS.HIS_TREATMENT.START_END_CODE";
        /// <summary>
        /// Cho phep cau hinh so bat dau khi sinh so nhap vien
        /// </summary>
        private const string START_IN_CODE_CFG = "MOS.HIS_TREATMENT.START_IN_CODE";
        /// <summary>
        /// Cho phep cau hinh so bat dau khi sinh so luu tru benh an
        /// </summary>
        private const string START_STORE_CODE_CFG = "MOS.HIS_TREATMENT.START_STORE_CODE";
        /// <summary>
        /// Không cho phép thay đổi bệnh nhân (ghép mã) trong trường hợp nếu khi thay đổi sẽ dẫn đến có tình trạng 2 BN có cùng số thẻ
        /// </summary>
        private const string IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT_CFG = "MOS.HIS_TREATMENT.IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT";
        /// <summary>
        /// Ko cho phep xoa ho so trong truong hop co giao dich
        /// </summary>
        private const string DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION_CFG = "MOS.HIS_TREATMENT.DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION";
        /// <summary>
        /// Lay thoi gian duyet khoa vien phi theo thoi gian ra vien trong truong hop tu dong thuc hien duyet
        /// </summary>
        private const string IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO_CFG = "MOS.HIS_TREATMENT.IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO";
        /// <summary>
        /// Cho phep thoi gian duyet khoa vien phi lon hon thoi gian hien tai hay ko
        /// </summary>
        private const string ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME_CFG = "MOS.HIS_TREATMENT.ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME";
        /// <summary>
        /// Co sinh so "chuyen vien" trong truong hop "luu tam" khi ket thuc dieu tri hay khong
        /// </summary>
        private const string GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING_CFG = "MOS.HIS_TREATMENT.GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING";

        /// <summary>
        /// Co sinh so "ra vien" trong truong hop "luu tam" khi ket thuc dieu tri hay khong
        /// </summary>
        private const string GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING_CFG = "MOS.HIS_TREATMENT.GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING";

        /// <summary>
        /// Co canh bao tong tien benh nhan cung chi tra lon hon 6 thang luong co ban hay khong
        /// </summary>
        private const string CONFIG_KEY__WARNING_OVER_SIX_MONTH = "MOS.HIS_TREATMENT.BHYT.FIVE_YEAR.WARNING_OVER_SIX_MONTHS";

        /// <summary>
        /// Ket thuc het tat ca cac yeu cau kham truoc khi kham them
        /// </summary>
        private const string CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM = "MOS.HIS_TREATMENT.MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM";

        /// <summary>
        /// Ket thuc het tat ca cac yeu cau kham truoc khi nhap vien
        /// </summary>
        private const string CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE = "MOS.HIS_TREATMENT.MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE";

        /// <summary>
        /// Khong cho phep tao, sua, xoa to dieu tri neu ho so dieu tri da ket thuc
        /// </summary>
        private const string CONFIG_KEY__DOT_NOT_ALLOW_CREATE_OR_EDIT_TRACKING = "MOS.HIS_TRACKING.DO_NOT_ALLOW_CREATING_OR_MODIFYING_AFTER_FINISHING";

        /// <summary>
        /// Co gui notify duyet benh an khi ket thuc dieu tri hay khong
        /// </summary> 
        private const string CONFIG_KEY__NOTIFY_APPROVE_MEDI_REOCORD_WHEN_FINISH_TREATMENT = "MOS.HIS_TREATMENT.FINISH.NOTIFY_APPROVE_MEDICAL_RECORD";

        /// <summary>
        /// Co chan duyet khoa vien phi khi chua duyet benh an hay khong
        /// </summary> 
        private const string CONFIG_KEY__HIS_TREATMENT__LOCK_FEE_AFTER_APPROVE_MEDI_RECORD__OPTION = "MOS.HIS_TREATMENT.LOCK_FEE_AFTER_APPROVE_MEDI_RECORD.OPTION";

        private const string MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT_CFG = "MOS.HIS_TREATMENT.MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT";

        private const string AUTO_STORE_MEDI_RECORD_BY_PROGRAM_CFG = "MOS.HIS_TREATMENT.AUTO_STORE_MEDI_RECORD_BY_PROGRAM";

        private const string STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING_CFG = "MOS.HIS_TREATMENT.STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING";

        /// <summary>
        /// Kiem tra vat tu thieu so thong tin hoa don chung tu truoc khi duyet khoa vien phi hay khong
        /// </summary>
        private const string VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE_CFG = "MOS.HIS_TREATMENT.VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE";

        /// <summary>
        /// Thu muc luu file xml4210 thong tuyen (file xuat khi ho so chua duyet khoa vien phi ma co check chon)
        /// </summary>
        private const string XML4210_COLLINEAR_FOLDER_PATH_CFG = "MOS.HIS_TREATMENT.XML4210_COLLINEAR_FOLDER_PATH";

        /// <summary>
        /// Cau hinh chan ket thuc dieu tri loai hen kham voi benh nhan bhyt trai tuyen hoac tg hen kham > tg han den cua the BHYT
        /// </summary>
        private const string CONFIG_KEY__FINISH__BLOCK_APPOINTMENT = "MOS.HIS_TREATMENT.FINISH.BLOCK_APPOINTMENT";

        private const string IS_MANUAL_IN_CODE_CFG = "MOS.HIS_TREATMENT.IS_MANUAL_IN_CODE";

        /// <summary>
        /// Thu muc luu file xml2076 ho so chung tu
        /// </summary>
        private const string XML2076_FOLDER_PATH_CFG = "MOS.HIS_TREATMENT.FINISH.XML2076_FOLDER_PATH";

        private const string XML_CHECK_IN_FOLDER_PATH_CFG = "MOS.EXPORT_XML.XML_CHECK_IN_FOLDER_PATH";
        private const string AUTO_EXPORT_XML_CHECK_IN_CFG = "MOS.EXPORT_XML.IS_AUTO_EXPORT_XML_CHECK_IN";
        /// <summary>
        /// Phai duyet tat ca suat an truoc khi ket thuc ra vien
        /// </summary>
        private const string FINISH_MUST_APPROVE_ALL_RATION_CFG = "MOS.HIS_TREATMENT.FINISH.MUST_APPROVE_ALL_RATION";

        /// <summary>
        /// Tuy chon lay thong tin benh phu khi ket thuc dieu tri
        /// </summary>
        private const string USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON_CFG = "MOS.HIS_TREATMENT.IS_USING_EXAM_SUB_ICD_WHEN_FINISH";

        /// <summary>
        /// Tùy chọn cho phép không xóa thông tin thời gian ra viện khi mở lại hồ sơ điều trị
        /// </summary>
        private const string DONOT_CLEAR_OUT_TIME_WHEN_REOPENING_CFG = "MOS.HIS_TREATMENT.DONOT_CLEAR_OUT_TIME_WHEN_REOPENING";

        /// <summary>
        /// tuy chon sua thong tin ho so
        /// </summary>
        private const string UPDATE_INFO_OPTION_CFG = "MOS.HIS_TREATMENT.UPDATE_INFO_OPTION";

        private const string CFG_MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS = "MOS.HIS_TREATMENT.MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS";
        private const string INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME_CFG = "MOS.HIS_TREATMENT.INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME";
        private const string DEFAULT_OF_AUTO_END_CFG = "MOS.HIS_TREATMENT_END_TYPE.TREATMENT_END_TYPE_CODE.DEFAULT_OF_AUTO_END";

        private const string SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME_OPTION_CFG = "MOS.HIS_TREATMENT.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME_OPTION";
        private const string AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION_CFG = "MOS.HIS_TREATMENT.AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM";
        private const string IS_ALLOW_DELETING_IF_EXIST_SERVICE_REQ_CFG = "MOS.HIS_TREATMENT.ALLOW_DELETING_IF_EXIST_SERVICE_REQ";
        private const string IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION_CFG = "MOS.HIS_TREATMENT.IN_CODE_GENERATE_TYPE_OPTION";
        private const string AUTO_CREATE_WHEN_APPOINTMENT_CFG = "MOS.HIS_TREATMENT.AUTO_CREATE_WHEN_APPOINTMENT";
        private const string DEATH_CONNECTION_INFO_CFG = "MOS.HIS_DEATH.CONNECTION_INFO";

        private const string IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE_CFG = "MOS.HIS_TREATMENT.IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE";
        private const string SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME_OPTION_CFG = "MOS.HIS_TREATMENT.SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME_OPTION";

        private static string xmlCheckInFolderPath;
        public static string XML_CHECK_IN_FOLDER_PATH
        {
            get
            {
                if (xmlCheckInFolderPath == null)
                {
                    xmlCheckInFolderPath = ConfigUtil.GetStrConfig(XML_CHECK_IN_FOLDER_PATH_CFG);
                }
                return xmlCheckInFolderPath;
            }
        }

        private static UsingExamSubIcdWhenFinishOption? usingExamSubIcdWhenFinishOption;
        public static UsingExamSubIcdWhenFinishOption USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON
        {
            get
            {
                if (!usingExamSubIcdWhenFinishOption.HasValue)
                {
                    usingExamSubIcdWhenFinishOption = (UsingExamSubIcdWhenFinishOption)ConfigUtil.GetIntConfig(USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON_CFG);
                }
                return usingExamSubIcdWhenFinishOption.Value;
            }
        }

        private static bool? autoExportXmlCheckIn;
        public static bool AUTO_EXPORT_XML_CHECK_IN
        {
            get
            {
                if (!autoExportXmlCheckIn.HasValue)
                {
                    autoExportXmlCheckIn = ConfigUtil.GetIntConfig(AUTO_EXPORT_XML_CHECK_IN_CFG) == 1;
                }
                return autoExportXmlCheckIn.Value;
            }
        }

        private static string xml4210CollinearFolderPath;
        public static string XML4210_COLLINEAR_FOLDER_PATH
        {
            get
            {
                if (xml4210CollinearFolderPath == null)
                {
                    xml4210CollinearFolderPath = ConfigUtil.GetStrConfig(XML4210_COLLINEAR_FOLDER_PATH_CFG);
                }
                return xml4210CollinearFolderPath;
            }
        }

        private static string xml2076FolderPath;
        public static string XML2076_FOLDER_PATH
        {
            get
            {
                if (xml2076FolderPath == null)
                {
                    xml2076FolderPath = ConfigUtil.GetStrConfig(XML2076_FOLDER_PATH_CFG);
                }
                return xml2076FolderPath;
            }
        }

        public static List<long> AutoFinishServiceIds(long branchId)
        {
            List<long> result = new List<long>();
            try
            {
                List<HIS_CONFIG> configs = Loader.GetConfig(SERVICE_CODE__AUTO_FINISH_WHEN_TREATMENT_FINISH, branchId);
                if (configs != null && configs.Count > 0)
                {
                    HIS_CONFIG config = configs.Where(o => o.BRANCH_ID == branchId).FirstOrDefault();
                    if (config == null)
                    {
                        config = configs.Where(o => !o.BRANCH_ID.HasValue).FirstOrDefault();
                    }

                    if (config != null)
                    {
                        string value = string.Format(",{0},", config.VALUE);
                        result = HisServiceCFG.DATA_VIEW != null ? HisServiceCFG.DATA_VIEW
                            .Where(o => value.Contains("," + o.SERVICE_CODE + ","))
                            .Select(o => o.ID)
                            .ToList() : null;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return result;
        }

        private static bool? mustSetProgramWhenFinishingInPatient;
        public static bool MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT
        {
            get
            {
                if (!mustSetProgramWhenFinishingInPatient.HasValue)
                {
                    mustSetProgramWhenFinishingInPatient = ConfigUtil.GetIntConfig(MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT_CFG) == 1;
                }
                return mustSetProgramWhenFinishingInPatient.Value;
            }
        }

        private static bool? verifyInvoiceInfoMaterialBeforeLockFee;
        public static bool VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE
        {
            get
            {
                if (!verifyInvoiceInfoMaterialBeforeLockFee.HasValue)
                {
                    verifyInvoiceInfoMaterialBeforeLockFee = ConfigUtil.GetIntConfig(VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE_CFG) == 1;
                }
                return verifyInvoiceInfoMaterialBeforeLockFee.Value;
            }
        }

        private static bool? autoStoreMediRecordByProgram;
        public static bool AUTO_STORE_MEDI_RECORD_BY_PROGRAM
        {
            get
            {
                if (!autoStoreMediRecordByProgram.HasValue)
                {
                    autoStoreMediRecordByProgram = ConfigUtil.GetIntConfig(AUTO_STORE_MEDI_RECORD_BY_PROGRAM_CFG) == 1;
                }
                return autoStoreMediRecordByProgram.Value;
            }
        }

        private static bool? isNotAllowingChangingPatientIfHeinCardConflict;
        public static bool IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT
        {
            get
            {
                if (!isNotAllowingChangingPatientIfHeinCardConflict.HasValue)
                {
                    isNotAllowingChangingPatientIfHeinCardConflict = ConfigUtil.GetIntConfig(IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT_CFG) == 1;
                }
                return isNotAllowingChangingPatientIfHeinCardConflict.Value;
            }
        }

        private static bool? doNotAllowDeletingIfExistTransaction;
        public static bool DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION
        {
            get
            {
                if (!doNotAllowDeletingIfExistTransaction.HasValue)
                {
                    doNotAllowDeletingIfExistTransaction = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION_CFG) == 1;
                }
                return doNotAllowDeletingIfExistTransaction.Value;
            }
        }

        private static bool? isUsingLastestIcd;
        public static bool IS_USING_LASTEST_ICD
        {
            get
            {
                if (!isUsingLastestIcd.HasValue)
                {
                    isUsingLastestIcd = ConfigUtil.GetIntConfig(IS_USING_LASTEST_ICD_CFG) == 1;
                }
                return isUsingLastestIcd.Value;
            }
        }

        private static bool? isManualEndCode;
        public static bool IS_MANUAL_END_CODE
        {
            get
            {
                if (!isManualEndCode.HasValue)
                {
                    isManualEndCode = ConfigUtil.GetIntConfig(IS_MANUAL_END_CODE_CFG) == 1;
                }
                return isManualEndCode.Value;
            }
        }

        private static bool? isPricingFirstExamAsMainExam;
        public static bool IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM
        {
            get
            {
                if (!isPricingFirstExamAsMainExam.HasValue)
                {
                    isPricingFirstExamAsMainExam = ConfigUtil.GetIntConfig(IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM_CFG) == 1;
                }
                return isPricingFirstExamAsMainExam.Value;
            }
        }

        private static bool? isManualInCode;
        public static bool IS_MANUAL_IN_CODE
        {
            get
            {
                if (!isManualInCode.HasValue)
                {
                    isManualInCode = ConfigUtil.GetIntConfig(IS_MANUAL_IN_CODE_CFG) == 1;
                }
                return isManualInCode.Value;
            }
        }

        private static bool? storeMediRecordByProgramIncaseOfTemporaryFinishing;
        public static bool STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING
        {
            get
            {
                if (!storeMediRecordByProgramIncaseOfTemporaryFinishing.HasValue)
                {
                    storeMediRecordByProgramIncaseOfTemporaryFinishing = ConfigUtil.GetIntConfig(STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING_CFG) == 1;
                }
                return storeMediRecordByProgramIncaseOfTemporaryFinishing.Value;
            }
        }

        private static int? manyTreatmentPerDayOption;
        public static int MANY_TREATMENT_PER_DAY_OPTION
        {
            get
            {
                if (!manyTreatmentPerDayOption.HasValue)
                {
                    manyTreatmentPerDayOption = ConfigUtil.GetIntConfig(MANY_TREATMENT_PER_DAY_OPTION_CFG);
                }
                return manyTreatmentPerDayOption.Value;
            }
            set
            {
                manyTreatmentPerDayOption = value;
            }
        }

        private static AlowManyTreatmentOpeningOption? allowManyTreatmentOpeningOption;
        public static AlowManyTreatmentOpeningOption ALLOW_MANY_TREATMENT_OPENING_OPTION
        {
            get
            {
                if (!allowManyTreatmentOpeningOption.HasValue)
                {
                    allowManyTreatmentOpeningOption = (AlowManyTreatmentOpeningOption)ConfigUtil.GetIntConfig(ALLOW_MANY_TREATMENT_OPENING_OPTION_CFG);

                }
                return allowManyTreatmentOpeningOption.Value;
            }
        }

        private static int? storeCodeOption;
        public static int STORE_CODE_OPTION
        {
            get
            {
                if (!storeCodeOption.HasValue)
                {
                    storeCodeOption = ConfigUtil.GetIntConfig(STORE_CODE_OPTION_CFG);
                }
                return storeCodeOption.Value;
            }
        }

        private static int? inCodeFormatOption;
        public static int IN_CODE_FORMAT_OPTION
        {
            get
            {
                if (!inCodeFormatOption.HasValue)
                {
                    inCodeFormatOption = ConfigUtil.GetIntConfig(IN_CODE_FORMAT_OPTION_CFG);
                }
                return inCodeFormatOption.Value;
            }
        }

        private static int? inCodeGenerateOption;
        public static int IN_CODE_GENERATE_OPTION
        {
            get
            {
                if (!inCodeGenerateOption.HasValue)
                {
                    inCodeGenerateOption = ConfigUtil.GetIntConfig(IN_CODE_GENERATE_OPTION_CFG);
                }
                return inCodeGenerateOption.Value;
            }
        }

        private static int? storeCodeSeedTimeOption;
        public static int STORE_CODE_SEED_TIME_OPTION
        {
            get
            {
                if (!storeCodeSeedTimeOption.HasValue)
                {
                    storeCodeSeedTimeOption = ConfigUtil.GetIntConfig(STORE_CODE_SEED_TIME_OPTION_CFG);
                }
                return storeCodeSeedTimeOption.Value;
            }
        }

        private static MustFinishAllServicesBeforeFinishTreatment? mustFinishAllServicesBeforeFinishTreatment;
        public static MustFinishAllServicesBeforeFinishTreatment MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT
        {
            get
            {
                if (!mustFinishAllServicesBeforeFinishTreatment.HasValue)
                {
                    mustFinishAllServicesBeforeFinishTreatment = (MustFinishAllServicesBeforeFinishTreatment)ConfigUtil.GetIntConfig(MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT_CFG);
                }
                return mustFinishAllServicesBeforeFinishTreatment.Value;
            }
        }

        private static bool? mustFinishAllExamBeforeFinishTreatment;
        public static bool MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT
        {
            get
            {
                if (!mustFinishAllExamBeforeFinishTreatment.HasValue)
                {
                    mustFinishAllExamBeforeFinishTreatment = ConfigUtil.GetIntConfig(MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT_CFG) == 1;
                }
                return mustFinishAllExamBeforeFinishTreatment.Value;
            }
        }

        private static CheckPreviousDebtOption? checkPreviousDebtOption;
        public static CheckPreviousDebtOption CHECK_PREVIOUS_DEBT_OPTION
        {
            get
            {
                if (!checkPreviousDebtOption.HasValue)
                {
                    checkPreviousDebtOption = (CheckPreviousDebtOption)ConfigUtil.GetIntConfig(CHECK_PREVIOUS_DEBT_OPTION_CFG);
                }
                return checkPreviousDebtOption.Value;
            }
        }

        private static bool? isCheckPreviousPrescription;
        public static bool IS_CHECK_PREVIOUS_PRESCRIPTION
        {
            get
            {
                if (!isCheckPreviousPrescription.HasValue)
                {
                    isCheckPreviousPrescription = ConfigUtil.GetIntConfig(IS_CHECK_PREVIOUS_PRESCRIPTION_CFG) == 1;
                }
                return isCheckPreviousPrescription.Value;
            }
        }

        private static bool? isCheckPreviousPrescriptionExam;
        public static bool IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM
        {
            get
            {
                if (!isCheckPreviousPrescriptionExam.HasValue)
                {
                    isCheckPreviousPrescriptionExam = ConfigUtil.GetIntConfig(IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM_CFG) == 1;
                }
                return isCheckPreviousPrescriptionExam.Value;
            }
        }

        private static bool? isCheckTodayFinishTreatment;
        public static bool IS_CHECK_TODAY_FINISH_TREATMENT
        {
            get
            {
                if (!isCheckTodayFinishTreatment.HasValue)
                {
                    isCheckTodayFinishTreatment = ConfigUtil.GetIntConfig(IS_CHECK_TODAY_FINISH_TREATMENT_CFG) == 1;
                }
                return isCheckTodayFinishTreatment.Value;
            }
        }

        private static bool? autoLockAfterBill;
        public static bool AUTO_LOCK_AFTER_BILL
        {
            get
            {
                if (!autoLockAfterBill.HasValue)
                {
                    autoLockAfterBill = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_BILL_CFG) == 1;
                }
                return autoLockAfterBill.Value;
            }
        }

        private static bool? autoLockAfterRepay;
        public static bool AUTO_LOCK_AFTER_REPAY
        {
            get
            {
                if (!autoLockAfterRepay.HasValue)
                {
                    autoLockAfterRepay = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_REPAY_CFG) == 1;
                }
                return autoLockAfterRepay.Value;
            }
        }

        private static bool? autoLockAfterHeinApproval;
        public static bool AUTO_LOCK_AFTER_HEIN_APPROVAL
        {
            get
            {
                if (!autoLockAfterHeinApproval.HasValue)
                {
                    autoLockAfterHeinApproval = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_HEIN_APPROVAL_CFG) == 1;
                }
                return autoLockAfterHeinApproval.Value;
            }
        }

        private static AutoHeinApprovalAfterFeeLockOption? autoHeinApprovalAfterFeeLockOption;
        public static AutoHeinApprovalAfterFeeLockOption AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION
        {
            get
            {
                if (!autoHeinApprovalAfterFeeLockOption.HasValue)
                {
                    autoHeinApprovalAfterFeeLockOption = (AutoHeinApprovalAfterFeeLockOption)ConfigUtil.GetIntConfig(AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION_CFG);
                }
                return autoHeinApprovalAfterFeeLockOption.Value;
            }
        }

        private static bool? DoNotClearOutTimeWhenReopening;
        public static bool DONOT_CLEAR_OUT_TIME_WHEN_REOPENING
        {
            get
            {
                if (!DoNotClearOutTimeWhenReopening.HasValue)
                {
                    DoNotClearOutTimeWhenReopening = ConfigUtil.GetIntConfig(DONOT_CLEAR_OUT_TIME_WHEN_REOPENING_CFG) == 1;
                }
                return DoNotClearOutTimeWhenReopening.Value;
            }
        }


        private static bool? autoLockAfterFinishIfHasNoPatientPrice;
        public static bool AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE
        {
            get
            {
                if (!autoLockAfterFinishIfHasNoPatientPrice.HasValue)
                {
                    autoLockAfterFinishIfHasNoPatientPrice = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE_CFG) == 1;
                }
                return autoLockAfterFinishIfHasNoPatientPrice.Value;
            }
        }

        private static bool? IsDoNotAllowToLockFeeIfMustPay;
        public static bool IS_DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY
        {
            get
            {
                if (!IsDoNotAllowToLockFeeIfMustPay.HasValue)
                {
                    IsDoNotAllowToLockFeeIfMustPay = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY) == 1;
                }
                return IsDoNotAllowToLockFeeIfMustPay.Value;
            }
        }

        private static bool? finishTimeNotGreaterThanCurrentTime;
        public static bool FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME
        {
            get
            {
                if (!finishTimeNotGreaterThanCurrentTime.HasValue)
                {
                    finishTimeNotGreaterThanCurrentTime = ConfigUtil.GetIntConfig(FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG) == 1;
                }
                return finishTimeNotGreaterThanCurrentTime.Value;
            }
        }

        /// <summary>
        /// Co su dung "dich vu hen kham" khi dang ky tiep don tren kiosk hay khong
        /// </summary>
        private static bool? isUsingAppointmentServiceInKioskReception;
        public static bool IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION
        {
            get
            {
                if (!isUsingAppointmentServiceInKioskReception.HasValue)
                {
                    isUsingAppointmentServiceInKioskReception = ConfigUtil.GetIntConfig(IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION_CFG) == 1;
                }
                return isUsingAppointmentServiceInKioskReception.Value;
            }
        }

        /// <summary>
        /// So ngay cho phep den muon hon so voi ngay hen kham
        /// </summary>
        private static int? allowedAppointmentDayAfter;
        public static int ALLOWED_APPOINTMENT_DAY_AFTER
        {
            get
            {
                if (!allowedAppointmentDayAfter.HasValue)
                {
                    allowedAppointmentDayAfter = ConfigUtil.GetIntConfig(ALLOWED_APPOINTMENT_DAY_AFTER_CFG);
                }
                return allowedAppointmentDayAfter.Value;
            }
        }

        /// <summary>
        /// So ngay cho phep den som hon so voi ngay hen kham
        /// </summary>
        private static int? allowedAppointmentDayBefore;
        public static int ALLOWED_APPOINTMENT_DAY_BEFORE
        {
            get
            {
                if (!allowedAppointmentDayBefore.HasValue)
                {
                    allowedAppointmentDayBefore = ConfigUtil.GetIntConfig(ALLOWED_APPOINTMENT_DAY_BEFORE_CFG);
                }
                return allowedAppointmentDayBefore.Value;
            }
        }

        /// <summary>
        /// Cau hinh cho phep giu "ma luu tru" khi xoa ho so khoi tu luu tru hay khong
        /// </summary>
        private static bool? isKeepingStoreCode;
        public static bool IS_KEEPING_STORE_CODE
        {
            get
            {
                if (!isKeepingStoreCode.HasValue)
                {
                    isKeepingStoreCode = ConfigUtil.GetIntConfig(IS_KEEPING_STORE_CODE_CFG) == 1;
                }
                return isKeepingStoreCode.Value;
            }
        }

        private static EndCodeOption endCodeOption;
        public static EndCodeOption END_CODE_OPTION
        {
            get
            {
                if (endCodeOption == 0)
                {
                    try
                    {
                        endCodeOption = (EndCodeOption)ConfigUtil.GetIntConfig(END_CODE_OPTION_CFG);
                    }
                    catch (Exception ex)
                    {
                        LogSystem.Error(ex);
                    }
                }
                return endCodeOption;
            }
        }

        private static StartCodeConfig startInCode;
        public static StartCodeConfig START_IN_CODE
        {
            get
            {
                if (startInCode == null)
                {
                    startInCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_IN_CODE_CFG));
                }
                return startInCode;
            }
        }

        private static StartCodeConfig startStoreCode;
        public static StartCodeConfig START_STORE_CODE
        {
            get
            {
                if (startStoreCode == null)
                {
                    startStoreCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_STORE_CODE_CFG));
                }
                return startStoreCode;
            }
        }

        private static StartCodeConfig startEndCode;
        public static StartCodeConfig START_END_CODE
        {
            get
            {
                if (startEndCode == null)
                {
                    startEndCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_END_CODE_CFG));
                }
                return startEndCode;
            }
        }

        /// <summary>
        /// Co su dung thoi gian ra vien cho thoi gian khoa vien phi trong truong hop tu dong hay ko
        /// </summary>
        private static bool? isUsingOutTimeForFeeLockTimeInCaseOfAuto;
        public static bool IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO
        {
            get
            {
                if (!isUsingOutTimeForFeeLockTimeInCaseOfAuto.HasValue)
                {
                    isUsingOutTimeForFeeLockTimeInCaseOfAuto = ConfigUtil.GetIntConfig(IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO_CFG) == 1;
                }
                return isUsingOutTimeForFeeLockTimeInCaseOfAuto.Value;
            }
        }

        /// <summary>
        /// Co cho phep thoi gian duyet khoa vien phi lon hon thoi gian hien tai hay ko
        /// </summary>
        private static bool? allowFeeLockTimeGreaterThanSystemTime;
        public static bool ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME
        {
            get
            {
                if (!allowFeeLockTimeGreaterThanSystemTime.HasValue)
                {
                    allowFeeLockTimeGreaterThanSystemTime = ConfigUtil.GetIntConfig(ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME_CFG) == 1;
                }
                return allowFeeLockTimeGreaterThanSystemTime.Value;
            }
        }

        private static bool? generateOutCodeInCaseOfTempFinishing;
        public static bool GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING
        {
            get
            {
                if (!generateOutCodeInCaseOfTempFinishing.HasValue)
                {
                    generateOutCodeInCaseOfTempFinishing = ConfigUtil.GetIntConfig(GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING_CFG) == 1;
                }
                return generateOutCodeInCaseOfTempFinishing.Value;
            }
        }

        private static bool? generateEndCodeInCaseOfTempFinishing;
        public static bool GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING
        {
            get
            {
                if (!generateEndCodeInCaseOfTempFinishing.HasValue)
                {
                    generateEndCodeInCaseOfTempFinishing = ConfigUtil.GetIntConfig(GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING_CFG) == 1;
                }
                return generateEndCodeInCaseOfTempFinishing.Value;
            }
        }

        private static bool? warningOverSixMonths;
        public static bool FIVE_YEAR_WARNING_OVER_SIX_MONTHS
        {
            get
            {
                if (!warningOverSixMonths.HasValue)
                {
                    warningOverSixMonths = ConfigUtil.GetIntConfig(CONFIG_KEY__WARNING_OVER_SIX_MONTH) == 1;
                }
                return warningOverSixMonths.Value;
            }
        }

        private static bool? mustFinishAllExamForAddExam;
        public static bool MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM
        {
            get
            {
                if (!mustFinishAllExamForAddExam.HasValue)
                {
                    mustFinishAllExamForAddExam = ConfigUtil.GetIntConfig(CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM) == 1;
                }
                return mustFinishAllExamForAddExam.Value;
            }
        }

        private static bool? mustFinishAllExamForHospitalize;
        public static bool MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE
        {
            get
            {
                if (!mustFinishAllExamForHospitalize.HasValue)
                {
                    mustFinishAllExamForHospitalize = ConfigUtil.GetIntConfig(CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE) == 1;
                }
                return mustFinishAllExamForHospitalize.Value;
            }
        }

        private static bool? doNotAllowCreateOrEditTracking;
        public static bool DO_NOT_ALLOW_CREATE_OR_EDIT_TRACKING
        {
            get
            {
                if (!doNotAllowCreateOrEditTracking.HasValue)
                {
                    doNotAllowCreateOrEditTracking = ConfigUtil.GetIntConfig(CONFIG_KEY__DOT_NOT_ALLOW_CREATE_OR_EDIT_TRACKING) == 1;
                }
                return doNotAllowCreateOrEditTracking.Value;
            }
        }

        private static bool? notifyApproveMediRecordWhenTreatmentFinish;
        public static bool NOTIFY_APPROVE_MEDI_RECORD_WHEN_TREATMENT_FINISH
        {
            get
            {
                if (!notifyApproveMediRecordWhenTreatmentFinish.HasValue)
                {
                    notifyApproveMediRecordWhenTreatmentFinish = ConfigUtil.GetIntConfig(CONFIG_KEY__NOTIFY_APPROVE_MEDI_REOCORD_WHEN_FINISH_TREATMENT) == 1;
                }
                return notifyApproveMediRecordWhenTreatmentFinish.Value;
            }
        }

        private static int? blockAppointmentOption;
        public static int BLOCK_APPOINTMENT_OPTION
        {
            get
            {
                if (!blockAppointmentOption.HasValue)
                {
                    blockAppointmentOption = ConfigUtil.GetIntConfig(CONFIG_KEY__FINISH__BLOCK_APPOINTMENT);
                }
                return blockAppointmentOption.Value;
            }
        }

        private static bool? updateInfoOption;
        public static bool UPDATE_INFO_OPTION
        {
            get
            {
                if (!updateInfoOption.HasValue)
                {
                    updateInfoOption = ConfigUtil.GetIntConfig(UPDATE_INFO_OPTION_CFG) == 1;
                }
                return updateInfoOption.Value;
            }
        }

        private static ApproveMediRecordBeforeLockFeeOption? approveMediRecordBeforeLockFeeOption;
        public static ApproveMediRecordBeforeLockFeeOption APPROVE_MEDI_RECORD_BEFORE_LOCK_FEE_OPTION
        {
            get
            {
                if (!approveMediRecordBeforeLockFeeOption.HasValue)
                {
                    approveMediRecordBeforeLockFeeOption = (ApproveMediRecordBeforeLockFeeOption)ConfigUtil.GetIntConfig(CONFIG_KEY__HIS_TREATMENT__LOCK_FEE_AFTER_APPROVE_MEDI_RECORD__OPTION);

                }
                return approveMediRecordBeforeLockFeeOption.Value;
            }
        }

        private static MustApproveRationBeforeFinishTreatment? mustApproveRationBeforeFinish;
        public static MustApproveRationBeforeFinishTreatment MUST_APPROVE_RATION_BEFORE_FINISH
        {
            get
            {
                if (!mustApproveRationBeforeFinish.HasValue)
                {
                    mustApproveRationBeforeFinish = (MustApproveRationBeforeFinishTreatment)ConfigUtil.GetIntConfig(FINISH_MUST_APPROVE_ALL_RATION_CFG);

                }
                return mustApproveRationBeforeFinish.Value;
            }
        }

        private static StartCodeConfig GetStartCodeConfig(string input)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    string[] cfgs = input.Split(':');
                    if (cfgs != null && cfgs.Length >= 2)
                    {
                        StartCodeConfig rs = new StartCodeConfig();
                        rs.StartNumber = long.Parse(cfgs[0]);
                        rs.StartYear = long.Parse(cfgs[1]);
                        return rs;
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }
            return null;
        }

        private static int? miniumTimesForExamWithSubclinicals;
        public static int? MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS
        {
            get
            {
                if (!miniumTimesForExamWithSubclinicals.HasValue)
                {
                    miniumTimesForExamWithSubclinicals = ConfigUtil.GetIntConfig(CFG_MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS);
                }

                return miniumTimesForExamWithSubclinicals.Value;
            }
        }

        private static bool? isIntructionTimeMustNotGreaterThanFinishTime;
        public static bool IS_INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME
        {
            get
            {
                if (!isIntructionTimeMustNotGreaterThanFinishTime.HasValue)
                {
                    isIntructionTimeMustNotGreaterThanFinishTime = ConfigUtil.GetIntConfig(INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME_CFG) == 1;
                }
                return isIntructionTimeMustNotGreaterThanFinishTime.Value;
            }
        }

        private static int? serviceFinishTimeMustBeLessThanOutTime;
        public static int SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME
        {
            get
            {
                if (!serviceFinishTimeMustBeLessThanOutTime.HasValue)
                {
                    serviceFinishTimeMustBeLessThanOutTime = ConfigUtil.GetIntConfig(SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME_OPTION_CFG);
                }
                return serviceFinishTimeMustBeLessThanOutTime.Value;
            }
        }

        private static string defaultOfAutoEnd;
        public static string DEFAULT_OF_AUTO_END
        {
            get
            {
                if (defaultOfAutoEnd == null)
                {
                    defaultOfAutoEnd = ConfigUtil.GetStrConfig(DEFAULT_OF_AUTO_END_CFG);
                }
                return defaultOfAutoEnd;
            }
        }

        private static AutoSetIcdWhenFinishInAdditionExamOption? autoSetIcdWhenFinishInAdditionExamOption;
        public static AutoSetIcdWhenFinishInAdditionExamOption AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION
        {
            get
            {
                if (!autoSetIcdWhenFinishInAdditionExamOption.HasValue)
                {
                    autoSetIcdWhenFinishInAdditionExamOption = (AutoSetIcdWhenFinishInAdditionExamOption)ConfigUtil.GetIntConfig(AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION_CFG);
                }
                return autoSetIcdWhenFinishInAdditionExamOption.Value;
            }
        }

        private static bool? isGenerateStoreBordereauCodeWhenLockHein;
        public static bool IS_GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN
        {
            get
            {
                if (!isGenerateStoreBordereauCodeWhenLockHein.HasValue)
                {
                    isGenerateStoreBordereauCodeWhenLockHein = ConfigUtil.GetIntConfig(GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN_CFG) == 1;
                }
                return isGenerateStoreBordereauCodeWhenLockHein.Value;
            }
        }

        private static bool? isAllowDeletingIfExistServiceReq;
        public static bool IS_ALLOW_DELETING_IF_EXIST_SERVICE_REQ
        {
            get
            {
                if (!isAllowDeletingIfExistServiceReq.HasValue)
                {
                    isAllowDeletingIfExistServiceReq = ConfigUtil.GetIntConfig(IS_ALLOW_DELETING_IF_EXIST_SERVICE_REQ_CFG) == 1;
                }
                return isAllowDeletingIfExistServiceReq.Value;
            }
        }

        private static bool? isAllowInCodeGenerateTypeOption;
        public static bool IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION
        {
            get
            {
                if (!isAllowInCodeGenerateTypeOption.HasValue)
                {
                    isAllowInCodeGenerateTypeOption = ConfigUtil.GetIntConfig(IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION_CFG) == 1;
                }
                return isAllowInCodeGenerateTypeOption.Value;
            }
        }

        private static bool? autoCreateWhenAppointment;
        public static bool AUTO_CREATE_WHEN_APPOINTMENT
        {
            get
            {
                if (!autoCreateWhenAppointment.HasValue)
                {
                    autoCreateWhenAppointment = ConfigUtil.GetIntConfig(AUTO_CREATE_WHEN_APPOINTMENT_CFG) == 1;
                }
                return autoCreateWhenAppointment.Value;
            }
        }

        private static List<DeathSyncInfo> deathSyncInfo;
        public static List<DeathSyncInfo> DEATH_SYNC_INFO
        {
            get
            {
                if (deathSyncInfo == null)
                {
                    deathSyncInfo = GetByKey(DEATH_CONNECTION_INFO_CFG);
                }
                return deathSyncInfo;
            }
            set
            {
                deathSyncInfo = value;
            }
        }

        private static bool? isRequiredMrSummaryDetailWhenStore;
        public static bool IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE
        {
            get
            {
                if (!isRequiredMrSummaryDetailWhenStore.HasValue)
                {
                    isRequiredMrSummaryDetailWhenStore = ConfigUtil.GetIntConfig(IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE_CFG) == 1;
                }
                return isRequiredMrSummaryDetailWhenStore.Value;
            }
        }

        private static int? serviceFinishTimeMustBeLessThanPresTime;
        public static int SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME
        {
            get
            {
                if (!serviceFinishTimeMustBeLessThanPresTime.HasValue)
                {
                    serviceFinishTimeMustBeLessThanPresTime = ConfigUtil.GetIntConfig(SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME_OPTION_CFG);
                }
                return serviceFinishTimeMustBeLessThanPresTime.Value;
            }
        }
        private static List<DeathSyncInfo> GetByKey(string code)
        {
            List<DeathSyncInfo> result = new List<DeathSyncInfo>();
            try
            {
                string data = ConfigUtil.GetStrConfig(code);
                if (!string.IsNullOrWhiteSpace(data))
                {
                    string[] branchGroup = data.Split('|');
                    foreach (var item in branchGroup)
                    {
                        string[] sp = item.Split(';');
                        if (sp.Length > 4)
                        {
                            DeathSyncInfo info = new DeathSyncInfo();
                            info.HeinMediOrgCode = sp[0];
                            info.User = sp[1];
                            info.Password = sp[2];
                            info.Url = sp[3];
                            if (sp.Length > 4)
                            {
                                info.CertificateLink = sp[4];
                            }
                            if (sp.Length > 5)
                            {
                                info.CertificatePass = sp[5];
                            }

                            result.Add(info);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                result = new List<DeathSyncInfo>();
            }

            return result;
        }


        public static void Reload()
        {
            isAutoDetectReceivingDepartmentByFirstExamExecuteRoom = ConfigUtil.GetIntConfig(IS_AUTO_DETECT_RECEIVING_DEPARTMENT_BY_FIRST_EXAM_EXECUTE_ROOM_CFG) == 1;
            manyTreatmentPerDayOption = ConfigUtil.GetIntConfig(MANY_TREATMENT_PER_DAY_OPTION_CFG);
            allowManyTreatmentOpeningOption = (AlowManyTreatmentOpeningOption)ConfigUtil.GetIntConfig(ALLOW_MANY_TREATMENT_OPENING_OPTION_CFG);
            storeCodeOption = ConfigUtil.GetIntConfig(STORE_CODE_OPTION_CFG);
            storeCodeSeedTimeOption = ConfigUtil.GetIntConfig(STORE_CODE_SEED_TIME_OPTION_CFG);
            inCodeGenerateOption = ConfigUtil.GetIntConfig(IN_CODE_GENERATE_OPTION_CFG);
            inCodeFormatOption = ConfigUtil.GetIntConfig(IN_CODE_FORMAT_OPTION_CFG);
            mustFinishAllServicesBeforeFinishTreatment = (MustFinishAllServicesBeforeFinishTreatment)ConfigUtil.GetIntConfig(MUST_FINISH_ALL_SERVICES_BEFORE_FINISH_TREATMENT_CFG);
            mustFinishAllExamBeforeFinishTreatment = ConfigUtil.GetIntConfig(MUST_FINISH_ALL_EXAM_BEFORE_FINISH_TREATMENT_CFG) == 1;
            checkPreviousDebtOption = (CheckPreviousDebtOption)ConfigUtil.GetIntConfig(CHECK_PREVIOUS_DEBT_OPTION_CFG);
            isCheckPreviousPrescription = ConfigUtil.GetIntConfig(IS_CHECK_PREVIOUS_PRESCRIPTION_CFG) == 1;
            isCheckPreviousPrescriptionExam = ConfigUtil.GetIntConfig(IS_CHECK_PREVIOUS_PRESCRIPTION_EXAM_CFG) == 1;
            autoLockAfterBill = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_BILL_CFG) == 1;
            autoLockAfterRepay = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_REPAY_CFG) == 1;
            autoLockAfterHeinApproval = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_HEIN_APPROVAL_CFG) == 1;
            autoHeinApprovalAfterFeeLockOption = (AutoHeinApprovalAfterFeeLockOption)ConfigUtil.GetIntConfig(AUTO_HEIN_APPROVAL_AFTER_FEE_LOCK_OPTION_CFG);
            isManualEndCode = ConfigUtil.GetIntConfig(IS_MANUAL_END_CODE_CFG) == 1;
            isPricingFirstExamAsMainExam = ConfigUtil.GetIntConfig(IS_PRICING_FIRST_EXAM_AS_MAIN_EXAM_CFG) == 1;
            finishTimeNotGreaterThanCurrentTime = ConfigUtil.GetIntConfig(FINISH_TIME_NOT_GREATER_THAN_CURRENT_TIME_CFG) == 1;
            isUsingLastestIcd = ConfigUtil.GetIntConfig(IS_USING_LASTEST_ICD_CFG) == 1;
            isCheckTodayFinishTreatment = ConfigUtil.GetIntConfig(IS_CHECK_TODAY_FINISH_TREATMENT_CFG) == 1;
            isUsingAppointmentServiceInKioskReception = ConfigUtil.GetIntConfig(IS_USING_APPOINTMENT_SERVICE_IN_KIOSK_RECEPTION_CFG) == 1;
            allowedAppointmentDayAfter = ConfigUtil.GetIntConfig(ALLOWED_APPOINTMENT_DAY_AFTER_CFG);
            allowedAppointmentDayBefore = ConfigUtil.GetIntConfig(ALLOWED_APPOINTMENT_DAY_BEFORE_CFG);
            isKeepingStoreCode = ConfigUtil.GetIntConfig(IS_KEEPING_STORE_CODE_CFG) == 1;
            DoNotClearOutTimeWhenReopening = ConfigUtil.GetIntConfig(DONOT_CLEAR_OUT_TIME_WHEN_REOPENING_CFG) == 1;
            mustSetProgramWhenFinishingInPatient = ConfigUtil.GetIntConfig(MUST_SET_PROGRAM_WHEN_FINISHING_IN_PATIENT_CFG) == 1;
            xml2076FolderPath = ConfigUtil.GetStrConfig(XML2076_FOLDER_PATH_CFG);

            try
            {
                endCodeOption = (EndCodeOption)ConfigUtil.GetIntConfig(END_CODE_OPTION_CFG);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
            }

            isNotAllowingChangingPatientIfHeinCardConflict = ConfigUtil.GetIntConfig(IS_NOT_ALLOWING_CHANGING_PATIENT_IF_HEIN_CARD_CONFLICT_CFG) == 1;

            startEndCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_END_CODE_CFG));
            startInCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_IN_CODE_CFG));
            startStoreCode = HisTreatmentCFG.GetStartCodeConfig(ConfigUtil.GetStrConfig(START_STORE_CODE_CFG));
            doNotAllowDeletingIfExistTransaction = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_DELETING_IF_EXIST_TRANSACTION_CFG) == 1;
            autoLockAfterFinishIfHasNoPatientPrice = ConfigUtil.GetIntConfig(AUTO_LOCK_AFTER_FINISH_IF_HAS_NO_PATIENT_PRICE_CFG) == 1;
            IsDoNotAllowToLockFeeIfMustPay = ConfigUtil.GetIntConfig(DO_NOT_ALLOW_TO_LOCK_FEE_IF_MUST_PAY) == 1;
            isUsingOutTimeForFeeLockTimeInCaseOfAuto = ConfigUtil.GetIntConfig(IS_USING_OUT_TIME_FOR_FEE_LOCK_TIME_IN_CASE_OF_AUTO_CFG) == 1;
            allowFeeLockTimeGreaterThanSystemTime = ConfigUtil.GetIntConfig(ALLOW_FEE_LOCK_TIME_GREATER_THAN_SYSTEM_TIME_CFG) == 1;
            generateOutCodeInCaseOfTempFinishing = ConfigUtil.GetIntConfig(GENERATE_OUT_CODE_IN_CASE_OF_TEMP_FINISHING_CFG) == 1;
            warningOverSixMonths = ConfigUtil.GetIntConfig(CONFIG_KEY__WARNING_OVER_SIX_MONTH) == 1;
            mustFinishAllExamForAddExam = ConfigUtil.GetIntConfig(CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_ADD_EXAM) == 1;
            mustFinishAllExamForHospitalize = ConfigUtil.GetIntConfig(CONFIG_KEY__MUST_FINISH_ALL_EXAM_FOR_HOSPITALIZE) == 1;
            doNotAllowCreateOrEditTracking = ConfigUtil.GetIntConfig(CONFIG_KEY__DOT_NOT_ALLOW_CREATE_OR_EDIT_TRACKING) == 1;
            notifyApproveMediRecordWhenTreatmentFinish = ConfigUtil.GetIntConfig(CONFIG_KEY__NOTIFY_APPROVE_MEDI_REOCORD_WHEN_FINISH_TREATMENT) == 1;
            autoStoreMediRecordByProgram = ConfigUtil.GetIntConfig(AUTO_STORE_MEDI_RECORD_BY_PROGRAM_CFG) == 1;
            generateEndCodeInCaseOfTempFinishing = ConfigUtil.GetIntConfig(GENERATE_END_CODE_IN_CASE_OF_TEMP_FINISHING_CFG) == 1;

            try
            {
                approveMediRecordBeforeLockFeeOption = (ApproveMediRecordBeforeLockFeeOption)ConfigUtil.GetIntConfig(CONFIG_KEY__HIS_TREATMENT__LOCK_FEE_AFTER_APPROVE_MEDI_RECORD__OPTION);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            verifyInvoiceInfoMaterialBeforeLockFee = ConfigUtil.GetIntConfig(VERIFY_INVOICE_INFO_MATERIAL_BEFORE_LOCK_FEE_CFG) == 1;
            xml4210CollinearFolderPath = ConfigUtil.GetStrConfig(XML4210_COLLINEAR_FOLDER_PATH_CFG);
            blockAppointmentOption = ConfigUtil.GetIntConfig(CONFIG_KEY__FINISH__BLOCK_APPOINTMENT);
            isManualInCode = ConfigUtil.GetIntConfig(IS_MANUAL_IN_CODE_CFG) == 1;

            storeMediRecordByProgramIncaseOfTemporaryFinishing = ConfigUtil.GetIntConfig(STORE_MEDI_RECORD_BY_PROGRAM_INCASE_OF_TEMPORARY_FINISHING_CFG) == 1;
            xmlCheckInFolderPath = ConfigUtil.GetStrConfig(XML_CHECK_IN_FOLDER_PATH_CFG);
            autoExportXmlCheckIn = ConfigUtil.GetIntConfig(AUTO_EXPORT_XML_CHECK_IN_CFG) == 1;
            usingExamSubIcdWhenFinishOption = (UsingExamSubIcdWhenFinishOption)ConfigUtil.GetIntConfig(USING_EXAM_SUB_ICD_WHEN_FINISH_OPTON_CFG);
            updateInfoOption = ConfigUtil.GetIntConfig(UPDATE_INFO_OPTION_CFG) == 1;
            isIntructionTimeMustNotGreaterThanFinishTime = ConfigUtil.GetIntConfig(INTRUCTION_TIME_MUST_NOT_GREATER_THAN_FINISH_TIME_CFG) == 1;
            serviceFinishTimeMustBeLessThanOutTime = ConfigUtil.GetIntConfig(SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_OUT_TIME_OPTION_CFG);
            autoSetIcdWhenFinishInAdditionExamOption = (AutoSetIcdWhenFinishInAdditionExamOption)ConfigUtil.GetIntConfig(AUTO_SET_ICD_WHEN_FINISH_IN_ADDITION_EXAM_OPTION_CFG);
            isGenerateStoreBordereauCodeWhenLockHein = ConfigUtil.GetIntConfig(GENERATE_STORE_BORDEREAU_CODE_WHEN_LOCK_HEIN_CFG) == 1;
            isAllowDeletingIfExistServiceReq = ConfigUtil.GetIntConfig(IS_ALLOW_DELETING_IF_EXIST_SERVICE_REQ_CFG) == 1;
            defaultOfAutoEnd = ConfigUtil.GetStrConfig(DEFAULT_OF_AUTO_END_CFG);
            isAllowInCodeGenerateTypeOption = ConfigUtil.GetIntConfig(IS_ALLOW_IN_CODE_GENERATE_TYPE_OPTION_CFG) == 1;
            try
            {
                mustApproveRationBeforeFinish = (MustApproveRationBeforeFinishTreatment)ConfigUtil.GetIntConfig(FINISH_MUST_APPROVE_ALL_RATION_CFG);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }
            try
            {
                miniumTimesForExamWithSubclinicals = ConfigUtil.GetIntConfig(CFG_MINIMUM_TIMES_FOR_EXAM_WITH_SUBCLINICALS);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
            }

            autoCreateWhenAppointment = ConfigUtil.GetIntConfig(AUTO_CREATE_WHEN_APPOINTMENT_CFG) == 1;
            deathSyncInfo = GetByKey(DEATH_CONNECTION_INFO_CFG);
            isRequiredMrSummaryDetailWhenStore = ConfigUtil.GetIntConfig(IS_REQUIRED_MR_SUMMARY_DETAIL_WHEN_STORE_CFG) == 1;
            serviceFinishTimeMustBeLessThanPresTime = ConfigUtil.GetIntConfig(SERVICE_FINISH_TIME_MUST_BE_LESS_THAN_PRES_TIME_OPTION_CFG);
        }
    }
}
