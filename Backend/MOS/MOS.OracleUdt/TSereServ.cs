using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TSereServ : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisSereServ[] SereServArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TSereServ Null
        {
            get
            {
                TSereServ obj = new TSereServ();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, SereServArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            SereServArray = (THisSereServ[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
