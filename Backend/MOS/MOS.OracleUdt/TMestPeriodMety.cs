using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMestPeriodMety : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMestPeriodMety[] MestPeriodMetyArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMestPeriodMety Null
        {
            get
            {
                TMestPeriodMety obj = new TMestPeriodMety();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MestPeriodMetyArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MestPeriodMetyArray = (THisMestPeriodMety[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
