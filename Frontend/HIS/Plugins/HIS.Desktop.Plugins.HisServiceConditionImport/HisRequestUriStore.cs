using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisServiceConditionImport
{
    class HisRequestUriStore
    {
        internal const string HIS_SERVICE_CONDITION_IMPORT_UPDATE = "/api/HisServiceCondition/Update";
        internal const string HIS_SERVICE_CONDITION_IMPORT_GET = "/api/HisServiceCondition/Get";
        internal const string HIS_SERVICE_CONDITION_IMPORT_CREATE = "api/HisServiceCondition/Create";
        internal const string HIS_SERVICE_CONDITION_IMPORT_DELETE = "api/HisServiceCondition/Delete";
        internal const string HIS_SERVICE_CONDITION_IMPORT_CHANGELOCK = "api/HisServiceCondition/ChangeLock";
    }
}
