using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  HIS.Desktop.Plugins.HisImportMedicineType
{
    class HisRequestUriStore
    {
        internal const string MOSHIS_PROGRAM_CREATE = "api/Import/Create";
        internal const string MOSHIS_PROGRAM_DELETE = "api/Import/Delete";
        internal const string MOSHIS_PROGRAM_UPDATE = "api/Import/Update";
        internal const string MOSHIS_PROGRAM_GET = "api/Import/Get";
        internal const string MOSHIS_PROGRAM_CHANGE_LOCK = "api/Import/ChangeLock";
    }
}
