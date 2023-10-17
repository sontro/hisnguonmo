using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodBlty : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodBlty[] MestPeriodBltyArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodBlty Null
        {
            get
            {
                TMestPeriodBlty obj = new TMestPeriodBlty();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MestPeriodBltyArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MestPeriodBltyArray = (THisMestPeriodBlty[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
