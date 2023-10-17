using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodBlood : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodBlood[] MestPeriodBloodArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodBlood Null
        {
            get
            {
                TMestPeriodBlood obj = new TMestPeriodBlood();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MestPeriodBloodArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MestPeriodBloodArray = (THisMestPeriodBlood[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
