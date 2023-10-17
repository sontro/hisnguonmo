using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    [OracleCustomTypeMappingAttribute("HIS_RS.T_HIS_BCS_METY_REQ_REQ")]
    public class THisBcsMetyReqReqFactory : IOracleCustomTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new THisBcsMetyReqReq();
        }
    }
}
