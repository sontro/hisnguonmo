using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMaterialType : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMaterialType[] MaterialTypeArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMaterialType Null
        {
            get
            {
                TMaterialType obj = new TMaterialType();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MaterialTypeArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MaterialTypeArray = (THisMaterialType[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
