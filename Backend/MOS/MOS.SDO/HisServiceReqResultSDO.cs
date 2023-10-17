using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqResultSDO
    {
        //Chi tiet cac y/c kham
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
        //Du lieu phieu y/c kham
        public V_HIS_SERVICE_REQ ServiceReq { get; set; }
    }
}
