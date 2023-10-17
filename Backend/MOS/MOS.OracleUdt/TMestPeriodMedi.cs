using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodMedi : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodMedi[] MestPeriodMediArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodMedi Null
        {
            get
            {
                TMestPeriodMedi obj = new TMestPeriodMedi();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MestPeriodMediArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MestPeriodMediArray = (THisMestPeriodMedi[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
