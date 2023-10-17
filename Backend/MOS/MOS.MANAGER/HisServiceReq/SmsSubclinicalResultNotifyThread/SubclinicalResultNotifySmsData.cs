using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.SmsSubclinicalResultNotifyThread
{
    public class SubclinicalResultNotifySmsData
    {
        public long ServiceReqId { get; set; }
        public string PatientName { get; set; }
        public long DateOfBirth { get; set; }
        public string ServiceReqTypeName { get; set; }
        public long ServiceReqTypeId { get; set; }
        public string Mobile { get; set; }
        public string ExecuteRoomName { get; set; }
        public long? ResultingOrder { get; set; }
    }
}
