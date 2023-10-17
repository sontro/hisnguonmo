using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HID.Desktop.Plugins.FamilyInformation
{
    class HidRequestUriStore
    {
        internal const string MOSHIS_ICD_CREATE = "api/HidPerson/Create";
        internal const string MOSHIS_ICD_DELETE = "api/HidPerson/Delete";
        internal const string MOSHIS_ICD_UPDATE = "api/HidPerson/Update";
        internal const string MOSHIS_ICD_GET = "api/HidPerson/Get";
        internal const string MOSHIS_ICD_CHANGELOCK = "api/HidPerson/ChangeLock";
    }
}
