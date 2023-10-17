using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CallBriefPatient;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.CallBriefPatient;


namespace HIS.Desktop.Plugins.CallBriefPatient
{
    public sealed class CallBriefPatientBehavior : Tool<IDesktopToolContext>, ICallBriefPatient
    {
     Inventec.Desktop.Common.Modules.Module module;
     RefeshReference RefeshReference;
        MOS.SDO.HisTreatmentLogSDO treatmentSDO;
        public CallBriefPatientBehavior()
            : base()
        {
        }

        public CallBriefPatientBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module module, RefeshReference RefeshReference, MOS.SDO.HisTreatmentLogSDO treatmentSDO)
            : base()
        {
this.treatmentSDO = treatmentSDO;
         this.module = module;
         this.RefeshReference = RefeshReference;
        }

        object ICallBriefPatient.Run()
        {
            try
            {
             return new frmCallBriefPatient(module, RefeshReference, treatmentSDO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                //param.HasException = true;
                return null;
            }
        }
    }
}
