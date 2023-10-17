using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMedicineBean : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMedicineBean[] MedicineBeanArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMedicineBean Null
        {
            get
            {
                TMedicineBean obj = new TMedicineBean();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MedicineBeanArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MedicineBeanArray = (THisMedicineBean[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
