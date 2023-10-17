using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HistoryMaterial
{
    class HisRequestUriStore
    {
        internal const string MOSHIS_PATIENT_PROGRAM_CREATE = "api/HistoryMaterial/Create";
        internal const string MOSHIS_PATIENT_PROGRAM_DELETE = "api/HistoryMaterial/Delete";
        internal const string MOSHIS_PATIENT_PROGRAM_UPDATE = "api/HistoryMaterial/Update";
        internal const string MOSHIS_PATIENT_PROGRAM_GET = "api/HistoryMaterial/Get";
        internal const string MOSHIS_PATIENT_PROGRAM_GETVIEW = "api/HistoryMaterial/GetView";
        internal const string MOSHIS_PATIENT_PROGRAM_CHANGE_LOCK = "api/HistoryMaterial/ChangeLock";
    }
}
