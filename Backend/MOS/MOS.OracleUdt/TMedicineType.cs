using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TMedicineType : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisMedicineType[] MedicineTypeArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TMedicineType Null
        {
            get
            {
                TMedicineType obj = new TMedicineType();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, MedicineTypeArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            MedicineTypeArray = (THisMedicineType[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
