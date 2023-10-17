using MOS.EFMODEL.DataModels;
using System.Collections.Generic;

namespace MOS.SDO
{
    public class HisServiceReqListResultSDO
    {
        public string SessionCode { get; set; }
        public List<V_HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
        public List<HIS_SERE_SERV_EXT> SereServExts { get; set; }
        public List<HIS_SERE_SERV_RATION> SereServRations { get; set; }
        public List<HIS_SERE_SERV_BILL> SereServBills { get; set; }
        public List<HIS_SERE_SERV_DEPOSIT> SereServDeposits { get; set; }
        public List<V_HIS_SERE_SERV> DepositedSereServs { get; set; }
        public List<V_HIS_TRANSACTION> Transactions { get; set; }
    }
}
