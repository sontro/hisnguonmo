using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisService : INullable, IOracleCustomType
    {
        private bool objectIsNull;

        [OracleObjectMappingAttribute("ID")]
        public long ID { get; set; }
        [OracleObjectMappingAttribute("CREATE_TIME")]
        public Nullable<long> CREATE_TIME { get; set; }
        [OracleObjectMappingAttribute("MODIFY_TIME")]
        public Nullable<long> MODIFY_TIME { get; set; }
        [OracleObjectMappingAttribute("CREATOR")]
        public string CREATOR { get; set; }
        [OracleObjectMappingAttribute("MODIFIER")]
        public string MODIFIER { get; set; }
        [OracleObjectMappingAttribute("APP_CREATOR")]
        public string APP_CREATOR { get; set; }
        [OracleObjectMappingAttribute("APP_MODIFIER")]
        public string APP_MODIFIER { get; set; }
        [OracleObjectMappingAttribute("IS_ACTIVE")]
        public Nullable<short> IS_ACTIVE { get; set; }
        [OracleObjectMappingAttribute("IS_DELETE")]
        public Nullable<short> IS_DELETE { get; set; }
        [OracleObjectMappingAttribute("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
        [OracleObjectMappingAttribute("SERVICE_CODE")]
        public string SERVICE_CODE { get; set; }
        [OracleObjectMappingAttribute("SERVICE_NAME")]
        public string SERVICE_NAME { get; set; }
        [OracleObjectMappingAttribute("SERVICE_TYPE_ID")]
        public long SERVICE_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("SERVICE_UNIT_ID")]
        public long SERVICE_UNIT_ID { get; set; }
        [OracleObjectMappingAttribute("PARENT_ID")]
        public Nullable<long> PARENT_ID { get; set; }
        [OracleObjectMappingAttribute("IS_LEAF")]
        public Nullable<short> IS_LEAF { get; set; }
        [OracleObjectMappingAttribute("NUM_ORDER")]
        public Nullable<long> NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("HEIN_SERVICE_TYPE_ID")]
        public Nullable<long> HEIN_SERVICE_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("HEIN_SERVICE_BHYT_CODE")]
        public string HEIN_SERVICE_BHYT_CODE { get; set; }
        [OracleObjectMappingAttribute("HEIN_SERVICE_BHYT_NAME")]
        public string HEIN_SERVICE_BHYT_NAME { get; set; }
        [OracleObjectMappingAttribute("HEIN_ORDER")]
        public string HEIN_ORDER { get; set; }
        [OracleObjectMappingAttribute("IS_USE_SERVICE_HEIN")]
        public Nullable<short> IS_USE_SERVICE_HEIN { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_PRICE_OLD")]
        public Nullable<decimal> HEIN_LIMIT_PRICE_OLD { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_RATIO_OLD")]
        public Nullable<decimal> HEIN_LIMIT_RATIO_OLD { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_PRICE")]
        public Nullable<decimal> HEIN_LIMIT_PRICE { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_RATIO")]
        public Nullable<decimal> HEIN_LIMIT_RATIO { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_PRICE_IN_TIME")]
        public Nullable<long> HEIN_LIMIT_PRICE_IN_TIME { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_PRICE_INTR_TIME")]
        public Nullable<long> HEIN_LIMIT_PRICE_INTR_TIME { get; set; }
        [OracleObjectMappingAttribute("SPECIALITY_CODE")]
        public string SPECIALITY_CODE { get; set; }
        [OracleObjectMappingAttribute("IS_MULTI_REQUEST")]
        public Nullable<short> IS_MULTI_REQUEST { get; set; }
        [OracleObjectMappingAttribute("MAX_EXPEND")]
        public Nullable<decimal> MAX_EXPEND { get; set; }
        [OracleObjectMappingAttribute("BILL_OPTION")]
        public Nullable<short> BILL_OPTION { get; set; }
        [OracleObjectMappingAttribute("BILL_PATIENT_TYPE_ID")]
        public Nullable<long> BILL_PATIENT_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_PARENT_FEE")]
        public Nullable<short> IS_OUT_PARENT_FEE { get; set; }
        [OracleObjectMappingAttribute("PTTT_GROUP_ID")]
        public Nullable<long> PTTT_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("PTTT_METHOD_ID")]
        public Nullable<long> PTTT_METHOD_ID { get; set; }
        [OracleObjectMappingAttribute("ICD_CM_ID")]
        public Nullable<long> ICD_CM_ID { get; set; }
        [OracleObjectMappingAttribute("COGS")]
        public Nullable<decimal> COGS { get; set; }
        [OracleObjectMappingAttribute("ESTIMATE_DURATION")]
        public Nullable<decimal> ESTIMATE_DURATION { get; set; }
        [OracleObjectMappingAttribute("REVENUE_DEPARTMENT_ID")]
        public Nullable<long> REVENUE_DEPARTMENT_ID { get; set; }
        [OracleObjectMappingAttribute("PACKAGE_ID")]
        public Nullable<long> PACKAGE_ID { get; set; }
        [OracleObjectMappingAttribute("PACKAGE_PRICE")]
        public Nullable<decimal> PACKAGE_PRICE { get; set; }
        [OracleObjectMappingAttribute("NUMBER_OF_FILM")]
        public Nullable<long> NUMBER_OF_FILM { get; set; }
        [OracleObjectMappingAttribute("PACS_TYPE_CODE")]
        public string PACS_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("MIN_DURATION")]
        public Nullable<long> MIN_DURATION { get; set; }
        [OracleObjectMappingAttribute("EXE_SERVICE_MODULE_ID")]
        public Nullable<long> EXE_SERVICE_MODULE_ID { get; set; }
        [OracleObjectMappingAttribute("GENDER_ID")]
        public Nullable<long> GENDER_ID { get; set; }
        [OracleObjectMappingAttribute("IS_ALLOW_EXPEND")]
        public Nullable<short> IS_ALLOW_EXPEND { get; set; }
        [OracleObjectMappingAttribute("AGE_FROM")]
        public Nullable<long> AGE_FROM { get; set; }
        [OracleObjectMappingAttribute("AGE_TO")]
        public Nullable<long> AGE_TO { get; set; }
        [OracleObjectMappingAttribute("RATION_GROUP_ID")]
        public Nullable<long> RATION_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("RATION_SYMBOL")]
        public Nullable<long> RATION_SYMBOL { get; set; }
        [OracleObjectMappingAttribute("IS_NO_HEIN_LIMIT_FOR_SPECIAL")]
        public Nullable<short> IS_NO_HEIN_LIMIT_FOR_SPECIAL { get; set; }
        [OracleObjectMappingAttribute("IS_KIDNEY")]
        public Nullable<short> IS_KIDNEY { get; set; }
        [OracleObjectMappingAttribute("NOTICE")]
        public string NOTICE { get; set; }
        [OracleObjectMappingAttribute("IS_SPECIFIC_HEIN_PRICE")]
        public Nullable<short> IS_SPECIFIC_HEIN_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_OTHER_SOURCE_PAID")]
        public Nullable<short> IS_OTHER_SOURCE_PAID { get; set; }
        [OracleObjectMappingAttribute("CAPACITY")]
        public Nullable<long> CAPACITY { get; set; }
        [OracleObjectMappingAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("IS_ANTIBIOTIC_RESISTANCE")]
        public Nullable<short> IS_ANTIBIOTIC_RESISTANCE { get; set; }
        [OracleObjectMappingAttribute("DIIM_TYPE_ID")]
        public Nullable<long> DIIM_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("FUEX_TYPE_ID")]
        public Nullable<long> FUEX_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TAX_RATE_TYPE")]
        public Nullable<long> TAX_RATE_TYPE { get; set; }
        [OracleObjectMappingAttribute("IS_SPLIT_SERVICE_REQ")]
        public Nullable<short> IS_SPLIT_SERVICE_REQ { get; set; }
        [OracleObjectMappingAttribute("IS_ENABLE_ASSIGN_PRICE")]
        public Nullable<short> IS_ENABLE_ASSIGN_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_AUTO_FINISH")]
        public Nullable<short> IS_AUTO_FINISH { get; set; }
        [OracleObjectMappingAttribute("PROCESS_CODE")]
        public string PROCESS_CODE { get; set; }
        [OracleObjectMappingAttribute("TEST_TYPE_ID")]
        public Nullable<long> TEST_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_OF_DRG")]
        public Nullable<short> IS_OUT_OF_DRG { get; set; }
        [OracleObjectMappingAttribute("IS_CONDITIONED")]
        public Nullable<short> IS_CONDITIONED { get; set; }
        [OracleObjectMappingAttribute("MIN_PROCESS_TIME")]
        public Nullable<long> MIN_PROCESS_TIME { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_CHANGE_BILL_PATY")]
        public Nullable<short> IS_NOT_CHANGE_BILL_PATY { get; set; }
        [OracleObjectMappingAttribute("IS_SPLIT_SERVICE")]
        public Nullable<short> IS_SPLIT_SERVICE { get; set; }
        [OracleObjectMappingAttribute("OTHER_PAY_SOURCE_ID")]
        public Nullable<long> OTHER_PAY_SOURCE_ID { get; set; }
        [OracleObjectMappingAttribute("OTHER_PAY_SOURCE_ICDS")]
        public string OTHER_PAY_SOURCE_ICDS { get; set; }
        [OracleObjectMappingAttribute("BODY_PART_IDS")]
        public string BODY_PART_IDS { get; set; }
        [OracleObjectMappingAttribute("TESTING_TECHNIQUE")]
        public string TESTING_TECHNIQUE { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_OF_MANAGEMENT")]
        public Nullable<short> IS_OUT_OF_MANAGEMENT { get; set; }
        [OracleObjectMappingAttribute("MUST_BE_CONSULTED")]
        public Nullable<short> MUST_BE_CONSULTED { get; set; }
        [OracleObjectMappingAttribute("SUIM_INDEX_ID")]
        public Nullable<long> SUIM_INDEX_ID { get; set; }
        [OracleObjectMappingAttribute("ATTACH_ASSIGN_PRINT_TYPE_CODE")]
        public string ATTACH_ASSIGN_PRINT_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("TEST_COVID_TYPE")]
        public Nullable<long> TEST_COVID_TYPE { get; set; }
        [OracleObjectMappingAttribute("MAX_PROCESS_TIME")]
        public Nullable<long> MAX_PROCESS_TIME { get; set; }
        [OracleObjectMappingAttribute("IS_DISALLOWANCE_NO_EXECUTE")]
        public Nullable<short> IS_DISALLOWANCE_NO_EXECUTE { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_SHOW_TRACKING")]
        public Nullable<short> IS_NOT_SHOW_TRACKING { get; set; }
        [OracleObjectMappingAttribute("FILM_SIZE_ID")]
        public Nullable<long> FILM_SIZE_ID { get; set; }
        [OracleObjectMappingAttribute("IS_AUTO_EXPEND")]
        public Nullable<short> IS_AUTO_EXPEND { get; set; }
        [OracleObjectMappingAttribute("IS_BLOCK_DEPARTMENT_TRAN")]
        public Nullable<short> IS_BLOCK_DEPARTMENT_TRAN { get; set; }
        [OracleObjectMappingAttribute("WARNING_SAMPLING_TIME")]
        public Nullable<long> WARNING_SAMPLING_TIME { get; set; }
        [OracleObjectMappingAttribute("APPLIED_PATIENT_TYPE_IDS")]
        public string APPLIED_PATIENT_TYPE_IDS { get; set; }
        [OracleObjectMappingAttribute("DEFAULT_PATIENT_TYPE_ID")]
        public Nullable<long> DEFAULT_PATIENT_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("MAX_TOTAL_PROCESS_TIME")]
        public Nullable<long> MAX_TOTAL_PROCESS_TIME { get; set; }
        [OracleObjectMappingAttribute("ALLOW_SIMULTANEITY")]
        public Nullable<long> ALLOW_SIMULTANEITY { get; set; }
        [OracleObjectMappingAttribute("SAMPLE_TYPE_CODE")]
        public string SAMPLE_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("MIN_PROC_TIME_EXCEPT_PATY_IDS")]
        public string MIN_PROC_TIME_EXCEPT_PATY_IDS { get; set; }
        [OracleObjectMappingAttribute("MAX_PROC_TIME_EXCEPT_PATY_IDS")]
        public string MAX_PROC_TIME_EXCEPT_PATY_IDS { get; set; }
        [OracleObjectMappingAttribute("MAX_AMOUNT")]
        public Nullable<long> MAX_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("ALLOW_SEND_PACS")]
        public Nullable<short> ALLOW_SEND_PACS { get; set; }
        [OracleObjectMappingAttribute("DO_NOT_USE_BHYT")]
        public Nullable<short> DO_NOT_USE_BHYT { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_REQUIRED_COMPLETE")]
        public Nullable<short> IS_NOT_REQUIRED_COMPLETE { get; set; }
        [OracleObjectMappingAttribute("PETROLEUM_CODE")]
        public string PETROLEUM_CODE { get; set; }
        [OracleObjectMappingAttribute("PETROLEUM_NAME")]
        public string PETROLEUM_NAME { get; set; }

        public static THisService Null
        {
            get
            {
                THisService company = new THisService();
                company.objectIsNull = true;
                return company;
            }
        }

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public void FromCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, "ID", ID);
            OracleUdt.SetValue(con, pUdt, "CREATE_TIME", CREATE_TIME);
            OracleUdt.SetValue(con, pUdt, "MODIFY_TIME", MODIFY_TIME);
            OracleUdt.SetValue(con, pUdt, "CREATOR", CREATOR);
            OracleUdt.SetValue(con, pUdt, "MODIFIER", MODIFIER);
            OracleUdt.SetValue(con, pUdt, "APP_CREATOR", APP_CREATOR);
            OracleUdt.SetValue(con, pUdt, "APP_MODIFIER", APP_MODIFIER);
            OracleUdt.SetValue(con, pUdt, "IS_ACTIVE", IS_ACTIVE);
            OracleUdt.SetValue(con, pUdt, "IS_DELETE", IS_DELETE);
            OracleUdt.SetValue(con, pUdt, "GROUP_CODE", GROUP_CODE);
            OracleUdt.SetValue(con, pUdt, "SERVICE_CODE", SERVICE_CODE);
            OracleUdt.SetValue(con, pUdt, "SERVICE_NAME", SERVICE_NAME);
            OracleUdt.SetValue(con, pUdt, "SERVICE_TYPE_ID", SERVICE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "SERVICE_UNIT_ID", SERVICE_UNIT_ID);
            OracleUdt.SetValue(con, pUdt, "PARENT_ID", PARENT_ID);
            OracleUdt.SetValue(con, pUdt, "IS_LEAF", IS_LEAF);
            OracleUdt.SetValue(con, pUdt, "NUM_ORDER", NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "HEIN_SERVICE_TYPE_ID", HEIN_SERVICE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "HEIN_SERVICE_BHYT_CODE", HEIN_SERVICE_BHYT_CODE);
            OracleUdt.SetValue(con, pUdt, "HEIN_SERVICE_BHYT_NAME", HEIN_SERVICE_BHYT_NAME);
            OracleUdt.SetValue(con, pUdt, "HEIN_ORDER", HEIN_ORDER);
            OracleUdt.SetValue(con, pUdt, "IS_USE_SERVICE_HEIN", IS_USE_SERVICE_HEIN);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_PRICE_OLD", HEIN_LIMIT_PRICE_OLD);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_RATIO_OLD", HEIN_LIMIT_RATIO_OLD);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_PRICE", HEIN_LIMIT_PRICE);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_RATIO", HEIN_LIMIT_RATIO);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_PRICE_IN_TIME", HEIN_LIMIT_PRICE_IN_TIME);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_PRICE_INTR_TIME", HEIN_LIMIT_PRICE_INTR_TIME);
            OracleUdt.SetValue(con, pUdt, "SPECIALITY_CODE", SPECIALITY_CODE);
            OracleUdt.SetValue(con, pUdt, "IS_MULTI_REQUEST", IS_MULTI_REQUEST);
            OracleUdt.SetValue(con, pUdt, "MAX_EXPEND", MAX_EXPEND);
            OracleUdt.SetValue(con, pUdt, "BILL_OPTION", BILL_OPTION);
            OracleUdt.SetValue(con, pUdt, "BILL_PATIENT_TYPE_ID", BILL_PATIENT_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_PARENT_FEE", IS_OUT_PARENT_FEE);
            OracleUdt.SetValue(con, pUdt, "PTTT_GROUP_ID", PTTT_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "PTTT_METHOD_ID", PTTT_METHOD_ID);
            OracleUdt.SetValue(con, pUdt, "ICD_CM_ID", ICD_CM_ID);
            OracleUdt.SetValue(con, pUdt, "COGS", COGS);
            OracleUdt.SetValue(con, pUdt, "ESTIMATE_DURATION", ESTIMATE_DURATION);
            OracleUdt.SetValue(con, pUdt, "REVENUE_DEPARTMENT_ID", REVENUE_DEPARTMENT_ID);
            OracleUdt.SetValue(con, pUdt, "PACKAGE_ID", PACKAGE_ID);
            OracleUdt.SetValue(con, pUdt, "PACKAGE_PRICE", PACKAGE_PRICE);
            OracleUdt.SetValue(con, pUdt, "NUMBER_OF_FILM", NUMBER_OF_FILM);
            OracleUdt.SetValue(con, pUdt, "PACS_TYPE_CODE", PACS_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "MIN_DURATION", MIN_DURATION);
            OracleUdt.SetValue(con, pUdt, "EXE_SERVICE_MODULE_ID", EXE_SERVICE_MODULE_ID);
            OracleUdt.SetValue(con, pUdt, "GENDER_ID", GENDER_ID);
            OracleUdt.SetValue(con, pUdt, "IS_ALLOW_EXPEND", IS_ALLOW_EXPEND);
            OracleUdt.SetValue(con, pUdt, "AGE_FROM", AGE_FROM);
            OracleUdt.SetValue(con, pUdt, "AGE_TO", AGE_TO);
            OracleUdt.SetValue(con, pUdt, "RATION_GROUP_ID", RATION_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "RATION_SYMBOL", RATION_SYMBOL);
            OracleUdt.SetValue(con, pUdt, "IS_NO_HEIN_LIMIT_FOR_SPECIAL", IS_NO_HEIN_LIMIT_FOR_SPECIAL);
            OracleUdt.SetValue(con, pUdt, "IS_KIDNEY", IS_KIDNEY);
            OracleUdt.SetValue(con, pUdt, "NOTICE", NOTICE);
            OracleUdt.SetValue(con, pUdt, "IS_SPECIFIC_HEIN_PRICE", IS_SPECIFIC_HEIN_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_OTHER_SOURCE_PAID", IS_OTHER_SOURCE_PAID);
            OracleUdt.SetValue(con, pUdt, "CAPACITY", CAPACITY);
            OracleUdt.SetValue(con, pUdt, "DESCRIPTION", DESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "IS_ANTIBIOTIC_RESISTANCE", IS_ANTIBIOTIC_RESISTANCE);
            OracleUdt.SetValue(con, pUdt, "DIIM_TYPE_ID", DIIM_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "FUEX_TYPE_ID", FUEX_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TAX_RATE_TYPE", TAX_RATE_TYPE);
            OracleUdt.SetValue(con, pUdt, "IS_SPLIT_SERVICE_REQ", IS_SPLIT_SERVICE_REQ);
            OracleUdt.SetValue(con, pUdt, "IS_ENABLE_ASSIGN_PRICE", IS_ENABLE_ASSIGN_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_AUTO_FINISH", IS_AUTO_FINISH);
            OracleUdt.SetValue(con, pUdt, "PROCESS_CODE", PROCESS_CODE);
            OracleUdt.SetValue(con, pUdt, "TEST_TYPE_ID", TEST_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_OF_DRG", IS_OUT_OF_DRG);
            OracleUdt.SetValue(con, pUdt, "IS_CONDITIONED", IS_CONDITIONED);
            OracleUdt.SetValue(con, pUdt, "MIN_PROCESS_TIME", MIN_PROCESS_TIME);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_CHANGE_BILL_PATY", IS_NOT_CHANGE_BILL_PATY);
            OracleUdt.SetValue(con, pUdt, "IS_SPLIT_SERVICE", IS_SPLIT_SERVICE);
            OracleUdt.SetValue(con, pUdt, "OTHER_PAY_SOURCE_ID", OTHER_PAY_SOURCE_ID);
            OracleUdt.SetValue(con, pUdt, "OTHER_PAY_SOURCE_ICDS", OTHER_PAY_SOURCE_ICDS);
            OracleUdt.SetValue(con, pUdt, "BODY_PART_IDS", BODY_PART_IDS);
            OracleUdt.SetValue(con, pUdt, "TESTING_TECHNIQUE", TESTING_TECHNIQUE);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_OF_MANAGEMENT", IS_OUT_OF_MANAGEMENT);
            OracleUdt.SetValue(con, pUdt, "MUST_BE_CONSULTED", MUST_BE_CONSULTED);
            OracleUdt.SetValue(con, pUdt, "SUIM_INDEX_ID", SUIM_INDEX_ID);
            OracleUdt.SetValue(con, pUdt, "ATTACH_ASSIGN_PRINT_TYPE_CODE", ATTACH_ASSIGN_PRINT_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "TEST_COVID_TYPE", TEST_COVID_TYPE);
            OracleUdt.SetValue(con, pUdt, "MAX_PROCESS_TIME", MAX_PROCESS_TIME);
            OracleUdt.SetValue(con, pUdt, "IS_DISALLOWANCE_NO_EXECUTE", IS_DISALLOWANCE_NO_EXECUTE);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_SHOW_TRACKING", IS_NOT_SHOW_TRACKING);
            OracleUdt.SetValue(con, pUdt, "FILM_SIZE_ID", FILM_SIZE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_AUTO_EXPEND", IS_AUTO_EXPEND);
            OracleUdt.SetValue(con, pUdt, "IS_BLOCK_DEPARTMENT_TRAN", IS_BLOCK_DEPARTMENT_TRAN);
            OracleUdt.SetValue(con, pUdt, "WARNING_SAMPLING_TIME", WARNING_SAMPLING_TIME);
            OracleUdt.SetValue(con, pUdt, "APPLIED_PATIENT_TYPE_IDS", APPLIED_PATIENT_TYPE_IDS);
            OracleUdt.SetValue(con, pUdt, "DEFAULT_PATIENT_TYPE_ID", DEFAULT_PATIENT_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "MAX_TOTAL_PROCESS_TIME", MAX_TOTAL_PROCESS_TIME);
            OracleUdt.SetValue(con, pUdt, "ALLOW_SIMULTANEITY", ALLOW_SIMULTANEITY);
            OracleUdt.SetValue(con, pUdt, "SAMPLE_TYPE_CODE", SAMPLE_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "MIN_PROC_TIME_EXCEPT_PATY_IDS", MIN_PROC_TIME_EXCEPT_PATY_IDS);
            OracleUdt.SetValue(con, pUdt, "MAX_PROC_TIME_EXCEPT_PATY_IDS", MAX_PROC_TIME_EXCEPT_PATY_IDS);
            OracleUdt.SetValue(con, pUdt, "MAX_AMOUNT", MAX_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "ALLOW_SEND_PACS", ALLOW_SEND_PACS);
            OracleUdt.SetValue(con, pUdt, "DO_NOT_USE_BHYT", DO_NOT_USE_BHYT);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_REQUIRED_COMPLETE", IS_NOT_REQUIRED_COMPLETE);
            OracleUdt.SetValue(con, pUdt, "PETROLEUM_CODE", PETROLEUM_CODE);
            OracleUdt.SetValue(con, pUdt, "PETROLEUM_NAME", PETROLEUM_NAME);
        }

        public void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            ID = (long)OracleUdt.GetValue(con, pUdt, "ID");
            CREATE_TIME = (long?)OracleUdt.GetValue(con, pUdt, "CREATE_TIME");
            MODIFY_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MODIFY_TIME");
            CREATOR = (string)OracleUdt.GetValue(con, pUdt, "CREATOR");
            MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "MODIFIER");
            APP_CREATOR = (string)OracleUdt.GetValue(con, pUdt, "APP_CREATOR");
            APP_MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "APP_MODIFIER");
            IS_ACTIVE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ACTIVE");
            IS_DELETE = (short?)OracleUdt.GetValue(con, pUdt, "IS_DELETE");
            GROUP_CODE = (string)OracleUdt.GetValue(con, pUdt, "GROUP_CODE");
            SERVICE_CODE = (string)OracleUdt.GetValue(con, pUdt, "SERVICE_CODE");
            SERVICE_NAME = (string)OracleUdt.GetValue(con, pUdt, "SERVICE_NAME");
            SERVICE_TYPE_ID = (long)OracleUdt.GetValue(con, pUdt, "SERVICE_TYPE_ID");
            SERVICE_UNIT_ID = (long)OracleUdt.GetValue(con, pUdt, "SERVICE_UNIT_ID");
            PARENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "PARENT_ID");
            IS_LEAF = (short?)OracleUdt.GetValue(con, pUdt, "IS_LEAF");
            NUM_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "NUM_ORDER");
            HEIN_SERVICE_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "HEIN_SERVICE_TYPE_ID");
            HEIN_SERVICE_BHYT_CODE = (string)OracleUdt.GetValue(con, pUdt, "HEIN_SERVICE_BHYT_CODE");
            HEIN_SERVICE_BHYT_NAME = (string)OracleUdt.GetValue(con, pUdt, "HEIN_SERVICE_BHYT_NAME");
            HEIN_ORDER = (string)OracleUdt.GetValue(con, pUdt, "HEIN_ORDER");
            IS_USE_SERVICE_HEIN = (short?)OracleUdt.GetValue(con, pUdt, "IS_USE_SERVICE_HEIN");
            HEIN_LIMIT_PRICE_OLD = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_PRICE_OLD");
            HEIN_LIMIT_RATIO_OLD = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_RATIO_OLD");
            HEIN_LIMIT_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_PRICE");
            HEIN_LIMIT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_RATIO");
            HEIN_LIMIT_PRICE_IN_TIME = (long?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_PRICE_IN_TIME");
            HEIN_LIMIT_PRICE_INTR_TIME = (long?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_PRICE_INTR_TIME");
            SPECIALITY_CODE = (string)OracleUdt.GetValue(con, pUdt, "SPECIALITY_CODE");
            IS_MULTI_REQUEST = (short?)OracleUdt.GetValue(con, pUdt, "IS_MULTI_REQUEST");
            MAX_EXPEND = (decimal?)OracleUdt.GetValue(con, pUdt, "MAX_EXPEND");
            BILL_OPTION = (short?)OracleUdt.GetValue(con, pUdt, "BILL_OPTION");
            BILL_PATIENT_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "BILL_PATIENT_TYPE_ID");
            IS_OUT_PARENT_FEE = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_PARENT_FEE");
            PTTT_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "PTTT_GROUP_ID");
            PTTT_METHOD_ID = (long?)OracleUdt.GetValue(con, pUdt, "PTTT_METHOD_ID");
            ICD_CM_ID = (long?)OracleUdt.GetValue(con, pUdt, "ICD_CM_ID");
            COGS = (decimal?)OracleUdt.GetValue(con, pUdt, "COGS");
            ESTIMATE_DURATION = (decimal?)OracleUdt.GetValue(con, pUdt, "ESTIMATE_DURATION");
            REVENUE_DEPARTMENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "REVENUE_DEPARTMENT_ID");
            PACKAGE_ID = (long?)OracleUdt.GetValue(con, pUdt, "PACKAGE_ID");
            PACKAGE_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "PACKAGE_PRICE");
            NUMBER_OF_FILM = (long?)OracleUdt.GetValue(con, pUdt, "NUMBER_OF_FILM");
            PACS_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "PACS_TYPE_CODE");
            MIN_DURATION = (long?)OracleUdt.GetValue(con, pUdt, "MIN_DURATION");
            EXE_SERVICE_MODULE_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXE_SERVICE_MODULE_ID");
            GENDER_ID = (long?)OracleUdt.GetValue(con, pUdt, "GENDER_ID");
            IS_ALLOW_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_ALLOW_EXPEND");
            AGE_FROM = (long?)OracleUdt.GetValue(con, pUdt, "AGE_FROM");
            AGE_TO = (long?)OracleUdt.GetValue(con, pUdt, "AGE_TO");
            RATION_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "RATION_GROUP_ID");
            RATION_SYMBOL = (long?)OracleUdt.GetValue(con, pUdt, "RATION_SYMBOL");
            IS_NO_HEIN_LIMIT_FOR_SPECIAL = (short?)OracleUdt.GetValue(con, pUdt, "IS_NO_HEIN_LIMIT_FOR_SPECIAL");
            IS_KIDNEY = (short?)OracleUdt.GetValue(con, pUdt, "IS_KIDNEY");
            NOTICE = (string)OracleUdt.GetValue(con, pUdt, "NOTICE");
            IS_SPECIFIC_HEIN_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_SPECIFIC_HEIN_PRICE");
            IS_OTHER_SOURCE_PAID = (short?)OracleUdt.GetValue(con, pUdt, "IS_OTHER_SOURCE_PAID");
            CAPACITY = (long?)OracleUdt.GetValue(con, pUdt, "CAPACITY");
            IS_ANTIBIOTIC_RESISTANCE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ANTIBIOTIC_RESISTANCE");
            DIIM_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "DIIM_TYPE_ID");
            FUEX_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "FUEX_TYPE_ID");
            TAX_RATE_TYPE = (long?)OracleUdt.GetValue(con, pUdt, "TAX_RATE_TYPE");
            DESCRIPTION = (string)OracleUdt.GetValue(con, pUdt, "DESCRIPTION");
            IS_SPLIT_SERVICE_REQ = (short?)OracleUdt.GetValue(con, pUdt, "IS_SPLIT_SERVICE_REQ");
            IS_ENABLE_ASSIGN_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ENABLE_ASSIGN_PRICE");
            IS_AUTO_FINISH = (short?)OracleUdt.GetValue(con, pUdt, "IS_AUTO_FINISH");
            PROCESS_CODE = (string)OracleUdt.GetValue(con, pUdt, "PROCESS_CODE");
            TEST_TYPE_ID = (short?)OracleUdt.GetValue(con, pUdt, "TEST_TYPE_ID");
            IS_OUT_OF_DRG = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_OF_DRG");
            IS_CONDITIONED = (short?)OracleUdt.GetValue(con, pUdt, "IS_CONDITIONED");
            MIN_PROCESS_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MIN_PROCESS_TIME");
            IS_NOT_CHANGE_BILL_PATY = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_CHANGE_BILL_PATY");
            IS_SPLIT_SERVICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_SPLIT_SERVICE");
            OTHER_PAY_SOURCE_ID = (long?)OracleUdt.GetValue(con, pUdt, "OTHER_PAY_SOURCE_ID");
            OTHER_PAY_SOURCE_ICDS = (string)OracleUdt.GetValue(con, pUdt, "OTHER_PAY_SOURCE_ICDS");
            BODY_PART_IDS = (string)OracleUdt.GetValue(con, pUdt, "BODY_PART_IDS");
            TESTING_TECHNIQUE = (string)OracleUdt.GetValue(con, pUdt, "TESTING_TECHNIQUE");
            IS_OUT_OF_MANAGEMENT = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_OF_MANAGEMENT");
            MUST_BE_CONSULTED = (short?)OracleUdt.GetValue(con, pUdt, "MUST_BE_CONSULTED");
            SUIM_INDEX_ID = (long?)OracleUdt.GetValue(con, pUdt, "SUIM_INDEX_ID");
            ATTACH_ASSIGN_PRINT_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "ATTACH_ASSIGN_PRINT_TYPE_CODE");
            TEST_COVID_TYPE = (long?)OracleUdt.GetValue(con, pUdt, "TEST_COVID_TYPE");
            MAX_PROCESS_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MAX_PROCESS_TIME");
            IS_DISALLOWANCE_NO_EXECUTE = (short?)OracleUdt.GetValue(con, pUdt, "IS_DISALLOWANCE_NO_EXECUTE");
            IS_NOT_SHOW_TRACKING = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_SHOW_TRACKING");
            FILM_SIZE_ID = (long?)OracleUdt.GetValue(con, pUdt, "FILM_SIZE_ID");
            IS_AUTO_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_AUTO_EXPEND");
            IS_BLOCK_DEPARTMENT_TRAN = (short?)OracleUdt.GetValue(con, pUdt, "IS_BLOCK_DEPARTMENT_TRAN");
            WARNING_SAMPLING_TIME = (long?)OracleUdt.GetValue(con, pUdt, "WARNING_SAMPLING_TIME");
            APPLIED_PATIENT_TYPE_IDS = (string)OracleUdt.GetValue(con, pUdt, "APPLIED_PATIENT_TYPE_IDS");
            DEFAULT_PATIENT_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "DEFAULT_PATIENT_TYPE_ID");
            MAX_TOTAL_PROCESS_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MAX_TOTAL_PROCESS_TIME");
            ALLOW_SIMULTANEITY = (long?)OracleUdt.GetValue(con, pUdt, "ALLOW_SIMULTANEITY");
            SAMPLE_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "SAMPLE_TYPE_CODE");
            MIN_PROC_TIME_EXCEPT_PATY_IDS = (string)OracleUdt.GetValue(con, pUdt, "MIN_PROC_TIME_EXCEPT_PATY_IDS");
            MAX_PROC_TIME_EXCEPT_PATY_IDS = (string)OracleUdt.GetValue(con, pUdt, "MAX_PROC_TIME_EXCEPT_PATY_IDS");
            MAX_AMOUNT = (long?)OracleUdt.GetValue(con, pUdt, "MAX_AMOUNT");
            ALLOW_SEND_PACS = (short?)OracleUdt.GetValue(con, pUdt, "ALLOW_SEND_PACS");
            DO_NOT_USE_BHYT = (short?)OracleUdt.GetValue(con, pUdt, "DO_NOT_USE_BHYT");
            IS_NOT_REQUIRED_COMPLETE = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_REQUIRED_COMPLETE");
            PETROLEUM_CODE = (string)OracleUdt.GetValue(con, pUdt, "PETROLEUM_CODE");
            PETROLEUM_NAME = (string)OracleUdt.GetValue(con, pUdt, "PETROLEUM_NAME");
        }
    }
}
