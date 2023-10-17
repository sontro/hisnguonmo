using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TExpMestMatyReq : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisExpMestMatyReq[] ExpMestMatyReqArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TExpMestMatyReq Null
        {
            get
            {
                TExpMestMatyReq obj = new TExpMestMatyReq();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, ExpMestMatyReqArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            ExpMestMatyReqArray = (THisExpMestMatyReq[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
