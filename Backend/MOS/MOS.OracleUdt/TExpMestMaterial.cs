using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TExpMestMaterial : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisExpMestMaterial[] ExpMestMaterialArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TExpMestMaterial Null
        {
            get
            {
                TExpMestMaterial obj = new TExpMestMaterial();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, ExpMestMaterialArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            ExpMestMaterialArray = (THisExpMestMaterial[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
