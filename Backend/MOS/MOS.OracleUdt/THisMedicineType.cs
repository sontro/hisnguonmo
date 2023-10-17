using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisMedicineType : INullable, IOracleCustomType
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
        [OracleObjectMappingAttribute("MEDICINE_TYPE_CODE")]
        public string MEDICINE_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_TYPE_NAME")]
        public string MEDICINE_TYPE_NAME { get; set; }
        [OracleObjectMappingAttribute("SERVICE_ID")]
        public long SERVICE_ID { get; set; }
        [OracleObjectMappingAttribute("PARENT_ID")]
        public Nullable<long> PARENT_ID { get; set; }
        [OracleObjectMappingAttribute("IS_LEAF")]
        public Nullable<short> IS_LEAF { get; set; }
        [OracleObjectMappingAttribute("NUM_ORDER")]
        public Nullable<long> NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("CONCENTRA")]
        public string CONCENTRA { get; set; }
        [OracleObjectMappingAttribute("ACTIVE_INGR_BHYT_CODE")]
        public string ACTIVE_INGR_BHYT_CODE { get; set; }
        [OracleObjectMappingAttribute("ACTIVE_INGR_BHYT_NAME")]
        public string ACTIVE_INGR_BHYT_NAME { get; set; }
        [OracleObjectMappingAttribute("REGISTER_NUMBER")]
        public string REGISTER_NUMBER { get; set; }
        [OracleObjectMappingAttribute("PACKING_TYPE_ID__DELETE")]
        public Nullable<long> PACKING_TYPE_ID__DELETE { get; set; }
        [OracleObjectMappingAttribute("MANUFACTURER_ID")]
        public Nullable<long> MANUFACTURER_ID { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_USE_FORM_ID")]
        public Nullable<long> MEDICINE_USE_FORM_ID { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_LINE_ID")]
        public Nullable<long> MEDICINE_LINE_ID { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_GROUP_ID")]
        public Nullable<long> MEDICINE_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_UNIT_ID")]
        public long TDL_SERVICE_UNIT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_GENDER_ID")]
        public Nullable<long> TDL_GENDER_ID { get; set; }
        [OracleObjectMappingAttribute("NATIONAL_NAME")]
        public string NATIONAL_NAME { get; set; }
        [OracleObjectMappingAttribute("TUTORIAL")]
        public string TUTORIAL { get; set; }
        [OracleObjectMappingAttribute("IMP_PRICE")]
        public Nullable<decimal> IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("IMP_VAT_RATIO")]
        public Nullable<decimal> IMP_VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("INTERNAL_PRICE")]
        public Nullable<decimal> INTERNAL_PRICE { get; set; }
        [OracleObjectMappingAttribute("ALERT_MAX_IN_TREATMENT")]
        public Nullable<decimal> ALERT_MAX_IN_TREATMENT { get; set; }
        [OracleObjectMappingAttribute("ALERT_EXPIRED_DATE")]
        public Nullable<long> ALERT_EXPIRED_DATE { get; set; }
        [OracleObjectMappingAttribute("ALERT_MIN_IN_STOCK")]
        public Nullable<decimal> ALERT_MIN_IN_STOCK { get; set; }
        [OracleObjectMappingAttribute("ALERT_MAX_IN_PRESCRIPTION")]
        public Nullable<decimal> ALERT_MAX_IN_PRESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("IS_STOP_IMP")]
        public Nullable<short> IS_STOP_IMP { get; set; }
        [OracleObjectMappingAttribute("IS_STAR_MARK")]
        public Nullable<short> IS_STAR_MARK { get; set; }
        [OracleObjectMappingAttribute("IS_ALLOW_ODD")]
        public Nullable<short> IS_ALLOW_ODD { get; set; }
        [OracleObjectMappingAttribute("IS_ALLOW_EXPORT_ODD")]
        public Nullable<short> IS_ALLOW_EXPORT_ODD { get; set; }
        [OracleObjectMappingAttribute("IS_FUNCTIONAL_FOOD")]
        public Nullable<short> IS_FUNCTIONAL_FOOD { get; set; }
        [OracleObjectMappingAttribute("IS_REQUIRE_HSD")]
        public Nullable<short> IS_REQUIRE_HSD { get; set; }
        [OracleObjectMappingAttribute("IS_SALE_EQUAL_IMP_PRICE")]
        public Nullable<short> IS_SALE_EQUAL_IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_BUSINESS")]
        public Nullable<short> IS_BUSINESS { get; set; }
        [OracleObjectMappingAttribute("IS_RAW_MEDICINE")]
        public Nullable<short> IS_RAW_MEDICINE { get; set; }
        [OracleObjectMappingAttribute("IS_AUTO_EXPEND")]
        public Nullable<short> IS_AUTO_EXPEND { get; set; }
        [OracleObjectMappingAttribute("IS_VITAMIN_A")]
        public Nullable<short> IS_VITAMIN_A { get; set; }
        [OracleObjectMappingAttribute("IS_VACCINE")]
        public Nullable<short> IS_VACCINE { get; set; }
        [OracleObjectMappingAttribute("IS_TCMR")]
        public Nullable<short> IS_TCMR { get; set; }
        [OracleObjectMappingAttribute("IS_MUST_PREPARE")]
        public Nullable<short> IS_MUST_PREPARE { get; set; }
        [OracleObjectMappingAttribute("USE_ON_DAY")]
        public Nullable<decimal> USE_ON_DAY { get; set; }
        [OracleObjectMappingAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("MEMA_GROUP_ID")]
        public Nullable<long> MEMA_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("BYT_NUM_ORDER")]
        public string BYT_NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("TCY_NUM_ORDER")]
        public string TCY_NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_TYPE_PROPRIETARY_NAME")]
        public string MEDICINE_TYPE_PROPRIETARY_NAME { get; set; }
        [OracleObjectMappingAttribute("PACKING_TYPE_NAME")]
        public string PACKING_TYPE_NAME { get; set; }
        [OracleObjectMappingAttribute("RANK")]
        public Nullable<long> RANK { get; set; }
        [OracleObjectMappingAttribute("MEDICINE_NATIONAL_CODE")]
        public string MEDICINE_NATIONAL_CODE { get; set; }
        [OracleObjectMappingAttribute("IS_KIDNEY")]
        public Nullable<short> IS_KIDNEY { get; set; }
        [OracleObjectMappingAttribute("IS_CHEMICAL_SUBSTANCE")]
        public Nullable<short> IS_CHEMICAL_SUBSTANCE { get; set; }
        [OracleObjectMappingAttribute("RECORDING_TRANSACTION")]
        public string RECORDING_TRANSACTION { get; set; }
        [OracleObjectMappingAttribute("SOURCE_MEDICINE")]
        public short? SOURCE_MEDICINE { get; set; }
        [OracleObjectMappingAttribute("QUALITY_STANDARDS")]
        public string QUALITY_STANDARDS { get; set; }
        [OracleObjectMappingAttribute("PREPROCESSING")]
        public string PREPROCESSING { get; set; }
        [OracleObjectMappingAttribute("PROCESSING")]
        public string PROCESSING { get; set; }
        [OracleObjectMappingAttribute("USED_PART")]
        public string USED_PART { get; set; }
        [OracleObjectMappingAttribute("CONTRAINDICATION")]
        public string CONTRAINDICATION { get; set; }
        [OracleObjectMappingAttribute("DISTRIBUTED_AMOUNT")]
        public string DISTRIBUTED_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("DOSAGE_FORM")]
        public string DOSAGE_FORM { get; set; }
        [OracleObjectMappingAttribute("LAST_EXP_PRICE")]
        public decimal? LAST_EXP_PRICE { get; set; }
        [OracleObjectMappingAttribute("LAST_EXP_VAT_RATIO")]
        public decimal? LAST_EXP_VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("LAST_IMP_PRICE")]
        public decimal? LAST_IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("LAST_IMP_VAT_RATIO")]
        public decimal? LAST_IMP_VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("ATC_CODES")]
        public string ATC_CODES { get; set; }
        [OracleObjectMappingAttribute("LAST_EXPIRED_DATE")]
        public long? LAST_EXPIRED_DATE { get; set; }
        [OracleObjectMappingAttribute("IS_TREATMENT_DAY_COUNT")]
        public short? IS_TREATMENT_DAY_COUNT { get; set; }
        [OracleObjectMappingAttribute("ALLOW_MISSING_PKG_INFO")]
        public short? ALLOW_MISSING_PKG_INFO { get; set; }
        [OracleObjectMappingAttribute("IS_BLOCK_MAX_IN_PRESCRIPTION")]
        public short? IS_BLOCK_MAX_IN_PRESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("IS_OXYGEN")]
        public short? IS_OXYGEN { get; set; }
        [OracleObjectMappingAttribute("IS_SPLIT_COMPENSATION")]
        public short? IS_SPLIT_COMPENSATION { get; set; }
        [OracleObjectMappingAttribute("STORAGE_CONDITION_ID")]
        public long? STORAGE_CONDITION_ID { get; set; }
        [OracleObjectMappingAttribute("CONTRAINDICATION_IDS")]
        public string CONTRAINDICATION_IDS { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_HOSPITAL")]
        public short? IS_OUT_HOSPITAL { get; set; }
        [OracleObjectMappingAttribute("IMP_UNIT_ID")]
        public long? IMP_UNIT_ID { get; set; }
        [OracleObjectMappingAttribute("IMP_UNIT_CONVERT_RATIO")]
        public decimal? IMP_UNIT_CONVERT_RATIO { get; set; }
        [OracleObjectMappingAttribute("SCIENTIFIC_NAME")]
        public string SCIENTIFIC_NAME { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_TREATMENT_DAY_COUNT")]
        public short? IS_NOT_TREATMENT_DAY_COUNT { get; set; }
        [OracleObjectMappingAttribute("IS_ANAESTHESIA")]
        public short? IS_ANAESTHESIA { get; set; }
        [OracleObjectMappingAttribute("VACCINE_TYPE_ID")]
        public long? VACCINE_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("NUM_ORDER_CIRCULARS20")]
        public string NUM_ORDER_CIRCULARS20 { get; set; }
        [OracleObjectMappingAttribute("PREPROCESSING_CODE")]
        public string PREPROCESSING_CODE { get; set; }
        [OracleObjectMappingAttribute("PROCESSING_CODE")]
        public string PROCESSING_CODE { get; set; }
        [OracleObjectMappingAttribute("ALERT_MAX_IN_DAY")]
        public decimal? ALERT_MAX_IN_DAY { get; set; }
        [OracleObjectMappingAttribute("IS_BLOCK_MAX_IN_DAY")]
        public short? IS_BLOCK_MAX_IN_DAY { get; set; }
        [OracleObjectMappingAttribute("HTU_ID")]
        public Nullable<long> HTU_ID { get; set; }

        public static THisMedicineType Null
        {
            get
            {
                THisMedicineType company = new THisMedicineType();
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
            OracleUdt.SetValue(con, pUdt, "MEDICINE_TYPE_CODE", MEDICINE_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_TYPE_NAME", MEDICINE_TYPE_NAME);
            OracleUdt.SetValue(con, pUdt, "SERVICE_ID", SERVICE_ID);
            OracleUdt.SetValue(con, pUdt, "PARENT_ID", PARENT_ID);
            OracleUdt.SetValue(con, pUdt, "IS_LEAF", IS_LEAF);
            OracleUdt.SetValue(con, pUdt, "NUM_ORDER", NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "CONCENTRA", CONCENTRA);
            OracleUdt.SetValue(con, pUdt, "ACTIVE_INGR_BHYT_CODE", ACTIVE_INGR_BHYT_CODE);
            OracleUdt.SetValue(con, pUdt, "ACTIVE_INGR_BHYT_NAME", ACTIVE_INGR_BHYT_NAME);
            OracleUdt.SetValue(con, pUdt, "REGISTER_NUMBER", REGISTER_NUMBER);
            OracleUdt.SetValue(con, pUdt, "PACKING_TYPE_ID__DELETE", PACKING_TYPE_ID__DELETE);
            OracleUdt.SetValue(con, pUdt, "MANUFACTURER_ID", MANUFACTURER_ID);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_USE_FORM_ID", MEDICINE_USE_FORM_ID);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_LINE_ID", MEDICINE_LINE_ID);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_GROUP_ID", MEDICINE_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_UNIT_ID", TDL_SERVICE_UNIT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_GENDER_ID", TDL_GENDER_ID);
            OracleUdt.SetValue(con, pUdt, "NATIONAL_NAME", NATIONAL_NAME);
            OracleUdt.SetValue(con, pUdt, "TUTORIAL", TUTORIAL);
            OracleUdt.SetValue(con, pUdt, "IMP_PRICE", IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "IMP_VAT_RATIO", IMP_VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "INTERNAL_PRICE", INTERNAL_PRICE);
            OracleUdt.SetValue(con, pUdt, "ALERT_MAX_IN_TREATMENT", ALERT_MAX_IN_TREATMENT);
            OracleUdt.SetValue(con, pUdt, "ALERT_EXPIRED_DATE", ALERT_EXPIRED_DATE);
            OracleUdt.SetValue(con, pUdt, "ALERT_MIN_IN_STOCK", ALERT_MIN_IN_STOCK);
            OracleUdt.SetValue(con, pUdt, "ALERT_MAX_IN_PRESCRIPTION", ALERT_MAX_IN_PRESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "IS_STOP_IMP", IS_STOP_IMP);
            OracleUdt.SetValue(con, pUdt, "IS_STAR_MARK", IS_STAR_MARK);
            OracleUdt.SetValue(con, pUdt, "IS_ALLOW_ODD", IS_ALLOW_ODD);
            OracleUdt.SetValue(con, pUdt, "IS_ALLOW_EXPORT_ODD", IS_ALLOW_EXPORT_ODD);
            OracleUdt.SetValue(con, pUdt, "IS_FUNCTIONAL_FOOD", IS_FUNCTIONAL_FOOD);
            OracleUdt.SetValue(con, pUdt, "IS_REQUIRE_HSD", IS_REQUIRE_HSD);
            OracleUdt.SetValue(con, pUdt, "IS_SALE_EQUAL_IMP_PRICE", IS_SALE_EQUAL_IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_BUSINESS", IS_BUSINESS);
            OracleUdt.SetValue(con, pUdt, "IS_RAW_MEDICINE", IS_RAW_MEDICINE);
            OracleUdt.SetValue(con, pUdt, "IS_AUTO_EXPEND", IS_AUTO_EXPEND);
            OracleUdt.SetValue(con, pUdt, "IS_VITAMIN_A", IS_VITAMIN_A);
            OracleUdt.SetValue(con, pUdt, "IS_VACCINE", IS_VACCINE);
            OracleUdt.SetValue(con, pUdt, "IS_TCMR", IS_TCMR);
            OracleUdt.SetValue(con, pUdt, "IS_MUST_PREPARE", IS_MUST_PREPARE);
            OracleUdt.SetValue(con, pUdt, "USE_ON_DAY", USE_ON_DAY);
            OracleUdt.SetValue(con, pUdt, "DESCRIPTION", DESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "MEMA_GROUP_ID", MEMA_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "BYT_NUM_ORDER", BYT_NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "TCY_NUM_ORDER", TCY_NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_TYPE_PROPRIETARY_NAME", MEDICINE_TYPE_PROPRIETARY_NAME);
            OracleUdt.SetValue(con, pUdt, "PACKING_TYPE_NAME", PACKING_TYPE_NAME);
            OracleUdt.SetValue(con, pUdt, "RANK", RANK);
            OracleUdt.SetValue(con, pUdt, "MEDICINE_NATIONAL_CODE", MEDICINE_NATIONAL_CODE);
            OracleUdt.SetValue(con, pUdt, "IS_KIDNEY", IS_KIDNEY);
            OracleUdt.SetValue(con, pUdt, "IS_CHEMICAL_SUBSTANCE", IS_CHEMICAL_SUBSTANCE);
            OracleUdt.SetValue(con, pUdt, "RECORDING_TRANSACTION", RECORDING_TRANSACTION);
            OracleUdt.SetValue(con, pUdt, "SOURCE_MEDICINE", SOURCE_MEDICINE);
            OracleUdt.SetValue(con, pUdt, "QUALITY_STANDARDS", QUALITY_STANDARDS);
            OracleUdt.SetValue(con, pUdt, "PREPROCESSING", PREPROCESSING);
            OracleUdt.SetValue(con, pUdt, "PROCESSING", PROCESSING);
            OracleUdt.SetValue(con, pUdt, "USED_PART", USED_PART);
            OracleUdt.SetValue(con, pUdt, "CONTRAINDICATION", CONTRAINDICATION);
            OracleUdt.SetValue(con, pUdt, "DISTRIBUTED_AMOUNT", DISTRIBUTED_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "DOSAGE_FORM", DOSAGE_FORM);
            OracleUdt.SetValue(con, pUdt, "LAST_EXP_PRICE", LAST_EXP_PRICE);
            OracleUdt.SetValue(con, pUdt, "LAST_EXP_VAT_RATIO", LAST_EXP_VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "LAST_IMP_PRICE", LAST_IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "LAST_IMP_VAT_RATIO", LAST_IMP_VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "ATC_CODES", ATC_CODES);
            OracleUdt.SetValue(con, pUdt, "LAST_EXPIRED_DATE", LAST_EXPIRED_DATE);
            OracleUdt.SetValue(con, pUdt, "IS_TREATMENT_DAY_COUNT", IS_TREATMENT_DAY_COUNT);
            OracleUdt.SetValue(con, pUdt, "ALLOW_MISSING_PKG_INFO", ALLOW_MISSING_PKG_INFO);
            OracleUdt.SetValue(con, pUdt, "IS_BLOCK_MAX_IN_PRESCRIPTION", IS_BLOCK_MAX_IN_PRESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "IS_OXYGEN", IS_OXYGEN);
            OracleUdt.SetValue(con, pUdt, "IS_SPLIT_COMPENSATION", IS_SPLIT_COMPENSATION);
            OracleUdt.SetValue(con, pUdt, "STORAGE_CONDITION_ID", STORAGE_CONDITION_ID);
            OracleUdt.SetValue(con, pUdt, "CONTRAINDICATION_IDS", CONTRAINDICATION_IDS);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_HOSPITAL", IS_OUT_HOSPITAL);
            OracleUdt.SetValue(con, pUdt, "IMP_UNIT_ID", IMP_UNIT_ID);
            OracleUdt.SetValue(con, pUdt, "IMP_UNIT_CONVERT_RATIO", IMP_UNIT_CONVERT_RATIO);
            OracleUdt.SetValue(con, pUdt, "SCIENTIFIC_NAME", SCIENTIFIC_NAME);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_TREATMENT_DAY_COUNT", IS_NOT_TREATMENT_DAY_COUNT);
            OracleUdt.SetValue(con, pUdt, "IS_ANAESTHESIA", IS_ANAESTHESIA);
            OracleUdt.SetValue(con, pUdt, "VACCINE_TYPE_ID", VACCINE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "NUM_ORDER_CIRCULARS20", NUM_ORDER_CIRCULARS20);
            OracleUdt.SetValue(con, pUdt, "PREPROCESSING_CODE", PREPROCESSING_CODE);
            OracleUdt.SetValue(con, pUdt, "PROCESSING_CODE", PROCESSING_CODE);
            OracleUdt.SetValue(con, pUdt, "ALERT_MAX_IN_DAY", ALERT_MAX_IN_DAY);
            OracleUdt.SetValue(con, pUdt, "IS_BLOCK_MAX_IN_DAY", IS_BLOCK_MAX_IN_DAY);
            OracleUdt.SetValue(con, pUdt, "HTU_ID", HTU_ID);
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
            MEDICINE_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "MEDICINE_TYPE_CODE");
            MEDICINE_TYPE_NAME = (string)OracleUdt.GetValue(con, pUdt, "MEDICINE_TYPE_NAME");
            SERVICE_ID = (long)OracleUdt.GetValue(con, pUdt, "SERVICE_ID");
            PARENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "PARENT_ID");
            IS_LEAF = (short?)OracleUdt.GetValue(con, pUdt, "IS_LEAF");
            NUM_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "NUM_ORDER");
            CONCENTRA = (string)OracleUdt.GetValue(con, pUdt, "CONCENTRA");
            ACTIVE_INGR_BHYT_CODE = (string)OracleUdt.GetValue(con, pUdt, "ACTIVE_INGR_BHYT_CODE");
            ACTIVE_INGR_BHYT_NAME = (string)OracleUdt.GetValue(con, pUdt, "ACTIVE_INGR_BHYT_NAME");
            REGISTER_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "REGISTER_NUMBER");
            PACKING_TYPE_ID__DELETE = (long?)OracleUdt.GetValue(con, pUdt, "PACKING_TYPE_ID__DELETE");
            MANUFACTURER_ID = (long?)OracleUdt.GetValue(con, pUdt, "MANUFACTURER_ID");
            MEDICINE_USE_FORM_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDICINE_USE_FORM_ID");
            MEDICINE_LINE_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDICINE_LINE_ID");
            MEDICINE_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDICINE_GROUP_ID");
            TDL_SERVICE_UNIT_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_UNIT_ID");
            TDL_GENDER_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_GENDER_ID");
            NATIONAL_NAME = (string)OracleUdt.GetValue(con, pUdt, "NATIONAL_NAME");
            TUTORIAL = (string)OracleUdt.GetValue(con, pUdt, "TUTORIAL");
            IMP_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "IMP_PRICE");
            IMP_VAT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "IMP_VAT_RATIO");
            INTERNAL_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "INTERNAL_PRICE");
            ALERT_MAX_IN_TREATMENT = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MAX_IN_TREATMENT");
            ALERT_EXPIRED_DATE = (long?)OracleUdt.GetValue(con, pUdt, "ALERT_EXPIRED_DATE");
            ALERT_MIN_IN_STOCK = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MIN_IN_STOCK");
            ALERT_MAX_IN_PRESCRIPTION = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MAX_IN_PRESCRIPTION");
            IS_STOP_IMP = (short?)OracleUdt.GetValue(con, pUdt, "IS_STOP_IMP");
            IS_STAR_MARK = (short?)OracleUdt.GetValue(con, pUdt, "IS_STAR_MARK");
            IS_ALLOW_ODD = (short?)OracleUdt.GetValue(con, pUdt, "IS_ALLOW_ODD");
            IS_ALLOW_EXPORT_ODD = (short?)OracleUdt.GetValue(con, pUdt, "IS_ALLOW_EXPORT_ODD");
            IS_FUNCTIONAL_FOOD = (short?)OracleUdt.GetValue(con, pUdt, "IS_FUNCTIONAL_FOOD");
            IS_REQUIRE_HSD = (short?)OracleUdt.GetValue(con, pUdt, "IS_REQUIRE_HSD");
            IS_SALE_EQUAL_IMP_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_SALE_EQUAL_IMP_PRICE");
            IS_BUSINESS = (short?)OracleUdt.GetValue(con, pUdt, "IS_BUSINESS");
            IS_RAW_MEDICINE = (short?)OracleUdt.GetValue(con, pUdt, "IS_RAW_MEDICINE");
            IS_AUTO_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_AUTO_EXPEND");
            IS_VITAMIN_A = (short?)OracleUdt.GetValue(con, pUdt, "IS_VITAMIN_A");
            IS_VACCINE = (short?)OracleUdt.GetValue(con, pUdt, "IS_VACCINE");
            IS_TCMR = (short?)OracleUdt.GetValue(con, pUdt, "IS_TCMR");
            IS_MUST_PREPARE = (short?)OracleUdt.GetValue(con, pUdt, "IS_MUST_PREPARE");
            USE_ON_DAY = (decimal?)OracleUdt.GetValue(con, pUdt, "USE_ON_DAY");
            DESCRIPTION = (string)OracleUdt.GetValue(con, pUdt, "DESCRIPTION");
            MEMA_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEMA_GROUP_ID");
            BYT_NUM_ORDER = (string)OracleUdt.GetValue(con, pUdt, "BYT_NUM_ORDER");
            TCY_NUM_ORDER = (string)OracleUdt.GetValue(con, pUdt, "TCY_NUM_ORDER");
            MEDICINE_TYPE_PROPRIETARY_NAME = (string)OracleUdt.GetValue(con, pUdt, "MEDICINE_TYPE_PROPRIETARY_NAME");
            PACKING_TYPE_NAME = (string)OracleUdt.GetValue(con, pUdt, "PACKING_TYPE_NAME");
            RANK = (long?)OracleUdt.GetValue(con, pUdt, "RANK");
            MEDICINE_NATIONAL_CODE = (string)OracleUdt.GetValue(con, pUdt, "MEDICINE_NATIONAL_CODE");
            IS_KIDNEY = (short?)OracleUdt.GetValue(con, pUdt, "IS_KIDNEY");
            IS_CHEMICAL_SUBSTANCE = (short?)OracleUdt.GetValue(con, pUdt, "IS_CHEMICAL_SUBSTANCE");
            RECORDING_TRANSACTION = (string)OracleUdt.GetValue(con, pUdt, "RECORDING_TRANSACTION");
            SOURCE_MEDICINE = (short?)OracleUdt.GetValue(con, pUdt, "SOURCE_MEDICINE");
            QUALITY_STANDARDS = (string)OracleUdt.GetValue(con, pUdt, "QUALITY_STANDARDS");
            PREPROCESSING = (string)OracleUdt.GetValue(con, pUdt, "PREPROCESSING");
            PROCESSING = (string)OracleUdt.GetValue(con, pUdt, "PROCESSING");
            USED_PART = (string)OracleUdt.GetValue(con, pUdt, "USED_PART");
            CONTRAINDICATION = (string)OracleUdt.GetValue(con, pUdt, "CONTRAINDICATION");
            DISTRIBUTED_AMOUNT = (string)OracleUdt.GetValue(con, pUdt, "DISTRIBUTED_AMOUNT");
            DOSAGE_FORM = (string)OracleUdt.GetValue(con, pUdt, "DOSAGE_FORM");
            LAST_EXP_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "LAST_EXP_PRICE");
            LAST_EXP_VAT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "LAST_EXP_VAT_RATIO");
            LAST_IMP_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "LAST_IMP_PRICE");
            LAST_IMP_VAT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "LAST_IMP_VAT_RATIO");
            ATC_CODES = (string)OracleUdt.GetValue(con, pUdt, "ATC_CODES");
            LAST_EXPIRED_DATE = (long?)OracleUdt.GetValue(con, pUdt, "LAST_EXPIRED_DATE");
            IS_TREATMENT_DAY_COUNT = (short?)OracleUdt.GetValue(con, pUdt, "IS_TREATMENT_DAY_COUNT");
            ALLOW_MISSING_PKG_INFO = (short?)OracleUdt.GetValue(con, pUdt, "ALLOW_MISSING_PKG_INFO");
            IS_BLOCK_MAX_IN_PRESCRIPTION = (short?)OracleUdt.GetValue(con, pUdt, "IS_BLOCK_MAX_IN_PRESCRIPTION");
            IS_OXYGEN = (short?)OracleUdt.GetValue(con, pUdt, "IS_OXYGEN");
            IS_SPLIT_COMPENSATION = (short?)OracleUdt.GetValue(con, pUdt, "IS_SPLIT_COMPENSATION");
            STORAGE_CONDITION_ID = (long?)OracleUdt.GetValue(con, pUdt, "STORAGE_CONDITION_ID");
            CONTRAINDICATION_IDS = (string)OracleUdt.GetValue(con, pUdt, "CONTRAINDICATION_IDS");
            IS_OUT_HOSPITAL = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_HOSPITAL");
            IMP_UNIT_ID = (long?)OracleUdt.GetValue(con, pUdt, "IMP_UNIT_ID");
            IMP_UNIT_CONVERT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "IMP_UNIT_CONVERT_RATIO");
            SCIENTIFIC_NAME = (string)OracleUdt.GetValue(con, pUdt, "SCIENTIFIC_NAME");
            IS_NOT_TREATMENT_DAY_COUNT = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_TREATMENT_DAY_COUNT");
            IS_ANAESTHESIA = (short?)OracleUdt.GetValue(con, pUdt, "IS_ANAESTHESIA");
            VACCINE_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "VACCINE_TYPE_ID");
            NUM_ORDER_CIRCULARS20 = (string)OracleUdt.GetValue(con, pUdt, "NUM_ORDER_CIRCULARS20");
            PREPROCESSING_CODE = (string)OracleUdt.GetValue(con, pUdt, "PREPROCESSING_CODE");
            PROCESSING_CODE = (string)OracleUdt.GetValue(con, pUdt, "PROCESSING_CODE");
            ALERT_MAX_IN_DAY = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MAX_IN_DAY");
            IS_BLOCK_MAX_IN_DAY = (short?)OracleUdt.GetValue(con, pUdt, "IS_BLOCK_MAX_IN_DAY");
            HTU_ID = (long?)OracleUdt.GetValue(con, pUdt, "HTU_ID");
        }
    }
}
