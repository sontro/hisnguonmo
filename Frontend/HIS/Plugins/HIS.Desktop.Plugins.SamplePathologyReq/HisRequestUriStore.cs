using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.SamplePathologyReq
{
    public class HisRequestUriStore
    {
        internal const string HIS_SAMPLE_ROOM_GET = "/api/HisSampleRoom/Get";
        internal const string HIS_SERVICE_REQ_GET = "/api/HisServiceReq/Get";
        internal const string HIS_SERVICE_REQ_PAAN_TAKE_SAMPLE = "/api/HisServiceReq/PaanTakeSample";
        internal const string HIS_SERVICE_REQ_PAAN_CANCEL_SAMPLE = "/api/HisServiceReq/PaanCancelSample";
        internal const string HIS_SERVICE_REQ_GETVIEW = "/api/HisServiceReq/GetView";
        internal const string HIS_SERVICE_REQ_PAAN_BLOCK = "/api/HisServiceReq/PaanBlock";
        internal const string HIS_SERVICE_REQ_PAAN_INTRUCTION_NOTE = "/api/HisServiceReq/PaanIntructionNote";
    }
}
