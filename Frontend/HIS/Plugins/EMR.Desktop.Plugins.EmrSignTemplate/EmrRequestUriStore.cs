using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMR.Desktop.Plugins.EmrSignTemplate
{
    class EmrRequestUriStore
    {
        internal const string EMR_SIGN_TEMP_GET = "api/EmrSignTemp/Get";
        internal const string EMR_SIGNER_GET = "api/EmrSigner/Get";
        internal const string EMR_SIGN_TEMP_LOCK = "api/EmrSignTemp/Lock";
        internal const string EMR_SIGN_TEMP_UN_LOCK = "api/EmrSignTemp/UnLock";
        internal const string EMR_SIGN_TEMP_DELETE = "/api/EmrSignTemp/Delete";

        internal const string EMR_SIGN_TEMP_CREATE_BY_SDO = "api/EmrSignTemp/CreateBySdo";
        internal const string EMR_SIGN_TEMP_UPDATE_BY_SDO = "api/EmrSignTemp/UpdateBySdo";

        internal const string EMR_SIGN_ORDER_GET = "api/EmrSignOrder/Get";

    }
}
