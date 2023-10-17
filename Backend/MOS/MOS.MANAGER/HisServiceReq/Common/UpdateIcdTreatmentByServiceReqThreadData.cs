using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOS.MANAGER.HisServiceReq.Common
{
    //cap nhat treatment khi nguoi dung update service_Req
    class UpdateIcdTreatmentByServiceReqThreadData
    {
        public HIS_SERVICE_REQ NewServiceReq { get; set; }
        public HIS_SERVICE_REQ OldServiceReq { get; set; }
        public HIS_TREATMENT Treatment { get; set; }
    }
}
