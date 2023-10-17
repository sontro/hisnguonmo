using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TInt64 : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public Int64[] Int64Array;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TInt64 Null
        {
            get
            {
                TInt64 obj = new TInt64();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, Int64Array);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            Int64Array = (Int64[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
