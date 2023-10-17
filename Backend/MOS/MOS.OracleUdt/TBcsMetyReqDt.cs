using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TBcsMetyReqDt : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisBcsMetyReqDt[] BcsMetyReqDtArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TBcsMetyReqDt Null
        {
            get
            {
                TBcsMetyReqDt obj = new TBcsMetyReqDt();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, BcsMetyReqDtArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            BcsMetyReqDtArray = (THisBcsMetyReqDt[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
