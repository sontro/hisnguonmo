using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.MealRationDetail
{
    class RequestUriStore
    {
        internal const string HIS_EXP_MEST_Get = "api/HisExpMest/Get";
        internal const string HIS_AGGR_EXP_MEST_GetView = "api/HisAggrExpMest/GetView";
        internal const string HIS_EXP_MEST_AGGREXPORT = "api/HisExpMest/AggrExport";
        internal const string Ration_Sum_Approve = "api/HisRationSum/Approve";
        internal const string Ration_Sum_Unapprove = "api/HisRationSum/Unapprove";
        internal const string Ration_Sum_Remove = "api/HisRationSum/Remove";
    }
}
