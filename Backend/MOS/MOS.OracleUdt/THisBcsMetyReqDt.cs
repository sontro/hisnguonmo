using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class THisBcsMetyReqDt : INullable, IOracleCustomType
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

        [OracleObjectMappingAttribute("EXP_MEST_MEDICINE_ID")]
        public long EXP_MEST_MEDICINE_ID { get; set; }

        [OracleObjectMappingAttribute("EXP_MEST_METY_REQ_ID")]
        public long EXP_MEST_METY_REQ_ID { get; set; }

        [OracleObjectMappingAttribute("AMOUNT")]
        public decimal AMOUNT { get; set; }

        [OracleObjectMappingAttribute("TDL_XBTT_EXP_MEST_ID")]
        public long TDL_XBTT_EXP_MEST_ID { get; set; }

        [OracleObjectMappingAttribute("TDL_XBTT_EXP_MEST_CODE")]
        public string TDL_XBTT_EXP_MEST_CODE { get; set; }


        public static THisBcsMetyReqDt Null
        {
            get
            {
                THisBcsMetyReqDt company = new THisBcsMetyReqDt();
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
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_MEDICINE_ID", EXP_MEST_MEDICINE_ID);
            OracleUdt.SetValue(con, pUdt, "EXP_MEST_METY_REQ_ID", EXP_MEST_METY_REQ_ID);
            OracleUdt.SetValue(con, pUdt, "AMOUNT", AMOUNT);
            OracleUdt.SetValue(con, pUdt, "TDL_XBTT_EXP_MEST_ID", TDL_XBTT_EXP_MEST_ID);
            OracleUdt.SetValue(con, pUdt, "TDL_XBTT_EXP_MEST_CODE", TDL_XBTT_EXP_MEST_CODE);
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
            EXP_MEST_MEDICINE_ID = (long)OracleUdt.GetValue(con, pUdt, "EXP_MEST_MEDICINE_ID");
            EXP_MEST_METY_REQ_ID = (long)OracleUdt.GetValue(con, pUdt, "EXP_MEST_METY_REQ_ID");
            AMOUNT = (decimal)OracleUdt.GetValue(con, pUdt, "AMOUNT");
            TDL_XBTT_EXP_MEST_ID = (long)OracleUdt.GetValue(con, pUdt, "TDL_XBTT_EXP_MEST_ID");
            TDL_XBTT_EXP_MEST_CODE = (string)OracleUdt.GetValue(con, pUdt, "TDL_XBTT_EXP_MEST_CODE");
        }

    }
}
