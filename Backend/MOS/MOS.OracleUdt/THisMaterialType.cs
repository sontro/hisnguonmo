using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisMaterialType : INullable, IOracleCustomType
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
        [OracleObjectMappingAttribute("MATERIAL_TYPE_CODE")]
        public string MATERIAL_TYPE_CODE { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_TYPE_NAME")]
        public string MATERIAL_TYPE_NAME { get; set; }
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
        [OracleObjectMappingAttribute("PACKING_TYPE_ID__DELETE")]
        public Nullable<long> PACKING_TYPE_ID__DELETE { get; set; }
        [OracleObjectMappingAttribute("MANUFACTURER_ID")]
        public Nullable<long> MANUFACTURER_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_UNIT_ID")]
        public long TDL_SERVICE_UNIT_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_GENDER_ID")]
        public Nullable<long> TDL_GENDER_ID { get; set; }
        [OracleObjectMappingAttribute("NATIONAL_NAME")]
        public string NATIONAL_NAME { get; set; }
        [OracleObjectMappingAttribute("IMP_PRICE")]
        public Nullable<decimal> IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("IMP_VAT_RATIO")]
        public Nullable<decimal> IMP_VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("INTERNAL_PRICE")]
        public Nullable<decimal> INTERNAL_PRICE { get; set; }
        [OracleObjectMappingAttribute("ALERT_EXPIRED_DATE")]
        public Nullable<long> ALERT_EXPIRED_DATE { get; set; }
        [OracleObjectMappingAttribute("ALERT_MIN_IN_STOCK")]
        public Nullable<decimal> ALERT_MIN_IN_STOCK { get; set; }
        [OracleObjectMappingAttribute("ALERT_MAX_IN_PRESCRIPTION")]
        public Nullable<decimal> ALERT_MAX_IN_PRESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("IS_CHEMICAL_SUBSTANCE")]
        public Nullable<short> IS_CHEMICAL_SUBSTANCE { get; set; }
        [OracleObjectMappingAttribute("IS_AUTO_EXPEND")]
        public Nullable<short> IS_AUTO_EXPEND { get; set; }
        [OracleObjectMappingAttribute("IS_STENT")]
        public Nullable<short> IS_STENT { get; set; }
        [OracleObjectMappingAttribute("IS_IN_KTC_FEE")]
        public Nullable<short> IS_IN_KTC_FEE { get; set; }
        [OracleObjectMappingAttribute("IS_ALLOW_ODD")]
        public Nullable<short> IS_ALLOW_ODD { get; set; }
        [OracleObjectMappingAttribute("IS_ALLOW_EXPORT_ODD")]
        public Nullable<short> IS_ALLOW_EXPORT_ODD { get; set; }
        [OracleObjectMappingAttribute("IS_STOP_IMP")]
        public Nullable<short> IS_STOP_IMP { get; set; }
        [OracleObjectMappingAttribute("IS_REQUIRE_HSD")]
        public Nullable<short> IS_REQUIRE_HSD { get; set; }
        [OracleObjectMappingAttribute("IS_SALE_EQUAL_IMP_PRICE")]
        public Nullable<short> IS_SALE_EQUAL_IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("IS_BUSINESS")]
        public Nullable<short> IS_BUSINESS { get; set; }
        [OracleObjectMappingAttribute("IS_RAW_MATERIAL")]
        public Nullable<short> IS_RAW_MATERIAL { get; set; }
        [OracleObjectMappingAttribute("IS_MUST_PREPARE")]
        public Nullable<short> IS_MUST_PREPARE { get; set; }
        [OracleObjectMappingAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("MEMA_GROUP_ID")]
        public Nullable<long> MEMA_GROUP_ID { get; set; }
        [OracleObjectMappingAttribute("PACKING_TYPE_NAME")]
        public string PACKING_TYPE_NAME { get; set; }
        [OracleObjectMappingAttribute("IS_REUSABLE")]
        public Nullable<short> IS_REUSABLE { get; set; }
        [OracleObjectMappingAttribute("MAX_REUSE_COUNT")]
        public Nullable<long> MAX_REUSE_COUNT { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_GROUP_BHYT")]
        public string MATERIAL_GROUP_BHYT { get; set; }
        [OracleObjectMappingAttribute("RECORDING_TRANSACTION")]
        public string RECORDING_TRANSACTION { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_SHOW_TRACKING")]
        public Nullable<short> IS_NOT_SHOW_TRACKING { get; set; }
        [OracleObjectMappingAttribute("ALERT_MAX_IN_DAY")]
        public Nullable<decimal> ALERT_MAX_IN_DAY { get; set; }
        
        public static THisMaterialType Null
        {
            get
            {
                THisMaterialType company = new THisMaterialType();
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
            OracleUdt.SetValue(con, pUdt, "MATERIAL_TYPE_CODE", MATERIAL_TYPE_CODE);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_TYPE_NAME", MATERIAL_TYPE_NAME);
            OracleUdt.SetValue(con, pUdt, "SERVICE_ID", SERVICE_ID);
            OracleUdt.SetValue(con, pUdt, "PARENT_ID", PARENT_ID);
            OracleUdt.SetValue(con, pUdt, "IS_LEAF", IS_LEAF);
            OracleUdt.SetValue(con, pUdt, "NUM_ORDER", NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "CONCENTRA", CONCENTRA);            
            OracleUdt.SetValue(con, pUdt, "PACKING_TYPE_ID__DELETE", PACKING_TYPE_ID__DELETE);
            OracleUdt.SetValue(con, pUdt, "MANUFACTURER_ID", MANUFACTURER_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_UNIT_ID", TDL_SERVICE_UNIT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_GENDER_ID", TDL_GENDER_ID);
            OracleUdt.SetValue(con, pUdt, "NATIONAL_NAME", NATIONAL_NAME);
            OracleUdt.SetValue(con, pUdt, "IMP_PRICE", IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "IMP_VAT_RATIO", IMP_VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "INTERNAL_PRICE", INTERNAL_PRICE);
            OracleUdt.SetValue(con, pUdt, "ALERT_EXPIRED_DATE", ALERT_EXPIRED_DATE);
            OracleUdt.SetValue(con, pUdt, "ALERT_MIN_IN_STOCK", ALERT_MIN_IN_STOCK);
            OracleUdt.SetValue(con, pUdt, "ALERT_MAX_IN_PRESCRIPTION", ALERT_MAX_IN_PRESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "IS_CHEMICAL_SUBSTANCE", IS_CHEMICAL_SUBSTANCE);
            OracleUdt.SetValue(con, pUdt, "IS_AUTO_EXPEND", IS_AUTO_EXPEND);
            OracleUdt.SetValue(con, pUdt, "IS_STENT", IS_STENT);
            OracleUdt.SetValue(con, pUdt, "IS_IN_KTC_FEE", IS_IN_KTC_FEE);
            OracleUdt.SetValue(con, pUdt, "IS_ALLOW_ODD", IS_ALLOW_ODD);
            OracleUdt.SetValue(con, pUdt, "IS_ALLOW_EXPORT_ODD", IS_ALLOW_EXPORT_ODD);
            OracleUdt.SetValue(con, pUdt, "IS_STOP_IMP", IS_STOP_IMP);
            OracleUdt.SetValue(con, pUdt, "IS_REQUIRE_HSD", IS_REQUIRE_HSD);
            OracleUdt.SetValue(con, pUdt, "IS_SALE_EQUAL_IMP_PRICE", IS_SALE_EQUAL_IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "IS_BUSINESS", IS_BUSINESS);
            OracleUdt.SetValue(con, pUdt, "IS_RAW_MATERIAL", IS_RAW_MATERIAL);
            OracleUdt.SetValue(con, pUdt, "IS_MUST_PREPARE", IS_MUST_PREPARE);
            OracleUdt.SetValue(con, pUdt, "DESCRIPTION", DESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "MEMA_GROUP_ID", MEMA_GROUP_ID);
            OracleUdt.SetValue(con, pUdt, "PACKING_TYPE_NAME", PACKING_TYPE_NAME);
            OracleUdt.SetValue(con, pUdt, "IS_REUSABLE", IS_REUSABLE);
            OracleUdt.SetValue(con, pUdt, "MAX_REUSE_COUNT", MAX_REUSE_COUNT);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_GROUP_BHYT", MATERIAL_GROUP_BHYT);
            OracleUdt.SetValue(con, pUdt, "RECORDING_TRANSACTION", RECORDING_TRANSACTION);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_SHOW_TRACKING", IS_NOT_SHOW_TRACKING);
            OracleUdt.SetValue(con, pUdt, "ALERT_MAX_IN_DAY", ALERT_MAX_IN_DAY);
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
            MATERIAL_TYPE_CODE = (string)OracleUdt.GetValue(con, pUdt, "MATERIAL_TYPE_CODE");
            MATERIAL_TYPE_NAME = (string)OracleUdt.GetValue(con, pUdt, "MATERIAL_TYPE_NAME");
            SERVICE_ID = (long)OracleUdt.GetValue(con, pUdt, "SERVICE_ID");
            PARENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "PARENT_ID");
            IS_LEAF = (short?)OracleUdt.GetValue(con, pUdt, "IS_LEAF");
            NUM_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "NUM_ORDER");
            CONCENTRA = (string)OracleUdt.GetValue(con, pUdt, "CONCENTRA");
            PACKING_TYPE_ID__DELETE = (long?)OracleUdt.GetValue(con, pUdt, "PACKING_TYPE_ID__DELETE");
            MANUFACTURER_ID = (long?)OracleUdt.GetValue(con, pUdt, "MANUFACTURER_ID");            
            TDL_SERVICE_UNIT_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_UNIT_ID");
            TDL_GENDER_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_GENDER_ID");
            NATIONAL_NAME = (string)OracleUdt.GetValue(con, pUdt, "NATIONAL_NAME");
            IMP_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "IMP_PRICE");
            IMP_VAT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "IMP_VAT_RATIO");
            INTERNAL_PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "INTERNAL_PRICE");
            ALERT_EXPIRED_DATE = (long?)OracleUdt.GetValue(con, pUdt, "ALERT_EXPIRED_DATE");
            ALERT_MIN_IN_STOCK = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MIN_IN_STOCK");
            ALERT_MAX_IN_PRESCRIPTION = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MAX_IN_PRESCRIPTION");
            IS_CHEMICAL_SUBSTANCE = (short?)OracleUdt.GetValue(con, pUdt, "IS_CHEMICAL_SUBSTANCE");
            IS_AUTO_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_AUTO_EXPEND");
            IS_STENT = (short?)OracleUdt.GetValue(con, pUdt, "IS_STENT");
            IS_IN_KTC_FEE = (short?)OracleUdt.GetValue(con, pUdt, "IS_IN_KTC_FEE");
            IS_ALLOW_ODD = (short?)OracleUdt.GetValue(con, pUdt, "IS_ALLOW_ODD");
            IS_ALLOW_EXPORT_ODD = (short?)OracleUdt.GetValue(con, pUdt, "IS_ALLOW_EXPORT_ODD");
            IS_STOP_IMP = (short?)OracleUdt.GetValue(con, pUdt, "IS_STOP_IMP");
            IS_REQUIRE_HSD = (short?)OracleUdt.GetValue(con, pUdt, "IS_REQUIRE_HSD");
            IS_SALE_EQUAL_IMP_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_SALE_EQUAL_IMP_PRICE");
            IS_BUSINESS = (short?)OracleUdt.GetValue(con, pUdt, "IS_BUSINESS");
            IS_RAW_MATERIAL = (short?)OracleUdt.GetValue(con, pUdt, "IS_RAW_MATERIAL");
            IS_MUST_PREPARE = (short?)OracleUdt.GetValue(con, pUdt, "IS_MUST_PREPARE");
            DESCRIPTION = (string)OracleUdt.GetValue(con, pUdt, "DESCRIPTION");
            MEMA_GROUP_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEMA_GROUP_ID");
            PACKING_TYPE_NAME = (string)OracleUdt.GetValue(con, pUdt, "PACKING_TYPE_NAME");
            IS_REUSABLE = (short?)OracleUdt.GetValue(con, pUdt, "IS_REUSABLE");
            MAX_REUSE_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "MAX_REUSE_COUNT");
            MATERIAL_GROUP_BHYT = (string)OracleUdt.GetValue(con, pUdt, "MATERIAL_GROUP_BHYT");
            RECORDING_TRANSACTION = (string)OracleUdt.GetValue(con, pUdt, "RECORDING_TRANSACTION");
            IS_NOT_SHOW_TRACKING = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_SHOW_TRACKING");
            ALERT_MAX_IN_DAY = (decimal?)OracleUdt.GetValue(con, pUdt, "ALERT_MAX_IN_DAY");
        }
    }
}
