using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMaterialBean : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMaterialBean[] MaterialBeanArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMaterialBean Null
        {
            get
            {
                TMaterialBean obj = new TMaterialBean();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MaterialBeanArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MaterialBeanArray = (THisMaterialBean[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
