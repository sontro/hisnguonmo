using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TBcsMatyReqReq : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisBcsMatyReqReq[] BcsMatyReqReqArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TBcsMatyReqReq Null
        {
            get
            {
                TBcsMatyReqReq obj = new TBcsMatyReqReq();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, BcsMatyReqReqArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            BcsMatyReqReqArray = (THisBcsMatyReqReq[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
