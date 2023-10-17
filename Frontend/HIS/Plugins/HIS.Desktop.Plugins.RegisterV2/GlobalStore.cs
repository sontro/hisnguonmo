using HIS.Desktop.LocalStorage.BackendData;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIS.Desktop.Plugins.RegisterV2
{
    enum UCServiceRequestRegisterFactorySaveType
    {
        REGISTER,
        PROFILE,
        VALID,
    }

    class GlobalStore
    {
        internal static List<long> PatientTypeIdAllows { get; set; }              
        internal static Inventec.Desktop.Common.Modules.Module CurrentModule { get; set; }
        internal static long DepartmentId { get; set; }
        internal static UCServiceRequestRegisterFactorySaveType currentFactorySaveType { get; set; }
    }
}
