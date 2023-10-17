using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.HisAppointmentPeriod
{
    class HisRequestUriStore
    {
        internal const string MOSHIS_APPOINTMENT_PERIOD_CREATE = "api/HisAppointmentPeriod/Create";
        internal const string MOSHIS_APPOINTMENT_PERIOD_DELETE = "api/HisAppointmentPeriod/Delete";
        internal const string MOSHIS_APPOINTMENT_PERIOD_UPDATE = "api/HisAppointmentPeriod/Update";
        internal const string MOSHIS_APPOINTMENT_PERIOD_GET = "api/HisAppointmentPeriod/Get";
        internal const string MOSHIS_APPOINTMENT_PERIOD_LOCK = "api/HisAppointmentPeriod/ChangeLock";
    }
}
