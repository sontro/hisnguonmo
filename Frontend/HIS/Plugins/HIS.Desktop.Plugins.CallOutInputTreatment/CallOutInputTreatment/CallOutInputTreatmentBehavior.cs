using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.CallOutInputTreatment;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.CallOutInputTreatment;
using MOS.SDO;


namespace HIS.Desktop.Plugins.CallOutInputTreatment
{
    public sealed class CallOutInputTreatmentBehavior : Tool<IDesktopToolContext>, ICallOutInputTreatment
    {
     Inventec.Desktop.Common.Modules.Module module=null;
     RefeshReference RefeshReference = null;
     PatientTypeDepartmentADO TreatmentLogSDO = null;
        public CallOutInputTreatmentBehavior()
            : base()
        {
        }

        public CallOutInputTreatmentBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module module, RefeshReference RefeshReference, PatientTypeDepartmentADO HisTreatmentLogSDO)
            : base()
        {
         this.module = module;
         this.RefeshReference = RefeshReference;
         this.TreatmentLogSDO = HisTreatmentLogSDO;
        }

        object ICallOutInputTreatment.Run()
        {
            try
            {
             return new frmCallOutInputTreatment(module, RefeshReference, TreatmentLogSDO);
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
