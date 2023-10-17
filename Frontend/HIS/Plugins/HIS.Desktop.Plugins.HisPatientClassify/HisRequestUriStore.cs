using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisPatientClassify
{
    public class HisRequestUriStore
    {
        internal const string HIS_PATIENT_CLASSIFY_CREATE = "/api/HisPatientClassify/Create";
        internal const string HIS_PATIENT_CLASSIFY_DELETE = "/api/HisPatientClassify/Delete";
        internal const string HIS_PATIENT_CLASSIFY_UPDATE = "/api/HisPatientClassify/Update";
        internal const string HIS_PATIENT_CLASSIFY_GET = "/api/HisPatientClassify/Get";
        internal const string HIS_PATIENT_CLASSIFY_CHANGE_LOCK = "/api/HisPatientClassify/ChangeLock";
    }
}
