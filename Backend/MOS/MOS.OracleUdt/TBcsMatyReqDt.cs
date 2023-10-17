using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TBcsMatyReqDt : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisBcsMatyReqDt[] BcsMatyReqDtArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TBcsMatyReqDt Null
        {
            get
            {
                TBcsMatyReqDt obj = new TBcsMatyReqDt();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, BcsMatyReqDtArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            BcsMatyReqDtArray = (THisBcsMatyReqDt[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
