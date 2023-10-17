using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqUpdateResultSDO
    {
        public V_HIS_SERVICE_REQ ServiceReq { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
    }
}
