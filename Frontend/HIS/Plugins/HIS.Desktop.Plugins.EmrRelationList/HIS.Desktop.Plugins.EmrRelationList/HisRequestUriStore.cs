using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.EmrRelationList
{
    class HisRequestUriStore
    {
        internal const string EMR_RELATION_CREATE = "api/EmrRelation/Create";
        internal const string EMR_RELATION_DELETE = "api/EmrRelation/Delete";
        internal const string EMR_RELATION_UPDATE = "api/EmrRelation/Update";
        internal const string EMR_RELATION_GET = "api/EmrRelation/Get";
        internal const string EMR_RELATION_CHANGE_LOCK = "api/EmrRelation/ChangeLock";
    }
}
