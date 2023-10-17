using MOS.EFMODEL.DataModels;
using MOS.MANAGER.HisExpMestReason;
using MOS.MANAGER.HisExpMestType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.Config
{
    public class HisExpMestCFG
    {
        public enum CheckUnpaidPresOption
        {
            BY_PRES_TYPE = 1,
            BY_TREATMENT_TYPE = 2
        }

        public enum SpecialMedicineNumOrderOption
        {
            BY_YEAR__TYPE__REQ_DEPARTMENT__MEDI_STOCK = 1,
            NONE = 0
        }

        /// <summary>
        /// Nhom thuoc dac biet phuc vu danh so phieu linh/tra
        /// </summary>
        public enum SpecialMedicineType
        {
            /// <summary>
            /// 1: Gay nghien, huong than
            /// </summary>
            GN_HT = 1,

            /// <summary>
            /// 2: thuoc doc 
            /// </summary>
            DOC = 2
        }

        /// <summary>
        /// Cấu hình khi tự động duyệt phiếu bù sơ số thì có cho phép số lượng duyệt nhỏ hơn số lượng yêu cầu hay không. 1 : cho phép
        /// </summary>
        private const string CONFIG_KEY_ALLOW_AUTO_APPROVE_LESS_THAN_REQUEST__BCS = "HIS_EXP_MEST.EXP_MEST_TYPE.BCS.IS_ALLOW_AUTO_APPROVE_LESS_THAN_REQUEST";
        private const string CONFIG_KEY_AUTO_CREATE_AGGR_EXAM_EXP_MEST = "HIS_EXP_MEST.EXP_MEST_TYPE.THPK.AUTO_CREATE_AGGR_EXAM_EXP_MEST";
        private const string CONFIG_KEU_ALLOW_MOBA_EXAM_PRES = "HIS_EXP_MEST.EXP_MEST_TYPE.EXAM_PRES.IS_ALLOW_MOBA";
        private const string CONFIG_ALLOW_APPROVE_OTHER_TYPE__BCS = "MOS.HIS_EXP_MEST.BCS.APPROVE_OTHER_TYPE.IS_ALLOW";
        private const string CONFIG_EXP_MEST__OUT_PRES__APPROVE__IS_CHECK_UNPAID = "MOS.HIS_EXP_MEST.OUT_PRES.APPROVE.IS_CHECK_UNPAID";
        private const string CONFIG_KEY_EXP_MEST_SALE__EXPORT_MUST_BILL = "MOS.EXP_MEST.EXPORT_SALE.MUST_BILL";
        private const string CONFIG_KEY_EXP_MEST_SALE__AUTO_EXPORT = "MOS.TRANSACTION.EXP_MEST_SALE.IS_AUTO_EXPORT";
        private const string CONFIG_KEY_EXP_MEST_REASON__INVEN_CODE = "MOS.HIS_EXP_MEST_REASON.EXP_MEST_REASON_CODE.INVENTORY";
        private const string CONFIG_KEY_EXP_MEST__SPLIT_STAR_MARK = "MOS.HIS_SERVICE_REQ.SPLIT_STAR_MARK_PRESCRIPTION";
        private const string CONFIG_KEY_EXP_MEST__DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT = "MOS.HIS_EXP_MEST.DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT";
        private const string CONFIG_KEY_EXP_MEST__ERX_CONFIG = "HIS.Desktop.Plugins.InterconnectionPrescription.SysConfig";
        private const string CONFIG_KEY_TYPE_DO_NOT_ALLOW_SENDING = "MOS.INTERCONNECTION_PRESCRIPTION.TYPE_DO_NOT_ALLOW_SENDING";
        private const string CONFIG_KEY_AUTO_SET_IS_NOT_TAKEN_BY_DAY = "HIS.EXP_MEST.IN_PRES.AUTO_SET_IS_NOT_TAKEN.PRERIOD_BY_DAY";
        private const string CONFIG_KEY_MANAGE_PATIENT_IN_SALE = "MOS.HIS_EXP_MEST.MANAGE_PATIENT_IN_SALE";
        private const string CHECK_UNPAID_PRES_OPTION_CFG = "MOS.HIS_EXP_MEST.CHECK_UNPAID_PRES_OPTION";
        private const string IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE_CFG = "MOS.HIS_EXP_MEST.IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE";
        private const string SPECIAL_MEDICINE_NUM_ORDER_OPTION_CFG = "MOS.HIS_EXP_MEST.SPECIAL_MEDICINE_NUM_ORDER_OPTION";
        private const string DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST_CFG = "MOS.HIS_EXP_MEST.DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST";
        private const string IS_BLOOD_EXP_PRICE_OPTION_CFG = "MOS.HIS_SERVICE_REQ.BLOOD_EXP_PRICE_OPTION";
        private const string IS_REASON_REQUIRED_CFG = "MOS.EXP_MEST.IS_REASON_REQUIRED";
        private const string CONFIG_KEY_EXP_MEST__DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST = "MOS.HIS_EXP_MEST.DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST";
        private const string CONFIG_KEY_EXP_MEST__AUTO_SET_IS_NOT_TAKEN_WITH_EXP_MEST_TYPE_CODES = "MOS.HIS_EXP_MEST.AUTO_SET_IS_NOT_TAKEN.EXP_MEST_TYPE_CODES";

        public const string ControlCode_DELETE = "HIS000030";

        private static bool? disallowToExportAnapprovedUsingRequest;
        public static bool DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST
        {
            get
            {
                if (!disallowToExportAnapprovedUsingRequest.HasValue)
                {
                    disallowToExportAnapprovedUsingRequest = ConfigUtil.GetIntConfig(DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST_CFG) == 1;
                }
                return disallowToExportAnapprovedUsingRequest.Value;
            }
        }

        private static bool? isAutoCancelTransactionInCaseOfUnexportSale;
        public static bool IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE
        {
            get
            {
                if (!isAutoCancelTransactionInCaseOfUnexportSale.HasValue)
                {
                    isAutoCancelTransactionInCaseOfUnexportSale = ConfigUtil.GetIntConfig(IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE_CFG) == 1;
                }
                return isAutoCancelTransactionInCaseOfUnexportSale.Value;
            }
        }
        
        private static bool? allowApproveLessThanRequest;
        public static bool ALLOW_APPROVE_LESS_THAN_REQUEST
        {
            get
            {
                if (!allowApproveLessThanRequest.HasValue)
                {
                    allowApproveLessThanRequest = ConfigUtil.GetIntConfig(CONFIG_KEY_ALLOW_AUTO_APPROVE_LESS_THAN_REQUEST__BCS) == 1;
                }
                return allowApproveLessThanRequest.Value;
            }
        }

        private static bool? managePatientInSale;
        public static bool MANAGE_PATIENT_IN_SALE
        {
            get
            {
                if (!managePatientInSale.HasValue)
                {
                    managePatientInSale = ConfigUtil.GetIntConfig(CONFIG_KEY_MANAGE_PATIENT_IN_SALE) == 1;
                }
                return managePatientInSale.Value;
            }
        }

        private static bool? doNotAllowUnexportAfterTreatmentFinishingInCaseOfInPatient;
        public static bool DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT
        {
            get
            {
                if (!doNotAllowUnexportAfterTreatmentFinishingInCaseOfInPatient.HasValue)
                {
                    doNotAllowUnexportAfterTreatmentFinishingInCaseOfInPatient = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT) == 1;
                }
                return doNotAllowUnexportAfterTreatmentFinishingInCaseOfInPatient.Value;
            }
        }

        private static bool? doNotAllowMobaHasTransactionSaleExpMest;
        public static bool DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST
        {
            get
            {
                if (!doNotAllowMobaHasTransactionSaleExpMest.HasValue)
                {
                    doNotAllowMobaHasTransactionSaleExpMest = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST) == 1;
                }
                return doNotAllowMobaHasTransactionSaleExpMest.Value;
            }
        }

        private static bool? autoCreateAggrExamExpMest;
        public static bool AUTO_CREATE_AGGR_EXAM_EXP_MEST
        {
            get
            {
                if (!autoCreateAggrExamExpMest.HasValue)
                {
                    autoCreateAggrExamExpMest = ConfigUtil.GetIntConfig(CONFIG_KEY_AUTO_CREATE_AGGR_EXAM_EXP_MEST) == 1;
                }
                return autoCreateAggrExamExpMest.Value;
            }
        }

        private static bool? allowMobaExamPres;
        public static bool ALLOW_MOBA_EXAM_PRES
        {
            get
            {
                if (!allowMobaExamPres.HasValue)
                {
                    allowMobaExamPres = ConfigUtil.GetIntConfig(CONFIG_KEU_ALLOW_MOBA_EXAM_PRES) == 1;
                }
                return allowMobaExamPres.Value;
            }
        }

        private static bool? alloApproveOtherType;
        public static bool ALLOW_APPROVE_OTHER_TYPE
        {
            get
            {
                if (!alloApproveOtherType.HasValue)
                {
                    alloApproveOtherType = ConfigUtil.GetIntConfig(CONFIG_ALLOW_APPROVE_OTHER_TYPE__BCS) == 1;
                }
                return alloApproveOtherType.Value;
            }
        }

        private static bool? outPresIsCheckUnpaid;
        public static bool OUT_PRES_IS_CHECK_UNPAID
        {
            get
            {
                if (!outPresIsCheckUnpaid.HasValue)
                {
                    outPresIsCheckUnpaid = ConfigUtil.GetIntConfig(CONFIG_EXP_MEST__OUT_PRES__APPROVE__IS_CHECK_UNPAID) == 1;
                }
                return outPresIsCheckUnpaid.Value;
            }
        }

        private static bool? exportSaleMustBill;
        public static bool EXPORT_SALE_MUST_BILL
        {
            get
            {
                if (!exportSaleMustBill.HasValue)
                {
                    exportSaleMustBill = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST_SALE__EXPORT_MUST_BILL) == 1;
                }
                return exportSaleMustBill.Value;
            }
        }

        private static bool? isAutoExportExpMestSale;
        public static bool IS_AUTO_EXPORT_EXP_MEST_SALE
        {
            get
            {
                if (!isAutoExportExpMestSale.HasValue)
                {
                    isAutoExportExpMestSale = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST_SALE__AUTO_EXPORT) == 1;
                }
                return isAutoExportExpMestSale.Value;
            }
        }

        private static long? expMestReasonInveId;
        public static long EXP_MEST_REASON_INVE_ID
        {
            get
            {
                if (!expMestReasonInveId.HasValue)
                {
                    expMestReasonInveId = GetReasonId(ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST_REASON__INVEN_CODE));
                }
                return expMestReasonInveId.Value;
            }
        }

        private static bool? isSplitStarMark;
        public static bool IS_SPLIT_STAR_MARK
        {
            get
            {
                if (!isSplitStarMark.HasValue)
                {
                    isSplitStarMark = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__SPLIT_STAR_MARK) == 1;
                }
                return isSplitStarMark.Value;
            }
        }

        private static string erxConfig;
        public static string ERX_CONFIG
        {
            get
            {
                if (String.IsNullOrWhiteSpace(erxConfig))
                {
                    erxConfig = ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST__ERX_CONFIG);
                }
                return erxConfig;
            }
        }

        private static string typeDoNotAllowSending;
        public static string TYPE_DO_NOT_ALLOW_SENDING
        {
            get
            {
                if (String.IsNullOrWhiteSpace(typeDoNotAllowSending))
                {
                    typeDoNotAllowSending = ConfigUtil.GetStrConfig(CONFIG_KEY_TYPE_DO_NOT_ALLOW_SENDING);
                }
                return typeDoNotAllowSending;
            }
        }

        private static long? notTakenByDays;
        public static long NOT_TAKEN_BY_DAYs
        {
            get
            {
                if (!notTakenByDays.HasValue)
                {
                    notTakenByDays = ConfigUtil.GetLongConfig(CONFIG_KEY_AUTO_SET_IS_NOT_TAKEN_BY_DAY);
                }
                return notTakenByDays.Value;
            }
        }

        private static CheckUnpaidPresOption? checkUnpaidPresOption;
        public static CheckUnpaidPresOption CHECK_UNPAID_PRES_OPTION
        {
            get
            {
                if (!checkUnpaidPresOption.HasValue)
                {
                    checkUnpaidPresOption = (CheckUnpaidPresOption)ConfigUtil.GetIntConfig(CHECK_UNPAID_PRES_OPTION_CFG);
                }
                return checkUnpaidPresOption.Value;
            }
        }

        private static SpecialMedicineNumOrderOption? specialMedicineNumOrderOption;
        public static SpecialMedicineNumOrderOption SPECIAL_MEDICINE_NUM_ORDER_OPTION
        {
            get
            {
                if (!specialMedicineNumOrderOption.HasValue)
                {
                    specialMedicineNumOrderOption = (SpecialMedicineNumOrderOption)ConfigUtil.GetIntConfig(SPECIAL_MEDICINE_NUM_ORDER_OPTION_CFG);
                }
                return specialMedicineNumOrderOption.Value;
            }
        }

        private static bool? isBloodExpPriceOption;
        public static bool IS_BLOOD_EXP_PRICE_OPTION
        {
            get
            {
                if (!isBloodExpPriceOption.HasValue)
                {
                    isBloodExpPriceOption = ConfigUtil.GetIntConfig(IS_BLOOD_EXP_PRICE_OPTION_CFG) == 1;
                }
                return isBloodExpPriceOption.Value;
            }
        }

        private static bool? isReasonRequired;
        public static bool IS_REASON_REQUIRED
        {
            get
            {
                if (!isReasonRequired.HasValue)
                {
                    isReasonRequired = ConfigUtil.GetIntConfig(IS_REASON_REQUIRED_CFG) == 1;
                }
                return isReasonRequired.Value;
            }
        }

        private static string expMestTypeCodesWhenAutoSetIsNotTaken;
        public static string EXP_MEST_TYPE_CODES_WHEN_AUTO_SET_IS_NOT_TAKEN
        {
            get
            {
                if (String.IsNullOrWhiteSpace(expMestTypeCodesWhenAutoSetIsNotTaken))
                {
                    expMestTypeCodesWhenAutoSetIsNotTaken = ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST__AUTO_SET_IS_NOT_TAKEN_WITH_EXP_MEST_TYPE_CODES);
                }
                return expMestTypeCodesWhenAutoSetIsNotTaken;
            }
        }

        private static long GetReasonId(string code)
        {
            long result = 0;
            try
            {
                if (String.IsNullOrWhiteSpace(code))
                {
                    throw new ArgumentNullException("Code");
                }

                HIS_EXP_MEST_REASON rs = new HisExpMestReasonGet().GetByCode(code);
                if (rs == null)
                {
                    throw new Exception("Code invalid: " + code);
                }
                result = rs.ID;
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = 0;
            }
            return result;
        }

        public static void Reload()
        {
            allowApproveLessThanRequest = ConfigUtil.GetIntConfig(CONFIG_KEY_ALLOW_AUTO_APPROVE_LESS_THAN_REQUEST__BCS) == 1;
            autoCreateAggrExamExpMest = ConfigUtil.GetIntConfig(CONFIG_KEY_AUTO_CREATE_AGGR_EXAM_EXP_MEST) == 1;
            allowMobaExamPres = ConfigUtil.GetIntConfig(CONFIG_KEU_ALLOW_MOBA_EXAM_PRES) == 1;
            alloApproveOtherType = ConfigUtil.GetIntConfig(CONFIG_ALLOW_APPROVE_OTHER_TYPE__BCS) == 1;
            outPresIsCheckUnpaid = ConfigUtil.GetIntConfig(CONFIG_EXP_MEST__OUT_PRES__APPROVE__IS_CHECK_UNPAID) == 1;
            exportSaleMustBill = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST_SALE__EXPORT_MUST_BILL) == 1;
            isAutoExportExpMestSale = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST_SALE__AUTO_EXPORT) == 1;
            expMestReasonInveId = GetReasonId(ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST_REASON__INVEN_CODE));
            isSplitStarMark = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__SPLIT_STAR_MARK) == 1;
            doNotAllowUnexportAfterTreatmentFinishingInCaseOfInPatient = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__DONOT_ALLOW_UNEXPORT_AFTER_TREATMENT_FINISHING_IN_CASE_OF_INPATIENT) == 1;
            doNotAllowMobaHasTransactionSaleExpMest = ConfigUtil.GetIntConfig(CONFIG_KEY_EXP_MEST__DO_NOT_ALLOW_MOBA_HAS_TRANSACTION_SALE_EXP_MEST) == 1;
            typeDoNotAllowSending = ConfigUtil.GetStrConfig(CONFIG_KEY_TYPE_DO_NOT_ALLOW_SENDING);
            erxConfig = ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST__ERX_CONFIG);
            managePatientInSale = ConfigUtil.GetIntConfig(CONFIG_KEY_MANAGE_PATIENT_IN_SALE) == 1;
            checkUnpaidPresOption = (CheckUnpaidPresOption)ConfigUtil.GetIntConfig(CHECK_UNPAID_PRES_OPTION_CFG);
            isAutoCancelTransactionInCaseOfUnexportSale = ConfigUtil.GetIntConfig(IS_AUTO_CANCEL_TRANSACTION_IN_CASE_OF_UNEXPORT_SALE_CFG) == 1;
            specialMedicineNumOrderOption = (SpecialMedicineNumOrderOption)ConfigUtil.GetIntConfig(SPECIAL_MEDICINE_NUM_ORDER_OPTION_CFG);
            disallowToExportAnapprovedUsingRequest = ConfigUtil.GetIntConfig(DISALLOW_TO_EXPORT_UNAPPROVED_USING_REQUEST_CFG) == 1;
            isBloodExpPriceOption = ConfigUtil.GetIntConfig(IS_BLOOD_EXP_PRICE_OPTION_CFG) == 1;
            isReasonRequired = ConfigUtil.GetIntConfig(IS_REASON_REQUIRED_CFG) == 1;
            expMestTypeCodesWhenAutoSetIsNotTaken = ConfigUtil.GetStrConfig(CONFIG_KEY_EXP_MEST__AUTO_SET_IS_NOT_TAKEN_WITH_EXP_MEST_TYPE_CODES);
        }
    }
}
