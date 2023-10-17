using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TExpMestMedicine : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisExpMestMedicine[] ExpMestMedicineArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TExpMestMedicine Null
        {
            get
            {
                TExpMestMedicine obj = new TExpMestMedicine();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, ExpMestMedicineArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            ExpMestMedicineArray = (THisExpMestMedicine[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
