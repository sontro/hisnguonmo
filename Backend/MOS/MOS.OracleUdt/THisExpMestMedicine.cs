using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisExpMestMedicine : INullable, IOracleCustomType
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

        [OracleObjectMappingAttribute("MEDICINE_ID")]
        public Nullable<long> MEDICINE_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_MEDI_STOCK_ID")]
        public Nullable<long> TDL_MEDI_STOCK_ID { get; set; }

        [OracleObjectMappingAttribute("MEDI_STOCK_PERIOD_ID")]
        public Nullable<long> MEDI_STOCK_PERIOD_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_MEDICINE_TYPE_ID")]
        public Nullable<long> TDL_MEDICINE_TYPE_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_AGGR_EXP_MEST_ID")]
        public Nullable<long> TDL_AGGR_EXP_MEST_ID { get; set; }

        [OracleObjectMappingAttribute("EXP_MEST_METY_REQ_ID")]
        public Nullable<long> EXP_MEST_METY_REQ_ID { get; set; }

        [OracleObjectMappingAttribute("CK_IMP_MEST_MEDICINE_ID")]
        public Nullable<long> CK_IMP_MEST_MEDICINE_ID { get; set; }

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

        [OracleObjectMappingAttribute("USE_TIME_TO")]
        public Nullable<long> USE_TIME_TO { get; set; }

        [OracleObjectMappingAttribute("TUTORIAL")]
        public string TUTORIAL { get; set; }

        [OracleObjectMappingAttribute("TDL_SERVICE_REQ_ID")]
        public Nullable<long> TDL_SERVICE_REQ_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_TREATMENT_ID")]
        public Nullable<long> TDL_TREATMENT_ID { get; set; }

        [OracleObjectMappingAttribute("IS_USE_CLIENT_PRICE")]
        public Nullable<short> IS_USE_CLIENT_PRICE { get; set; }

        [OracleObjectMappingAttribute("VACCINATION_RESULT_ID")]
        public Nullable<long> VACCINATION_RESULT_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_VACCINATION_ID")]
        public Nullable<long> TDL_VACCINATION_ID { get; set; }

        [OracleObjectMappingAttribute("SPEED")]
        public Nullable<decimal> SPEED { get; set; }

        [OracleObjectMappingAttribute("EXPEND_TYPE_ID")]
        public Nullable<long> EXPEND_TYPE_ID { get; set; }

        [OracleObjectMappingAttribute("IS_NOT_PRES")]
        public Nullable<short> IS_NOT_PRES { get; set; }

        [OracleObjectMappingAttribute("USE_ORIGINAL_UNIT_FOR_PRES")]
        public Nullable<short> USE_ORIGINAL_UNIT_FOR_PRES { get; set; }

        [OracleObjectMappingAttribute("BCS_REQ_AMOUNT")]
        public Nullable<decimal> BCS_REQ_AMOUNT { get; set; }

        [OracleObjectMappingAttribute("DAY_COUNT")]
        public Nullable<long> DAY_COUNT { get; set; }

        [OracleObjectMappingAttribute("MORNING")]
        public string MORNING { get; set; }

        [OracleObjectMappingAttribute("NOON")]
        public string NOON { get; set; }

        [OracleObjectMappingAttribute("AFTERNOON")]
        public string AFTERNOON { get; set; }

        [OracleObjectMappingAttribute("EVENING")]
        public string EVENING { get; set; }

        [OracleObjectMappingAttribute("HTU_ID")]
        public Nullable<long> HTU_ID { get; set; }


        public static THisExpMestMedicine Null
        {
            get
            {
                THisExpMestMedicine company = new THisExpMestMedicine();
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
            OracleUdt.SetValue(con, pUdt, "MEDICINE_ID", MEDICINE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDI_STOCK_ID", TDL_MEDI_STOCK_ID);
            OracleUdt.SetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID", MEDI_STOCK_PERIOD_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_MEDICINE_TYPE_ID", TDL_MEDICINE_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_AGGR_EXP_MEST_ID", TDL_AGGR_EXP_MEST_ID);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_METY_REQ_ID", EXP_MEST_METY_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "CK_IMP_MEST_MEDICINE_ID", CK_IMP_MEST_MEDICINE_ID);
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
            OracleUdt.SetValue(con, pUdt, "USE_TIME_TO", USE_TIME_TO);
            OracleUdt.SetValue(con, pUdt, "TUTORIAL", TUTORIAL);
            OracleUdt.SetValue(con, pUdt, "TDL_SERVICE_REQ_ID", TDL_SERVICE_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_TREATMENT_ID", TDL_TREATMENT_ID);
            OracleUdt.SetValue(con, pUdt, "IS_USE_CLIENT_PRICE", IS_USE_CLIENT_PRICE);
            OracleUdt.SetValue(con, pUdt, "VACCINATION_RESULT_ID", VACCINATION_RESULT_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_VACCINATION_ID", TDL_VACCINATION_ID);
            OracleUdt.SetValue(con, pUdt, "SPEED", SPEED);
            OracleUdt.SetValue(con, pUdt, "EXPEND_TYPE_ID", EXPEND_TYPE_ID);
            OracleUdt.SetValue(con, pUdt, "IS_NOT_PRES", IS_NOT_PRES);
            OracleUdt.SetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES", USE_ORIGINAL_UNIT_FOR_PRES);
            OracleUdt.SetValue(con, pUdt, "BCS_REQ_AMOUNT", BCS_REQ_AMOUNT);
            OracleUdt.SetValue(con, pUdt, "DAY_COUNT", DAY_COUNT);
            OracleUdt.SetValue(con, pUdt, "MORNING", MORNING);
            OracleUdt.SetValue(con, pUdt, "NOON", NOON);
            OracleUdt.SetValue(con, pUdt, "AFTERNOON", AFTERNOON);
            OracleUdt.SetValue(con, pUdt, "EVENING", EVENING);
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
            EXP_MEST_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_ID");
            MEDICINE_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDICINE_ID");
            TDL_MEDI_STOCK_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MEDI_STOCK_ID");
            MEDI_STOCK_PERIOD_ID = (long?)OracleUdt.GetValue(con, pUdt, "MEDI_STOCK_PERIOD_ID");
            TDL_MEDICINE_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_MEDICINE_TYPE_ID");
            TDL_AGGR_EXP_MEST_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_AGGR_EXP_MEST_ID");
            EXP_MEST_METY_REQ_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXP_MEST_METY_REQ_ID");
            CK_IMP_MEST_MEDICINE_ID = (long?)OracleUdt.GetValue(con, pUdt, "CK_IMP_MEST_MEDICINE_ID");
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
            USE_TIME_TO = (long?)OracleUdt.GetValue(con, pUdt, "USE_TIME_TO");
            TUTORIAL = (string)OracleUdt.GetValue(con, pUdt, "TUTORIAL");
            TDL_SERVICE_REQ_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_SERVICE_REQ_ID");
            TDL_TREATMENT_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_TREATMENT_ID");
            IS_USE_CLIENT_PRICE = (short?)OracleUdt.GetValue(con, pUdt, "IS_USE_CLIENT_PRICE");
            VACCINATION_RESULT_ID = (long?)OracleUdt.GetValue(con, pUdt, "VACCINATION_RESULT_ID");
            TDL_VACCINATION_ID = (long?)OracleUdt.GetValue(con, pUdt, "TDL_VACCINATION_ID");
            SPEED = (decimal?)OracleUdt.GetValue(con, pUdt, "SPEED");
            EXPEND_TYPE_ID = (long?)OracleUdt.GetValue(con, pUdt, "EXPEND_TYPE_ID");
            IS_NOT_PRES = (short?)OracleUdt.GetValue(con, pUdt, "IS_NOT_PRES");
            USE_ORIGINAL_UNIT_FOR_PRES = (short?)OracleUdt.GetValue(con, pUdt, "USE_ORIGINAL_UNIT_FOR_PRES");
            BCS_REQ_AMOUNT = (decimal?)OracleUdt.GetValue(con, pUdt, "BCS_REQ_AMOUNT");
            DAY_COUNT = (long?)OracleUdt.GetValue(con, pUdt, "DAY_COUNT");
            MORNING = (string)OracleUdt.GetValue(con, pUdt, "MORNING");
            NOON = (string)OracleUdt.GetValue(con, pUdt, "NOON");
            AFTERNOON = (string)OracleUdt.GetValue(con, pUdt, "AFTERNOON");
            EVENING = (string)OracleUdt.GetValue(con, pUdt, "EVENING");
            HTU_ID = (long?)OracleUdt.GetValue(con, pUdt, "HTU_ID");

        }

    }
}
