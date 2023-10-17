using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisSereServ : INullable, IOracleCustomType
    {
        private bool objectIsNull;

        [OracleObjectMappingAttribute("CREATE_TIME")]
        public long? CREATE_TIME { get; set; }
        [OracleObjectMappingAttribute("MODIFY_TIME")]
        public long? MODIFY_TIME { get; set; }
        [OracleObjectMappingAttribute("IS_ACTIVE")]
        public short? IS_ACTIVE { get; set; }
        [OracleObjectMappingAttribute("IS_DELETE")]
        public short? IS_DELETE { get; set; }
        [OracleObjectMappingAttribute("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
        [OracleObjectMappingAttribute("EXECUTE_TIME")]
        public long? EXECUTE_TIME { get; set; }
        [OracleObjectMappingAttribute("TDL_HST_BHYT_CODE")]
        public string TDL_HST_BHYT_CODE { get; set; }
        [OracleObjectMappingAttribute("ID")]
        public long ID { get; set; }
        [OracleObjectMappingAttribute("CREATOR")]
        public string CREATOR { get; set; }
        [OracleObjectMappingAttribute("MODIFIER")]
        public string MODIFIER { get; set; }
        [OracleObjectMappingAttribute("APP_CREATOR")]
        public string APP_CREATOR { get; set; }
        [OracleObjectMappingAttribute("APP_MODIFIER")]
        public string APP_MODIFIER { get; set; }
        [OracleObjectMappingAttribute("SERVICE_ID")]
        public long SERVICE_ID { get; set; }
        [OracleObjectMappingAttribute("SERVICE_REQ_ID")]
        public Nullable<long> SERVICE_REQ_ID { get; set; }
        [OracleObjectMappingAttribute("PATIENT_TYPE_ID")]
        public long PATIENT_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("PARENT_ID")]
        public Nullable<long> PARENT_ID { get; set; }
        [OracleObjectMappingAttribute("HEIN_APPROVAL_ID")]
        public Nullable<long> HEIN_APPROVAL_ID { get; set; }
        [OracleObjectMappingAttribute("JSON_PATIENT_TYPE_ALTER")]
        public string JSON_PATIENT_TYPE_ALTER { get; set; }
        [OracleObjectMappingAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [OracleObjectMappingAttribute("PRICE")]
        public decimal PRICE { get; set; }
        [OracleObjectMappingAttribute("ACTUAL_PRICE")]
        public Nullable<decimal> ACTUAL_PRICE { get; set; }
        [OracleObjectMappingAttribute("ORIGINAL_PRICE")]
        public decimal ORIGINAL_PRICE { get; set; }
        [OracleObjectMappingAttribute("HEIN_PRICE")]
        public Nullable<decimal> HEIN_PRICE { get; set; }
        [OracleObjectMappingAttribute("HEIN_RATIO")]
        public Nullable<decimal> HEIN_RATIO { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_PRICE")]
        public Nullable<decimal> HEIN_LIMIT_PRICE { get; set; }
        [OracleObjectMappingAttribute("HEIN_LIMIT_RATIO")]
        public Nullable<decimal> HEIN_LIMIT_RATIO { get; set; }
        [OracleObjectMappingAttribute("HEIN_NORMAL_PRICE")]
        public Nullable<decimal> HEIN_NORMAL_PRICE { get; set; }
        [OracleObjectMappingAttribute("ADD_PRICE")]
        public Nullable<decimal> ADD_PRICE { get; set; }
        [OracleObjectMappingAttribute("OVERTIME_PRICE")]
        public Nullable<decimal> OVERTIME_PRICE { get; set; }
        [OracleObjectMappingAttribute("DISCOUNT")]
        public Nullable<decimal> DISCOUNT { get; set; }
        [OracleObjectMappingAttribute("VAT_RATIO")]
        public decimal VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("SHARE_COUNT")]
        public Nullable<long> SHARE_COUNT { get; set; }
        [OracleObjectMappingAttribute("STENT_ORDER")]
        public Nullable<long> STENT_ORDER { get; set; }
        [OracleObjectMappingAttribute("IS_EXPEND")]
        public Nullable<short> IS_EXPEND { get; set; }
        [OracleObjectMappingAttribute("IS_NO_PAY")]
        public Nullable<short> IS_NO_PAY { get; set; }
        [OracleObjectMappingAttribute("IS_NO_EXECUTE")]
        public Nullable<short> IS_NO_EXECUTE { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_PARENT_FEE")]
        public Nullable<short> IS_OUT_PARENT_FEE { get; set; }
        [OracleObjectMappingAttribute("IS_NO_HEIN_DIFFERENCE")]
        public Nullable<short> IS_NO_HEIN_DIFFERENCE { get; set; }
        [OracleObjectMappingAttribute("IS_SPECIMEN")]
        public Nullable<short> IS_SPECIMEN { get; set; }
        [OracleObjectMappingAttribute("IS_ADDITION")]
        public Nullable<short> IS_ADDITION { get; set; }
        [OracleObjectMappingAttribute("HEIN_CARD_NUMBER")]
        public string HEIN_CARD_NUMBER { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_ID")]
        public Nullable<long> MEDICINE_ID { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_ID")]
        public Nullable<long> MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("BLOOD_ID")]
        public Nullable<long> BLOOD_ID { get; set; }
        [OracleObjectMappingAttribute("EKIP_ID")]
        public Nullable<long> EKIP_ID { get; set; }
        [OracleObjectMappingAttribute("PACKAGE_ID")]
        public Nullable<long> PACKAGE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_INTRUCTION_TIME")]
        public long TDL_INTRUCTION_TIME { get; set; }
        [OracleObjectMappingAttribute("TDL_INTRUCTION_DATE")]
        public long TDL_INTRUCTION_DATE { get; set; }
        [OracleObjectMappingAttribute("TDL_PATIENT_ID")]
        public Nullable<long> TDL_PATIENT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_TREATMENT_ID")]
        public Nullable<long> TDL_TREATMENT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_TREATMENT_CODE")]
        public string TDL_TREATMENT_CODE { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_CODE")]
        public string TDL_SERVICE_CODE { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_NAME")]
        public string TDL_SERVICE_NAME { get; set; }
        [OracleObjectMappingAttribute("TDL_HEIN_SERVICE_BHYT_CODE")]
        public string TDL_HEIN_SERVICE_BHYT_CODE { get; set; }
        [OracleObjectMappingAttribute("TDL_HEIN_SERVICE_BHYT_NAME")]
        public string TDL_HEIN_SERVICE_BHYT_NAME { get; set; }
        [OracleObjectMappingAttribute("TDL_HEIN_ORDER")]
        public string TDL_HEIN_ORDER { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_TYPE_ID")]
        public long TDL_SERVICE_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_UNIT_ID")]
        public long TDL_SERVICE_UNIT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_HEIN_SERVICE_TYPE_ID")]
        public Nullable<long> TDL_HEIN_SERVICE_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_ACTIVE_INGR_BHYT_CODE")]
        public string TDL_ACTIVE_INGR_BHYT_CODE { get; set; }
        [OracleObjectMappingAttribute("TDL_ACTIVE_INGR_BHYT_NAME")]
        public string TDL_ACTIVE_INGR_BHYT_NAME { get; set; }
        [OracleObjectMappingAttribute("TDL_MEDICINE_CONCENTRA")]
        public string TDL_MEDICINE_CONCENTRA { get; set; }
        [OracleObjectMappingAttribute("TDL_MEDICINE_BID_NUM_ORDER")]
        public string TDL_MEDICINE_BID_NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("TDL_MEDICINE_REGISTER_NUMBER")]
        public string TDL_MEDICINE_REGISTER_NUMBER { get; set; }
        [OracleObjectMappingAttribute("TDL_MEDICINE_PACKAGE_NUMBER")]
        public string TDL_MEDICINE_PACKAGE_NUMBER { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_REQ_CODE")]
        public string TDL_SERVICE_REQ_CODE { get; set; }
        [OracleObjectMappingAttribute("TDL_REQUEST_ROOM_ID")]
        public long TDL_REQUEST_ROOM_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_REQUEST_DEPARTMENT_ID")]
        public long TDL_REQUEST_DEPARTMENT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_REQUEST_LOGINNAME")]
        public string TDL_REQUEST_LOGINNAME { get; set; }
        [OracleObjectMappingAttribute("TDL_REQUEST_USERNAME")]
        public string TDL_REQUEST_USERNAME { get; set; }
        [OracleObjectMappingAttribute("TDL_EXECUTE_ROOM_ID")]
        public long TDL_EXECUTE_ROOM_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_EXECUTE_DEPARTMENT_ID")]
        public long TDL_EXECUTE_DEPARTMENT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_EXECUTE_BRANCH_ID")]
        public long TDL_EXECUTE_BRANCH_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_EXECUTE_GROUP_ID")]
        public Nullable<long> TDL_EXECUTE_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SPECIALITY_CODE")]
        public string TDL_SPECIALITY_CODE { get; set; }
        [OracleObjectMappingAttribute("INVOICE_ID")]
        public Nullable<long> INVOICE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_REQ_TYPE_ID")]
        public long TDL_SERVICE_REQ_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_PACS_TYPE_CODE")]
        public string TDL_PACS_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("EQUIPMENT_SET_ID")]
        public Nullable<long> EQUIPMENT_SET_ID { get; set; }
        [OracleObjectMappingAttribute("EQUIPMENT_SET_ORDER")]
        public Nullable<long> EQUIPMENT_SET_ORDER { get; set; }
        [OracleObjectMappingAttribute("TDL_IS_MAIN_EXAM")]
        public Nullable<short> TDL_IS_MAIN_EXAM { get; set; }
        [OracleObjectMappingAttribute("IS_SENT_EXT")]
        public Nullable<short> IS_SENT_EXT { get; set; }
        [OracleObjectMappingAttribute("EXP_MEST_MATERIAL_ID")]
        public Nullable<long> EXP_MEST_MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("EXP_MEST_MEDICINE_ID")]
        public Nullable<long> EXP_MEST_MEDICINE_ID { get; set; }
        [OracleObjectMappingAttribute("LIMIT_PRICE")]
        public Nullable<decimal> LIMIT_PRICE { get; set; }
        [OracleObjectMappingAttribute("PRIMARY_PATIENT_TYPE_ID")]
        public Nullable<long> PRIMARY_PATIENT_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("PRIMARY_PRICE")]
        public Nullable<decimal> PRIMARY_PRICE { get; set; }
        [OracleObjectMappingAttribute("TDL_BILL_OPTION")]
        public Nullable<short> TDL_BILL_OPTION { get; set; }
        [OracleObjectMappingAttribute("EXPEND_TYPE_ID")]
        public Nullable<long> EXPEND_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("IS_USER_PACKAGE_PRICE")]
        public Nullable<short> IS_USER_PACKAGE_PRICE { get; set; }
        [OracleObjectMappingAttribute("PACKAGE_PRICE")]
        public Nullable<decimal> PACKAGE_PRICE { get; set; }
        [OracleObjectMappingAttribute("USER_PRICE")]
        public Nullable<decimal> USER_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_FUND_ACCEPTED")]
        public Nullable<short> IS_FUND_ACCEPTED { get; set; }
        [OracleObjectMappingAttribute("PATIENT_PRICE_BHYT")]
        public Nullable<decimal> PATIENT_PRICE_BHYT { get; set; }
        [OracleObjectMappingAttribute("OTHER_SOURCE_PRICE")]
        public Nullable<decimal> OTHER_SOURCE_PRICE { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_GROUP_BHYT")]
        public string TDL_MATERIAL_GROUP_BHYT { get; set; }
        [OracleObjectMappingAttribute("TDL_IS_SPECIFIC_HEIN_PRICE")]
        public Nullable<short> TDL_IS_SPECIFIC_HEIN_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_OTHER_SOURCE_PAID")]
        public Nullable<short> IS_OTHER_SOURCE_PAID { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_PRES")]
        public Nullable<short> IS_NOT_PRES { get; set; }
        [OracleObjectMappingAttribute("USE_ORIGINAL_UNIT_FOR_PRES")]
        public Nullable<short> USE_ORIGINAL_UNIT_FOR_PRES { get; set; }
        [OracleObjectMappingAttribute("AMOUNT_TEMP")]
        public Nullable<decimal> AMOUNT_TEMP { get; set; }
        [OracleObjectMappingAttribute("OTHER_PAY_SOURCE_ID")]
        public Nullable<long> OTHER_PAY_SOURCE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_TAX_RATE_TYPE")]
        public Nullable<long> TDL_SERVICE_TAX_RATE_TYPE { get; set; }
        [OracleObjectMappingAttribute("CONFIG_HEIN_LIMIT_PRICE")]
        public Nullable<decimal> CONFIG_HEIN_LIMIT_PRICE { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_DESCRIPTION")]
        public string TDL_SERVICE_DESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("SERVICE_CONDITION_ID")]
        public Nullable<long> SERVICE_CONDITION_ID { get; set; }
        [OracleObjectMappingAttribute("IS_ACCEPTING_NO_EXECUTE")]
        public Nullable<short> IS_ACCEPTING_NO_EXECUTE { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_USE_BHYT")]
        public Nullable<short> IS_NOT_USE_BHYT { get; set; }
        [OracleObjectMappingAttribute("TDL_IS_VACCINE")]
        public Nullable<short> TDL_IS_VACCINE { get; set; }
        [OracleObjectMappingAttribute("TDL_RATION_TIME_ID")]
        public Nullable<long> TDL_RATION_TIME_ID { get; set; }
        

        public static THisSereServ Null
        {
            get
            {
                THisSereServ company = new THisSereServ();
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
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_REQ_TYPE_ID", TDL_SERVICE_REQ_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_ADDITION", IS_ADDITION);
            OracleUdt.SetValue(con, pUdt, "HEIN_CARD_NUMBER", HEIN_CARD_NUMBER);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_ID", MEDICINE_ID);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_ID", MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "BLOOD_ID", BLOOD_ID);
            OracleUdt.SetValue(con, pUdt, "EKIP_ID", EKIP_ID);
            OracleUdt.SetValue(con, pUdt, "PACKAGE_ID", PACKAGE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_INTRUCTION_TIME", TDL_INTRUCTION_TIME);
            OracleUdt.SetValue(con, pUdt, "TDL_INTRUCTION_DATE", TDL_INTRUCTION_DATE);
            OracleUdt.SetValue(con, pUdt, "TDL_PATIENT_ID", TDL_PATIENT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_TREATMENT_ID", TDL_TREATMENT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_TREATMENT_CODE", TDL_TREATMENT_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_CODE", TDL_SERVICE_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_NAME", TDL_SERVICE_NAME);
            OracleUdt.SetValue(con, pUdt, "TDL_HEIN_SERVICE_BHYT_CODE", TDL_HEIN_SERVICE_BHYT_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_EXECUTE_GROUP_ID", TDL_EXECUTE_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_SPECIALITY_CODE", TDL_SPECIALITY_CODE);
            OracleUdt.SetValue(con, pUdt, "INVOICE_ID", INVOICE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_HEIN_SERVICE_BHYT_NAME", TDL_HEIN_SERVICE_BHYT_NAME);
            OracleUdt.SetValue(con, pUdt, "TDL_HEIN_ORDER", TDL_HEIN_ORDER);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_TYPE_ID", TDL_SERVICE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_UNIT_ID", TDL_SERVICE_UNIT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_HEIN_SERVICE_TYPE_ID", TDL_HEIN_SERVICE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_ACTIVE_INGR_BHYT_CODE", TDL_ACTIVE_INGR_BHYT_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_ACTIVE_INGR_BHYT_NAME", TDL_ACTIVE_INGR_BHYT_NAME);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDICINE_CONCENTRA", TDL_MEDICINE_CONCENTRA);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDICINE_BID_NUM_ORDER", TDL_MEDICINE_BID_NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDICINE_REGISTER_NUMBER", TDL_MEDICINE_REGISTER_NUMBER);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDICINE_PACKAGE_NUMBER", TDL_MEDICINE_PACKAGE_NUMBER);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_REQ_CODE", TDL_SERVICE_REQ_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_REQUEST_ROOM_ID", TDL_REQUEST_ROOM_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_REQUEST_DEPARTMENT_ID", TDL_REQUEST_DEPARTMENT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_REQUEST_LOGINNAME", TDL_REQUEST_LOGINNAME);
            OracleUdt.SetValue(con, pUdt, "TDL_REQUEST_USERNAME", TDL_REQUEST_USERNAME);
            OracleUdt.SetValue(con, pUdt, "TDL_EXECUTE_ROOM_ID", TDL_EXECUTE_ROOM_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_EXECUTE_DEPARTMENT_ID", TDL_EXECUTE_DEPARTMENT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_EXECUTE_BRANCH_ID", TDL_EXECUTE_BRANCH_ID);
            OracleUdt.SetValue(con, pUdt, "ID", ID);
            OracleUdt.SetValue(con, pUdt, "CREATOR", CREATOR);
            OracleUdt.SetValue(con, pUdt, "MODIFIER", MODIFIER);
            OracleUdt.SetValue(con, pUdt, "APP_CREATOR", APP_CREATOR);
            OracleUdt.SetValue(con, pUdt, "APP_MODIFIER", APP_MODIFIER);
            OracleUdt.SetValue(con, pUdt, "SERVICE_ID", SERVICE_ID);
            OracleUdt.SetValue(con, pUdt, "SERVICE_REQ_ID", SERVICE_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "PATIENT_TYPE_ID", PATIENT_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "PARENT_ID", PARENT_ID);
            OracleUdt.SetValue(con, pUdt, "HEIN_APPROVAL_ID", HEIN_APPROVAL_ID);
            OracleUdt.SetValue(con, pUdt, "JSON_PATIENT_TYPE_ALTER", JSON_PATIENT_TYPE_ALTER);
            OracleUdt.SetValue(con, pUdt, "AMOUNT", AMOUNT);
            OracleUdt.SetValue(con, pUdt, "PRICE", PRICE);
            OracleUdt.SetValue(con, pUdt, "ORIGINAL_PRICE", ORIGINAL_PRICE);
            OracleUdt.SetValue(con, pUdt, "HEIN_PRICE", HEIN_PRICE);
            OracleUdt.SetValue(con, pUdt, "HEIN_RATIO", HEIN_RATIO);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_PRICE", HEIN_LIMIT_PRICE);
            OracleUdt.SetValue(con, pUdt, "HEIN_LIMIT_RATIO", HEIN_LIMIT_RATIO);
            OracleUdt.SetValue(con, pUdt, "HEIN_NORMAL_PRICE", HEIN_NORMAL_PRICE);
            OracleUdt.SetValue(con, pUdt, "ADD_PRICE", ADD_PRICE);
            OracleUdt.SetValue(con, pUdt, "OVERTIME_PRICE", OVERTIME_PRICE);
            OracleUdt.SetValue(con, pUdt, "DISCOUNT", DISCOUNT);
            OracleUdt.SetValue(con, pUdt, "VAT_RATIO", VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "SHARE_COUNT", SHARE_COUNT);
            OracleUdt.SetValue(con, pUdt, "STENT_ORDER", STENT_ORDER);
            OracleUdt.SetValue(con, pUdt, "IS_EXPEND", IS_EXPEND);
            OracleUdt.SetValue(con, pUdt, "IS_NO_PAY", IS_NO_PAY);
            OracleUdt.SetValue(con, pUdt, "IS_NO_EXECUTE", IS_NO_EXECUTE);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_PARENT_FEE", IS_OUT_PARENT_FEE);
            OracleUdt.SetValue(con, pUdt, "IS_NO_HEIN_DIFFERENCE", IS_NO_HEIN_DIFFERENCE);
            OracleUdt.SetValue(con, pUdt, "IS_SPECIMEN", IS_SPECIMEN);
            OracleUdt.SetValue(con, pUdt, "CREATE_TIME", CREATE_TIME);
            OracleUdt.SetValue(con, pUdt, "MODIFY_TIME", MODIFY_TIME);
            OracleUdt.SetValue(con, pUdt, "IS_ACTIVE", IS_ACTIVE);
            OracleUdt.SetValue(con, pUdt, "IS_DELETE", IS_DELETE);
            OracleUdt.SetValue(con, pUdt, "GROUP_CODE", GROUP_CODE);
            OracleUdt.SetValue(con, pUdt, "EXECUTE_TIME", EXECUTE_TIME);
            OracleUdt.SetValue(con, pUdt, "TDL_HST_BHYT_CODE", TDL_HST_BHYT_CODE);
            OracleUdt.SetValue(con, pUdt, "TDL_PACS_TYPE_CODE", TDL_PACS_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "EQUIPMENT_SET_ID", EQUIPMENT_SET_ID);
            OracleUdt.SetValue(con, pUdt, "EQUIPMENT_SET_ORDER", EQUIPMENT_SET_ORDER);
            OracleUdt.SetValue(con, pUdt, "TDL_IS_MAIN_EXAM", TDL_IS_MAIN_EXAM);
            OracleUdt.SetValue(con, pUdt, "IS_SENT_EXT", IS_SENT_EXT);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_MEDICINE_ID", EXP_MEST_MEDICINE_ID);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_MATERIAL_ID", EXP_MEST_MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "LIMIT_PRICE", LIMIT_PRICE);
            OracleUdt.SetValue(con, pUdt, "PRIMARY_PATIENT_TYPE_ID", PRIMARY_PATIENT_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "PRIMARY_PRICE", PRIMARY_PRICE);
            OracleUdt.SetValue(con, pUdt, "TDL_BILL_OPTION", TDL_BILL_OPTION);
            OracleUdt.SetValue(con, pUdt, "EXPEND_TYPE_ID", EXPEND_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_USER_PACKAGE_PRICE", IS_USER_PACKAGE_PRICE);
            OracleUdt.SetValue(con, pUdt, "PACKAGE_PRICE", PACKAGE_PRICE);
            OracleUdt.SetValue(con, pUdt, "USER_PRICE", USER_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_FUND_ACCEPTED", IS_FUND_ACCEPTED);
            OracleUdt.SetValue(con, pUdt, "PATIENT_PRICE_BHYT", PATIENT_PRICE_BHYT);
            OracleUdt.SetValue(con, pUdt, "OTHER_SOURCE_PRICE", OTHER_SOURCE_PRICE);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_GROUP_BHYT", TDL_MATERIAL_GROUP_BHYT);
            OracleUdt.SetValue(con, pUdt, "TDL_IS_SPECIFIC_HEIN_PRICE", TDL_IS_SPECIFIC_HEIN_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_OTHER_SOURCE_PAID", IS_OTHER_SOURCE_PAID);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_PRES", IS_NOT_PRES);
            OracleUdt.SetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES", USE_ORIGINAL_UNIT_FOR_PRES);
            OracleUdt.SetValue(con, pUdt, "AMOUNT_TEMP", AMOUNT_TEMP);
            OracleUdt.SetValue(con, pUdt, "OTHER_PAY_SOURCE_ID", OTHER_PAY_SOURCE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_TAX_RATE_TYPE", TDL_SERVICE_TAX_RATE_TYPE);
            OracleUdt.SetValue(con, pUdt, "CONFIG_HEIN_LIMIT_PRICE", CONFIG_HEIN_LIMIT_PRICE);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_DESCRIPTION", TDL_SERVICE_DESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "SERVICE_CONDITION_ID", SERVICE_CONDITION_ID);
            OracleUdt.SetValue(con, pUdt, "IS_ACCEPTING_NO_EXECUTE", IS_ACCEPTING_NO_EXECUTE);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_USE_BHYT", IS_NOT_USE_BHYT);
            OracleUdt.SetValue(con, pUdt, "TDL_IS_VACCINE", TDL_IS_VACCINE);
            OracleUdt.SetValue(con, pUdt, "TDL_RATION_TIME_ID", TDL_RATION_TIME_ID);
            OracleUdt.SetValue(con, pUdt, "ACTUAL_PRICE", ACTUAL_PRICE);
            
        }

        public void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            TDL_SERVICE_REQ_TYPE_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_REQ_TYPE_ID");
            IS_ADDITION = (short?)OracleUdt.GetValue(con, pUdt, "IS_ADDITION");
            HEIN_CARD_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "HEIN_CARD_NUMBER");
            MEDICINE_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDICINE_ID");
            MATERIAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "MATERIAL_ID");
            BLOOD_ID = (long?)OracleUdt.GetValue(con, pUdt, "BLOOD_ID");
            EKIP_ID = (long?)OracleUdt.GetValue(con, pUdt, "EKIP_ID");
            PACKAGE_ID = (long?)OracleUdt.GetValue(con, pUdt, "PACKAGE_ID");
            TDL_INTRUCTION_TIME = (long)OracleUdt.GetValue(con, pUdt, "TDL_INTRUCTION_TIME");
            TDL_INTRUCTION_DATE = (long)OracleUdt.GetValue(con, pUdt, "TDL_INTRUCTION_DATE");
            TDL_PATIENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_PATIENT_ID");
            TDL_TREATMENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_TREATMENT_ID");
            TDL_TREATMENT_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_TREATMENT_CODE");
            TDL_SERVICE_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_CODE");
            TDL_SERVICE_NAME = (string)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_NAME");
            TDL_HEIN_SERVICE_BHYT_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_HEIN_SERVICE_BHYT_CODE");
            TDL_EXECUTE_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_EXECUTE_GROUP_ID");
            TDL_SPECIALITY_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_SPECIALITY_CODE");
            INVOICE_ID = (long?)OracleUdt.GetValue(con, pUdt, "INVOICE_ID");
            TDL_HEIN_SERVICE_BHYT_NAME = (string)OracleUdt.GetValue(con, pUdt, "TDL_HEIN_SERVICE_BHYT_NAME");
            TDL_HEIN_ORDER = (string)OracleUdt.GetValue(con, pUdt, "TDL_HEIN_ORDER");
            TDL_SERVICE_TYPE_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_TYPE_ID");
            TDL_SERVICE_UNIT_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_UNIT_ID");
            TDL_HEIN_SERVICE_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_HEIN_SERVICE_TYPE_ID");
            TDL_ACTIVE_INGR_BHYT_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_ACTIVE_INGR_BHYT_CODE");
            TDL_ACTIVE_INGR_BHYT_NAME = (string)OracleUdt.GetValue(con, pUdt, "TDL_ACTIVE_INGR_BHYT_NAME");
            TDL_MEDICINE_CONCENTRA = (string)OracleUdt.GetValue(con, pUdt, "TDL_MEDICINE_CONCENTRA");
            TDL_MEDICINE_BID_NUM_ORDER = (string)OracleUdt.GetValue(con, pUdt, "TDL_MEDICINE_BID_NUM_ORDER");
            TDL_MEDICINE_REGISTER_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "TDL_MEDICINE_REGISTER_NUMBER");
            TDL_MEDICINE_PACKAGE_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "TDL_MEDICINE_PACKAGE_NUMBER");
            TDL_SERVICE_REQ_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_REQ_CODE");
            TDL_REQUEST_ROOM_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_REQUEST_ROOM_ID");
            TDL_REQUEST_DEPARTMENT_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_REQUEST_DEPARTMENT_ID");
            TDL_REQUEST_LOGINNAME = (string)OracleUdt.GetValue(con, pUdt, "TDL_REQUEST_LOGINNAME");
            TDL_REQUEST_USERNAME = (string)OracleUdt.GetValue(con, pUdt, "TDL_REQUEST_USERNAME");
            TDL_EXECUTE_ROOM_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_EXECUTE_ROOM_ID");
            TDL_EXECUTE_DEPARTMENT_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_EXECUTE_DEPARTMENT_ID");
            TDL_EXECUTE_BRANCH_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_EXECUTE_BRANCH_ID");
            ID = (long)OracleUdt.GetValue(con, pUdt, "ID");
            CREATOR = (string)OracleUdt.GetValue(con, pUdt, "CREATOR");
            MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "MODIFIER");
            APP_CREATOR = (string)OracleUdt.GetValue(con, pUdt, "APP_CREATOR");
            APP_MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "APP_MODIFIER");
            SERVICE_ID = (long)OracleUdt.GetValue(con, pUdt, "SERVICE_ID");
            SERVICE_REQ_ID = (long?)OracleUdt.GetValue(con, pUdt, "SERVICE_REQ_ID");
            PATIENT_TYPE_ID = (long)OracleUdt.GetValue(con, pUdt, "PATIENT_TYPE_ID");
            PARENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "PARENT_ID");
            HEIN_APPROVAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "HEIN_APPROVAL_ID");
            JSON_PATIENT_TYPE_ALTER = (string)OracleUdt.GetValue(con, pUdt, "JSON_PATIENT_TYPE_ALTER");
            AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "AMOUNT");
            PRICE = (decimal)OracleUdt.GetValue(con, pUdt, "PRICE");
            ORIGINAL_PRICE = (decimal)OracleUdt.GetValue(con, pUdt, "ORIGINAL_PRICE");
            HEIN_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_PRICE");
            HEIN_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_RATIO");
            HEIN_LIMIT_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_PRICE");
            HEIN_LIMIT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_LIMIT_RATIO");
            HEIN_NORMAL_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "HEIN_NORMAL_PRICE");
            ADD_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "ADD_PRICE");
            OVERTIME_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "OVERTIME_PRICE");
            DISCOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "DISCOUNT");
            VAT_RATIO = (decimal)OracleUdt.GetValue(con, pUdt, "VAT_RATIO");
            SHARE_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "SHARE_COUNT");
            STENT_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "STENT_ORDER");
            IS_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_EXPEND");
            IS_NO_PAY = (short?)OracleUdt.GetValue(con, pUdt, "IS_NO_PAY");
            IS_NO_EXECUTE = (short?)OracleUdt.GetValue(con, pUdt, "IS_NO_EXECUTE");
            IS_OUT_PARENT_FEE = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_PARENT_FEE");
            IS_NO_HEIN_DIFFERENCE = (short?)OracleUdt.GetValue(con, pUdt, "IS_NO_HEIN_DIFFERENCE");
            IS_SPECIMEN = (short?)OracleUdt.GetValue(con, pUdt, "IS_SPECIMEN");
            CREATE_TIME = (long?)OracleUdt.GetValue(con, pUdt, "CREATE_TIME");
            MODIFY_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MODIFY_TIME");
            IS_ACTIVE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ACTIVE");
            IS_DELETE = (short?)OracleUdt.GetValue(con, pUdt, "IS_DELETE");
            GROUP_CODE = (string)OracleUdt.GetValue(con, pUdt, "GROUP_CODE");
            EXECUTE_TIME = (long?)OracleUdt.GetValue(con, pUdt, "EXECUTE_TIME");
            TDL_HST_BHYT_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_HST_BHYT_CODE");
            TDL_PACS_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_PACS_TYPE_CODE");
            EQUIPMENT_SET_ID = (long?)OracleUdt.GetValue(con, pUdt, "EQUIPMENT_SET_ID");
            EQUIPMENT_SET_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "EQUIPMENT_SET_ORDER");
            TDL_IS_MAIN_EXAM = (short?)OracleUdt.GetValue(con, pUdt, "TDL_IS_MAIN_EXAM");
            EXP_MEST_MATERIAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_MATERIAL_ID");
            EXP_MEST_MEDICINE_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_MEDICINE_ID");
            IS_SENT_EXT = (short?)OracleUdt.GetValue(con, pUdt, "IS_SENT_EXT");
            LIMIT_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "LIMIT_PRICE");
            PRIMARY_PATIENT_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "PRIMARY_PATIENT_TYPE_ID");
            PRIMARY_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "PRIMARY_PRICE");
            TDL_BILL_OPTION = (short?)OracleUdt.GetValue(con, pUdt, "TDL_BILL_OPTION");
            EXPEND_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXPEND_TYPE_ID");
            PACKAGE_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "PACKAGE_PRICE");
            USER_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "USER_PRICE");
            IS_USER_PACKAGE_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_USER_PACKAGE_PRICE");
            IS_FUND_ACCEPTED = (short?)OracleUdt.GetValue(con, pUdt, "IS_FUND_ACCEPTED");
            PATIENT_PRICE_BHYT = (decimal?)OracleUdt.GetValue(con, pUdt, "PATIENT_PRICE_BHYT");
            OTHER_SOURCE_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "OTHER_SOURCE_PRICE");
            TDL_MATERIAL_GROUP_BHYT = (string)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_GROUP_BHYT");
            TDL_IS_SPECIFIC_HEIN_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "TDL_IS_SPECIFIC_HEIN_PRICE");
            IS_OTHER_SOURCE_PAID = (short?)OracleUdt.GetValue(con, pUdt, "IS_OTHER_SOURCE_PAID");
            IS_NOT_PRES = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_PRES");
            USE_ORIGINAL_UNIT_FOR_PRES = (short?)OracleUdt.GetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES");
            AMOUNT_TEMP = (decimal?)OracleUdt.GetValue(con, pUdt, "AMOUNT_TEMP");
            OTHER_PAY_SOURCE_ID = (long?)OracleUdt.GetValue(con, pUdt, "OTHER_PAY_SOURCE_ID");
            TDL_SERVICE_TAX_RATE_TYPE = (long?)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_TAX_RATE_TYPE");
            CONFIG_HEIN_LIMIT_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "CONFIG_HEIN_LIMIT_PRICE");
            TDL_SERVICE_DESCRIPTION = (string)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_DESCRIPTION");
            SERVICE_CONDITION_ID = (long?)OracleUdt.GetValue(con, pUdt, "SERVICE_CONDITION_ID");
            IS_ACCEPTING_NO_EXECUTE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ACCEPTING_NO_EXECUTE");
            IS_NOT_USE_BHYT = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_USE_BHYT");
            TDL_IS_VACCINE = (short?)OracleUdt.GetValue(con, pUdt, "TDL_IS_VACCINE");
            TDL_RATION_TIME_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_RATION_TIME_ID");
            ACTUAL_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "ACTUAL_PRICE");
        }
    }
}
