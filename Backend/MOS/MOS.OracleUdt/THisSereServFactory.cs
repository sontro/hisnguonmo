using Oracle.DataAccess.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.OracleUDT
{
    [OracleCustomTypeMappingAttribute("HIS_RS.T_HIS_SERE_SERV")]
    public class THisSereServFactory : IOracleCustomTypeFactory
    {
        public IOracleCustomType CreateObject()
        {
            return new THisSereServ();
        }
    }
}
