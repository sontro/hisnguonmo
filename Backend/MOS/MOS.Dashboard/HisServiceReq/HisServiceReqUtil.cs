using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.Dashboard.HisServiceReq
{
    class HisServiceReqUtil
    {
        public static List<long> SERVICE_REQ_TYPE_IDs__SUBCLINICAL = new List<long>()
        {
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__CDHA,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__GPBL,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__NS,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__PHCN,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__SA,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__TDCN,
            IMSys.DbConfig.HIS_RS.HIS_SERVICE_REQ_TYPE.ID__XN,
        };
    }
}
