using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.AssignService.PaylaterDeposit
{
    class HisTransReqDepositData
    {
        public HIS_TREATMENT Treatment { get; set; }
        public HIS_BRANCH Branch { get; set; }
        public List<V_HIS_SERE_SERV> SereServs { get; set; }
        public long CashierRoomId { get; set; }
        public long AccountBookId { get; set; }
        public string TransReqCode { get; set; }
        public string ServiceCode { get; set; }
        public string LoginName { get; set; }
        public string UserName { get; set; }
    }
}
