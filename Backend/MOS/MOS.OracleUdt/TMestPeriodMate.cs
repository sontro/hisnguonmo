using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodMate : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodMate[] MestPeriodMatyArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodMate Null
        {
            get
            {
                TMestPeriodMate obj = new TMestPeriodMate();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MestPeriodMatyArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MestPeriodMatyArray = (THisMestPeriodMate[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
