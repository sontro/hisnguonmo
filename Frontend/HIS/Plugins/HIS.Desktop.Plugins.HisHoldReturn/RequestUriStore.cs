using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisHoldReturn
{
    class RequestUriStore
    {
        internal const string HIS_HORE_DHTY_GET = "api/HisHoreDhty/Get";
        internal const string HIS_TREATMENT_GET = "api/HisTreatment/Get";
        internal const string HIS_TREATMENT_GETView = "api/HisTreatment/GetView";
        internal const string HIS_HOLD_RETURN_GET = "api/HisHoldReturn/Get";
        internal const string HIS_HOLD_RETURN_GETVIEW = "api/HisHoldReturn/GetView";
        internal const string HIS_HOLD_RETURN_CREATE = "api/HisHoldReturn/Create";
        internal const string HIS_HOLD_RETURN_CREATESDO = "api/HisHoldReturn/CreateSdo";
        internal const string HIS_HOLD_RETURN_UPDATESDO = "api/HisHoldReturn/UpdateSdo";
        internal const string HIS_HOLD_RETURN_UPDATE = "api/HisHoldReturn/Update";
        internal const string HIS_HOLD_RETURN_DELETE = "api/HisHoldReturn/Delete";
        internal const string HIS_PATIENT_GETSPREVIOUSWARNING = "api/HisPatient/GetPreviousWarning";
        internal const string HIS_PATIENT_GETSDOADVANCE = "api/HisPatient/GetSdoAdvance";
    }
}
