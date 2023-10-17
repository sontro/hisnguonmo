using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TService : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisService[] ServiceArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TService Null
        {
            get
            {
                TService obj = new TService();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, ServiceArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            ServiceArray = (THisService[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
