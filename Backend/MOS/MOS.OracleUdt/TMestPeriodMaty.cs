using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodMaty : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodMaty[] MestPeriodMatyArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodMaty Null
        {
            get
            {
                TMestPeriodMaty obj = new TMestPeriodMaty();
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
            MestPeriodMatyArray = (THisMestPeriodMaty[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
