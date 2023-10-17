using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.CreatePatientList
{
    class HisRequestUriStore
    {
        public const string HIS_PATIENT_CREATE = "api/HisPatient/Create";
        public const string HIS_PATIENT_GETVIEW = "api/HisPatient/GetView";
        public const string HIS_MILITARY_RANK_GET = "api/HisMilitaryRank/Get";
        public const string HIS_CAREER_GET = "api/HisCareer/Get";
        public const string HIS_BLOOD_ABO__GET = "api/HisBloodAbo/Get";
        public const string HIS_BLOOD_RH__GET = "api/HisBloodRh/Get";
        public const string HIS_GENDER_GET = "api/HisGender/Get";
    }
}
