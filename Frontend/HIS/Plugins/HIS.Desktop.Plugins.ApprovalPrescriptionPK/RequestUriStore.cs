using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.ApprovalPrescriptionPK
{
    class RequestUriStore
    {
        internal const string HIS_SERVICE_REQ_GET = "api/HisServiceReq/Get";
        internal const string HIS_EXP_MEST_APPROVA = "api/HisExpMest/Approve";
        internal const string HIS_EXP_MEST_AGGR_EXAM_APPROVE = "api/HisExpMest/AggrExamApprove";
        internal const string HIS_EXP_MEST_AGGR_EXAM_EXPORT = "api/HisExpMest/AggrExamExport";
        internal const string HIS_EXP_MEST_AGGR_EXAM_UNAPPROVE = "api/HisExpMest/AggrExamUnapprove";
        internal const string HIS_EXP_MEST_AGGR_EXAM_UNEXPORT = "api/HisExpMest/AggrExamUnexport";
    }
}
