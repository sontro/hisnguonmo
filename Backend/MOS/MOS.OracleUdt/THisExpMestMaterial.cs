using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisExpMestMaterial : INullable, IOracleCustomType
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
        [OracleObjectMappingAttribute("EXP_MEST_ID")]
        public Nullable<long> EXP_MEST_ID { get; set; }
        [OracleObjectMappingAttribute("MATERIAL_ID")]
        public Nullable<long> MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_MEDI_STOCK_ID")]
        public Nullable<long> TDL_MEDI_STOCK_ID { get; set; }
        [OracleObjectMappingAttribute("MEDI_STOCK_PERIOD_ID")]
        public Nullable<long> MEDI_STOCK_PERIOD_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_MATERIAL_TYPE_ID")]
        public Nullable<long> TDL_MATERIAL_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("TDL_AGGR_EXP_MEST_ID")]
        public Nullable<long> TDL_AGGR_EXP_MEST_ID { get; set; }
        [OracleObjectMappingAttribute("EXP_MEST_MATY_REQ_ID")]
        public Nullable<long> EXP_MEST_MATY_REQ_ID { get; set; }
        [OracleObjectMappingAttribute("CK_IMP_MEST_MATERIAL_ID")]
        public Nullable<long> CK_IMP_MEST_MATERIAL_ID { get; set; }
        [OracleObjectMappingAttribute("IS_EXPORT")]
        public Nullable<short> IS_EXPORT { get; set; }
        [OracleObjectMappingAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }
        [OracleObjectMappingAttribute("PRICE")]
        public Nullable<decimal> PRICE { get; set; }
        [OracleObjectMappingAttribute("VAT_RATIO")]
        public Nullable<decimal> VAT_RATIO { get; set; }
        [OracleObjectMappingAttribute("DISCOUNT")]
        public Nullable<decimal> DISCOUNT { get; set; }
        [OracleObjectMappingAttribute("NUM_ORDER")]
        public Nullable<long> NUM_ORDER { get; set; }
        [OracleObjectMappingAttribute("DESCRIPTION")]
        public string DESCRIPTION { get; set; }
        [OracleObjectMappingAttribute("APPROVAL_LOGINNAME")]
        public string APPROVAL_LOGINNAME { get; set; }
        [OracleObjectMappingAttribute("APPROVAL_USERNAME")]
        public string APPROVAL_USERNAME { get; set; }
        [OracleObjectMappingAttribute("APPROVAL_TIME")]
        public Nullable<long> APPROVAL_TIME { get; set; }
        [OracleObjectMappingAttribute("APPROVAL_DATE")]
        public Nullable<long> APPROVAL_DATE { get; set; }
        [OracleObjectMappingAttribute("EXP_LOGINNAME")]
        public string EXP_LOGINNAME { get; set; }
        [OracleObjectMappingAttribute("EXP_USERNAME")]
        public string EXP_USERNAME { get; set; }
        [OracleObjectMappingAttribute("EXP_TIME")]
        public Nullable<long> EXP_TIME { get; set; }
        [OracleObjectMappingAttribute("EXP_DATE")]
        public Nullable<long> EXP_DATE { get; set; }
        [OracleObjectMappingAttribute("TH_AMOUNT")]
        public Nullable<decimal> TH_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("PATIENT_TYPE_ID")]
        public Nullable<long> PATIENT_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("SERE_SERV_PARENT_ID")]
        public Nullable<long> SERE_SERV_PARENT_ID { get; set; }
        [OracleObjectMappingAttribute("IS_EXPEND")]
        public Nullable<short> IS_EXPEND { get; set; }
        [OracleObjectMappingAttribute("IS_OUT_PARENT_FEE")]
        public Nullable<short> IS_OUT_PARENT_FEE { get; set; }
        [OracleObjectMappingAttribute("TDL_SERVICE_REQ_ID")]
        public Nullable<long> TDL_SERVICE_REQ_ID { get; set; }
        [OracleObjectMappingAttribute("STENT_ORDER")]
        public Nullable<long> STENT_ORDER { get; set; }
        [OracleObjectMappingAttribute("TDL_TREATMENT_ID")]
        public Nullable<long> TDL_TREATMENT_ID { get; set; }
        [OracleObjectMappingAttribute("EQUIPMENT_SET_ID")]
        public Nullable<long> EQUIPMENT_SET_ID { get; set; }
        [OracleObjectMappingAttribute("EQUIPMENT_SET_ORDER")]
        public Nullable<long> EQUIPMENT_SET_ORDER { get; set; }
        [OracleObjectMappingAttribute("IS_USE_CLIENT_PRICE")]
        public Nullable<short> IS_USE_CLIENT_PRICE { get; set; }
        [OracleObjectMappingAttribute("EXPEND_TYPE_ID")]
        public Nullable<long> EXPEND_TYPE_ID { get; set; }
        [OracleObjectMappingAttribute("SERIAL_NUMBER")]
        public string SERIAL_NUMBER { get; set; }
        [OracleObjectMappingAttribute("REMAIN_REUSE_COUNT")]
        public Nullable<long> REMAIN_REUSE_COUNT { get; set; }
        [OracleObjectMappingAttribute("IS_NOT_PRES")]
        public Nullable<short> IS_NOT_PRES { get; set; }
        [OracleObjectMappingAttribute("USE_ORIGINAL_UNIT_FOR_PRES")]
        public Nullable<short> USE_ORIGINAL_UNIT_FOR_PRES { get; set; }
        [OracleObjectMappingAttribute("BCS_REQ_AMOUNT")]
        public Nullable<decimal> BCS_REQ_AMOUNT { get; set; }
        [OracleObjectMappingAttribute("FAILED_AMOUNT")]
        public Nullable<decimal> FAILED_AMOUNT { get; set; }


        public static THisExpMestMaterial Null
        {
            get
            {
                THisExpMestMaterial company = new THisExpMestMaterial();
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
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_ID", EXP_MEST_ID);
            OracleUdt.SetValue(con, pUdt, "MATERIAL_ID", MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDI_STOCK_ID", TDL_MEDI_STOCK_ID);
            OracleUdt.SetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID", MEDI_STOCK_PERIOD_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID", TDL_MATERIAL_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_AGGR_EXP_MEST_ID", TDL_AGGR_EXP_MEST_ID);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_MATY_REQ_ID", EXP_MEST_MATY_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "CK_IMP_MEST_MATERIAL_ID", CK_IMP_MEST_MATERIAL_ID);
            OracleUdt.SetValue(con, pUdt, "IS_EXPORT", IS_EXPORT);
            OracleUdt.SetValue(con, pUdt, "AMOUNT", AMOUNT);
            OracleUdt.SetValue(con, pUdt, "PRICE", PRICE);
            OracleUdt.SetValue(con, pUdt, "VAT_RATIO", VAT_RATIO);
            OracleUdt.SetValue(con, pUdt, "DISCOUNT", DISCOUNT);
            OracleUdt.SetValue(con, pUdt, "NUM_ORDER", NUM_ORDER);
            OracleUdt.SetValue(con, pUdt, "DESCRIPTION", DESCRIPTION);
            OracleUdt.SetValue(con, pUdt, "APPROVAL_LOGINNAME", APPROVAL_LOGINNAME);
            OracleUdt.SetValue(con, pUdt, "APPROVAL_USERNAME", APPROVAL_USERNAME);
            OracleUdt.SetValue(con, pUdt, "APPROVAL_TIME", APPROVAL_TIME);
            OracleUdt.SetValue(con, pUdt, "APPROVAL_DATE", APPROVAL_DATE);
            OracleUdt.SetValue(con, pUdt, "EXP_LOGINNAME", EXP_LOGINNAME);
            OracleUdt.SetValue(con, pUdt, "EXP_USERNAME", EXP_USERNAME);
            OracleUdt.SetValue(con, pUdt, "EXP_TIME", EXP_TIME);
            OracleUdt.SetValue(con, pUdt, "EXP_DATE", EXP_DATE);
            OracleUdt.SetValue(con, pUdt, "TH_AMOUNT", TH_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "PATIENT_TYPE_ID", PATIENT_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "SERE_SERV_PARENT_ID", SERE_SERV_PARENT_ID);
            OracleUdt.SetValue(con, pUdt, "IS_EXPEND", IS_EXPEND);
            OracleUdt.SetValue(con, pUdt, "IS_OUT_PARENT_FEE", IS_OUT_PARENT_FEE);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_REQ_ID", TDL_SERVICE_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "STENT_ORDER", STENT_ORDER);
            OracleUdt.SetValue(con, pUdt, "TDL_TREATMENT_ID", TDL_TREATMENT_ID);
            OracleUdt.SetValue(con, pUdt, "EQUIPMENT_SET_ID", EQUIPMENT_SET_ID);
            OracleUdt.SetValue(con, pUdt, "EQUIPMENT_SET_ORDER", EQUIPMENT_SET_ORDER);
            OracleUdt.SetValue(con, pUdt, "IS_USE_CLIENT_PRICE", IS_USE_CLIENT_PRICE);
            OracleUdt.SetValue(con, pUdt, "EXPEND_TYPE_ID", EXPEND_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "SERIAL_NUMBER", SERIAL_NUMBER);
            OracleUdt.SetValue(con, pUdt, "REMAIN_REUSE_COUNT", REMAIN_REUSE_COUNT);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_PRES", IS_NOT_PRES);
            OracleUdt.SetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES", USE_ORIGINAL_UNIT_FOR_PRES);
            OracleUdt.SetValue(con, pUdt, "BCS_REQ_AMOUNT", BCS_REQ_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "FAILED_AMOUNT", FAILED_AMOUNT);
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
            EXP_MEST_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_ID");
            MATERIAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "MATERIAL_ID");
            TDL_MEDI_STOCK_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MEDI_STOCK_ID");
            MEDI_STOCK_PERIOD_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID");
            TDL_MATERIAL_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MATERIAL_TYPE_ID");
            TDL_AGGR_EXP_MEST_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_AGGR_EXP_MEST_ID");
            EXP_MEST_MATY_REQ_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_MATY_REQ_ID");
            CK_IMP_MEST_MATERIAL_ID = (long?)OracleUdt.GetValue(con, pUdt, "CK_IMP_MEST_MATERIAL_ID");
            IS_EXPORT = (short?)OracleUdt.GetValue(con, pUdt, "IS_EXPORT");
            AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "AMOUNT");
            PRICE = (decimal?)OracleUdt.GetValue(con, pUdt, "PRICE");
            VAT_RATIO = (decimal?)OracleUdt.GetValue(con, pUdt, "VAT_RATIO");
            DISCOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "DISCOUNT");
            NUM_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "NUM_ORDER");
            DESCRIPTION = (string)OracleUdt.GetValue(con, pUdt, "DESCRIPTION");
            APPROVAL_LOGINNAME = (string)OracleUdt.GetValue(con, pUdt, "APPROVAL_LOGINNAME");
            APPROVAL_USERNAME = (string)OracleUdt.GetValue(con, pUdt, "APPROVAL_USERNAME");
            APPROVAL_TIME = (long?)OracleUdt.GetValue(con, pUdt, "APPROVAL_TIME");
            APPROVAL_DATE = (long?)OracleUdt.GetValue(con, pUdt, "APPROVAL_DATE");
            EXP_LOGINNAME = (string)OracleUdt.GetValue(con, pUdt, "EXP_LOGINNAME");
            EXP_USERNAME = (string)OracleUdt.GetValue(con, pUdt, "EXP_USERNAME");
            EXP_TIME = (long?)OracleUdt.GetValue(con, pUdt, "EXP_TIME");
            EXP_DATE = (long?)OracleUdt.GetValue(con, pUdt, "EXP_DATE");
            TH_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "TH_AMOUNT");
            PATIENT_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "PATIENT_TYPE_ID");
            SERE_SERV_PARENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "SERE_SERV_PARENT_ID");
            IS_EXPEND = (short?)OracleUdt.GetValue(con, pUdt, "IS_EXPEND");
            IS_OUT_PARENT_FEE = (short?)OracleUdt.GetValue(con, pUdt, "IS_OUT_PARENT_FEE");
            TDL_SERVICE_REQ_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_REQ_ID");
            STENT_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "STENT_ORDER");
            TDL_TREATMENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_TREATMENT_ID");
            EQUIPMENT_SET_ID = (long?)OracleUdt.GetValue(con, pUdt, "EQUIPMENT_SET_ID");
            EQUIPMENT_SET_ORDER = (long?)OracleUdt.GetValue(con, pUdt, "EQUIPMENT_SET_ORDER");
            IS_USE_CLIENT_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_USE_CLIENT_PRICE");
            EXPEND_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXPEND_TYPE_ID");
            SERIAL_NUMBER = (string)OracleUdt.GetValue(con, pUdt, "SERIAL_NUMBER");
            REMAIN_REUSE_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "REMAIN_REUSE_COUNT");
            IS_NOT_PRES = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_PRES");
            USE_ORIGINAL_UNIT_FOR_PRES = (short?)OracleUdt.GetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES");
            BCS_REQ_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "BCS_REQ_AMOUNT");
            FAILED_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "FAILED_AMOUNT");
        }

    }
}
