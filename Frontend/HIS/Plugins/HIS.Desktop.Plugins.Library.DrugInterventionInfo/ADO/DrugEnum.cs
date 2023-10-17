using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.Library.DrugInterventionInfo.ADO
{
    class DrugEnum
    {
        public enum EServiceType
        {
            /// <summary>
            /// Không xác định
            /// </summary>
            Undefined = 0,

            /// <summary>
            /// Ngoại trú
            /// </summary>
            Outpatient = 1,

            /// <summary>
            /// Nội trú
            /// </summary>
            Inpatient = 2
        }

        public enum ValidationSeverityLevel
        {
            Normal = 0,
            Warning = 1,
            Major = 2,
            Contraindicated = 3
        }

        public enum TopIssue
        {
            Undefined = 0,
            DrugInteraction = 1,
            DuplicateTherapy = 2,
            Overdose = 3,
            IVSolvent = 4,
            DrugRoute = 5
        }

        public enum WarningCategory
        {
            Undefined = 0,
            Pregnancy = 1,
        }
    }
}
