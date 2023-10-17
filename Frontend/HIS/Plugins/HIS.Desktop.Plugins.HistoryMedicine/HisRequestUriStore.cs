using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HistoryMedicine
{
    class HisRequestUriStore
    {
        internal const string MOSHIS_PATIENT_PROGRAM_CREATE = "api/HistoryMedicine/Create";
        internal const string MOSHIS_PATIENT_PROGRAM_DELETE = "api/HistoryMedicine/Delete";
        internal const string MOSHIS_PATIENT_PROGRAM_UPDATE = "api/HistoryMedicine/Update";
        internal const string MOSHIS_PATIENT_PROGRAM_GET = "api/HistoryMedicine/Get";
        internal const string MOSHIS_PATIENT_PROGRAM_GETVIEW = "api/HistoryMedicine/GetView";
        internal const string MOSHIS_PATIENT_PROGRAM_CHANGE_LOCK = "api/HistoryMedicine/ChangeLock";
    }
}
