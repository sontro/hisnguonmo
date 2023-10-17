using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Ration
{
    class RationThreadData
    {
        public List<HIS_SERVICE_REQ> ServiceReqs { get; set; }
        public List<HIS_SERE_SERV_RATION> SereServRations { get; set; }
        public bool IsHalfInFirstDay { get; set; }
    }

    class UpdateRationThreadData
    {
        public HIS_SERVICE_REQ ServiceReq { get; set; }
        public List<HIS_SERE_SERV_RATION> CurrentSereServRations { get; set; }
        public List<HIS_SERE_SERV_RATION> OldSereServRations { get; set; }
    }
}
