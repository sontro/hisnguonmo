using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisMaterialBean : INullable, IOracleCustomType
    {
        private bool objectIsNull;
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
        [OracleObjectMappingAttribute("IS_ACTIVE")]
        public Nullable<short> IS_ACTIVE { get; set; }
        [OracleObjectMappingAttribute("IS_DELETE")]
        public Nullable<short> IS_DELETE { get; set; }
        [OracleObjectMappingAttribute("GROUP_CODE")]
        public string GROUP_CODE { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_ID")]
        public long MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [OracleObjectMappingAttribute("MEDI_STOCK_ID")]
        public Nullable<long> MEDI_STOCK_ID { get; set; }
        [OracleObjectMappingAttribute("SOURCE_ID")]
        public Nullable<long> SOURCE_ID { get; set; }
        [OracleObjectMappingAttribute("EXP_MEST_MATERIAL_ID")]
        public Nullable<long> EXP_MEST_MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("DETACH_AMOUNT")]
        public Nullable<decimal> DETACH_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("DETACH_KEY")]
        public string DETACH_KEY { get; set; }
        [OracleObjectMappingAttribute("IS_TH")]
        public Nullable<short> IS_TH { get; set; }
        [OracleObjectMappingAttribute("IS_CK")]
        public Nullable<short> IS_CK { get; set; }
        [OracleObjectMappingAttribute("IS_USE")]
        public Nullable<short> IS_USE { get; set; }
        [OracleObjectMappingAttribute("SESSION_KEY")]
        public string SESSION_KEY { get; set; }
        [OracleObjectMappingAttribute("SESSION_TIME")]
        public Nullable<long> SESSION_TIME { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_TYPE_ID")]
        public long TDL_MATERIAL_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_IS_ACTIVE")]
        public Nullable<short> TDL_MATERIAL_IS_ACTIVE { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_IMP_PRICE")]
        public decimal TDL_MATERIAL_IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_IMP_VAT_RATIO")]
        public decimal TDL_MATERIAL_IMP_VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_IMP_TIME")]
        public Nullable<long> TDL_MATERIAL_IMP_TIME { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_EXPIRED_DATE")]
        public Nullable<long> TDL_MATERIAL_EXPIRED_DATE { get; set; }
        [OracleObjectMappingAttribute("TDL_IS_SALE_EQUAL_IMP_PRICE")]
        public Nullable<short> TDL_IS_SALE_EQUAL_IMP_PRICE { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_ID")]
        public long TDL_SERVICE_ID { get; set; }
        [OracleObjectMappingAttribute("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
        [OracleObjectMappingAttribute("REMAIN_REUSE_COUNT")]
        public Nullable<long> REMAIN_REUSE_COUNT { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_MAX_REUSE_COUNT")]
        public Nullable<long> TDL_MATERIAL_MAX_REUSE_COUNT { get; set; }
        [OracleObjectMappingAttribute("CREATE_TIME")]
        public long? CREATE_TIME { get; set; }
        [OracleObjectMappingAttribute("MODIFY_TIME")]
        public long? MODIFY_TIME { get; set; }
        [OracleObjectMappingAttribute("TDL_PACKAGE_NUMBER")]
        public string TDL_PACKAGE_NUMBER { get; set; }

        public static THisMaterialBean Null
        {
            get
            {
                THisMaterialBean company = new THisMaterialBean();
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
            OracleUdt.SetValue(con, pUdt, "CREATE_TIME", CREATE_TIME);
            OracleUdt.SetValue(con, pUdt, "MODIFY_TIME", MODIFY_TIME);
            OracleUdt.SetValue(con, pUdt, "ID", ID);
            OracleUdt.SetValue(con, pUdt, "CREATOR", CREATOR);
            OracleUdt.SetValue(con, pUdt, "MODIFIER", MODIFIER);
            OracleUdt.SetValue(con, pUdt, "APP_CREATOR", APP_CREATOR);
            OracleUdt.SetValue(con, pUdt, "APP_MODIFIER", APP_MODIFIER);
            OracleUdt.SetValue(con, pUdt, "IS_ACTIVE", IS_ACTIVE);
            OracleUdt.SetValue(con, pUdt, "IS_DELETE", IS_DELETE);
            OracleUdt.SetValue(con, pUdt, "GROUP_CODE", GROUP_CODE);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_ID", MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "AMOUNT", AMOUNT);
            OracleUdt.SetValue(con, pUdt, "MEDI_STOCK_ID", MEDI_STOCK_ID);
            OracleUdt.SetValue(con, pUdt, "SOURCE_ID", SOURCE_ID);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_MATERIAL_ID", EXP_MEST_MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "DETACH_AMOUNT", DETACH_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "DETACH_KEY", DETACH_KEY);
            OracleUdt.SetValue(con, pUdt, "IS_TH", IS_TH);
            OracleUdt.SetValue(con, pUdt, "IS_CK", IS_CK);
            OracleUdt.SetValue(con, pUdt, "IS_USE", IS_USE);
            OracleUdt.SetValue(con, pUdt, "SESSION_KEY", SESSION_KEY);
            OracleUdt.SetValue(con, pUdt, "SESSION_TIME", SESSION_TIME);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID", TDL_MATERIAL_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_IS_ACTIVE", TDL_MATERIAL_IS_ACTIVE);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_IMP_PRICE", TDL_MATERIAL_IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_IMP_VAT_RATIO", TDL_MATERIAL_IMP_VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_IMP_TIME", TDL_MATERIAL_IMP_TIME);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_EXPIRED_DATE", TDL_MATERIAL_EXPIRED_DATE);
            OracleUdt.SetValue(con, pUdt, "TDL_IS_SALE_EQUAL_IMP_PRICE", TDL_IS_SALE_EQUAL_IMP_PRICE);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_ID", TDL_SERVICE_ID);
            OracleUdt.SetValue(con, pUdt, "SERIAL_NUMBER", SERIAL_NUMBER);
            OracleUdt.SetValue(con, pUdt, "REMAIN_REUSE_COUNT", REMAIN_REUSE_COUNT);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_MAX_REUSE_COUNT", TDL_MATERIAL_MAX_REUSE_COUNT);
            OracleUdt.SetValue(con, pUdt, "TDL_PACKAGE_NUMBER", TDL_PACKAGE_NUMBER);
        }

        public void ToCustomObject(Oracle.DataAccess.Client.OracleConnection con, IntPtr pUdt)
        {
            CREATE_TIME = (long?)OracleUdt.GetValue(con, pUdt, "CREATE_TIME");
            MODIFY_TIME = (long?)OracleUdt.GetValue(con, pUdt, "MODIFY_TIME");
            ID = (long)OracleUdt.GetValue(con, pUdt, "ID");
            CREATOR = (string)OracleUdt.GetValue(con, pUdt, "CREATOR");
            MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "MODIFIER");
            APP_CREATOR = (string)OracleUdt.GetValue(con, pUdt, "APP_CREATOR");
            APP_MODIFIER = (string)OracleUdt.GetValue(con, pUdt, "APP_MODIFIER");
            IS_ACTIVE = (short?)OracleUdt.GetValue(con, pUdt, "IS_ACTIVE");
            IS_DELETE = (short?)OracleUdt.GetValue(con, pUdt, "IS_DELETE");
            GROUP_CODE = (string)OracleUdt.GetValue(con, pUdt, "GROUP_CODE");
            MATERIAL_ID = (long)OracleUdt.GetValue(con, pUdt, "MATERIAL_ID");
            AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "AMOUNT");
            MEDI_STOCK_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDI_STOCK_ID");
            SOURCE_ID = (long?)OracleUdt.GetValue(con, pUdt, "SOURCE_ID");
            EXP_MEST_MATERIAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_MATERIAL_ID");
            DETACH_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "DETACH_AMOUNT");
            DETACH_KEY = (string)OracleUdt.GetValue(con, pUdt, "DETACH_KEY");
            IS_TH = (short?)OracleUdt.GetValue(con, pUdt, "IS_TH");
            IS_CK = (short?)OracleUdt.GetValue(con, pUdt, "IS_CK");
            IS_USE = (short?)OracleUdt.GetValue(con, pUdt, "IS_USE");
            SESSION_KEY = (string)OracleUdt.GetValue(con, pUdt, "SESSION_KEY");
            SESSION_TIME = (long?)OracleUdt.GetValue(con, pUdt, "SESSION_TIME");
            TDL_MATERIAL_TYPE_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID");
            TDL_MATERIAL_IS_ACTIVE = (short?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_IS_ACTIVE");
            TDL_MATERIAL_IMP_PRICE = (decimal)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_IMP_PRICE");
            TDL_MATERIAL_IMP_VAT_RATIO = (decimal)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_IMP_VAT_RATIO");
            TDL_MATERIAL_IMP_TIME = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_IMP_TIME");
            TDL_MATERIAL_EXPIRED_DATE = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_EXPIRED_DATE");
            TDL_IS_SALE_EQUAL_IMP_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "TDL_IS_SALE_EQUAL_IMP_PRICE");
            TDL_SERVICE_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_ID");
            SERIAL_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "SERIAL_NUMBER");
            REMAIN_REUSE_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "REMAIN_REUSE_COUNT");
            TDL_MATERIAL_MAX_REUSE_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_MAX_REUSE_COUNT");
            TDL_PACKAGE_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "TDL_PACKAGE_NUMBER");
        }

    }
}
