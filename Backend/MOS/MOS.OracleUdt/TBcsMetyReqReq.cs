using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TBcsMetyReqReq : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisBcsMetyReqReq[] BcsMetyReqReqArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TBcsMetyReqReq Null
        {
            get
            {
                TBcsMetyReqReq obj = new TBcsMetyReqReq();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, BcsMetyReqReqArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            BcsMetyReqReqArray = (THisBcsMetyReqReq[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
