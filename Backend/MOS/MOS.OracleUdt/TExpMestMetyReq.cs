using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    public class TExpMestMetyReq : INullable, IOracleCustomType
    {
        [OracleArrayMapping()]
        public THisExpMestMetyReq[] ExpMestMetyReqArray;

        private bool objectIsNull;

        #region INullable Members

        public bool IsNull
        {
            get { return objectIsNull; }
        }

        public static TExpMestMetyReq Null
        {
            get
            {
                TExpMestMetyReq obj = new TExpMestMetyReq();
                obj.objectIsNull = true;
                return obj;
            }
        }

        #endregion

        #region IOracleCustomType Members

        public void FromCustomObject(OracleConnection con, IntPtr pUdt)
        {
            OracleUdt.SetValue(con, pUdt, 0, ExpMestMetyReqArray);
        }

        public void ToCustomObject(OracleConnection con, IntPtr pUdt)
        {
            ExpMestMetyReqArray = (THisExpMestMetyReq[])OracleUdt.GetValue(con, pUdt, 0);
        }

        #endregion
    }
}
