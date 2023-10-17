using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalExportPrescription
{
    class RequestUriStore
    {
        internal const string HIS_PRESCRIPTION_GET_WITH_DETAIL = "api/HisPrescription/GetWithDetail";

        internal const string HIS_EXP_MEST_MEDICINE_GET = "api/HisExpMestMedicine/Get";
        internal const string HIS_EXP_MEST_MATERIAL_GET = "api/HisExpMestMaterial/Get";
        internal const string HIS_SERVICE_REQ_GET = "api/HisServiceReq/Get";
        internal const string HIS_EXP_MEST_GET = "api/HisExpMest/Get";

        internal const string HIS_EXP_MEST_APPROVA = "api/HisExpMest/Approve";
    }
}
