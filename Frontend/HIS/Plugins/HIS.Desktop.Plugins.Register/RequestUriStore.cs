using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Register
{
    class RequestUriStore
    {
        public const string HIS_PATIEN_PROGRAM_CREATE = "/api/HisPatientProgram/Create";
        public const string HIS_PATIEN_PROGRAM_UPDATE = "/api/HisPatientProgram/Update";
        public const string HIS_PATIEN_PROGRAM_GET = "/api/HisPatientProgram/GetViewByCode";
        public const string HIS_PATIEN_PROGRAM_GET_VIEW = "/api/HisPatientProgram/GetView";
        public const string HIS_PATIEN_PROGRAM_DELETE = "/api/HisPatientProgram/Delete";
        public const string HIS_APPOINTMENT_GETV = "/api/HisAppointment/GetViewByCode";
        public const string HIS_HOUSE_HOLD_GET_BY_CODE = "api/HisHousehold/GetByCode";
        public const string HIS_PATIENT_HOUSE_HOLD_GET = "/api/HisPatientHouseHold/GetView";
        public const string HIS_CASHIER_ROOM_GET = "/api/HisCashierRoom/Get";
        public const string HIS_CARD_GETVIEWBYSERVICECODE = "api/HisCard/GetCardSdoByCode";
        public const string HIS_PATIENT_GETSPREVIOUSWARNING = "api/HisPatient/GetPreviousWarning";
        public const string HIS_PATIENT_GETSDOADVANCE = "api/HisPatient/GetSdoAdvance";
        public const string HIS_SERE_SERV_GETVIEW_12 = "api/HisSereServ/GetView12";
        public const string HID_PERSON_GET = "api/HidPerson/Get";

        public const string HID_HOUSEHOLD_RELATION_GET = "api/HidHouseHoldRelation/Get";
    }
}
