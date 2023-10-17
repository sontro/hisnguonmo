using HIS.Desktop.ADO;
using Inventec.Core;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System;

namespace HIS.Desktop.Plugins.AssignPrescriptionCLS.AssignPrescription
{
    public sealed class AssignPrescriptionBehavior : Tool<IDesktopToolContext>, IAssignPrescription
    {
        AssignPrescriptionADO assignPrescriptionADO;
        Inventec.Desktop.Common.Modules.Module Module;
        public AssignPrescriptionBehavior()
            : base()
        {
        }

        public AssignPrescriptionBehavior(Inventec.Desktop.Common.Modules.Module module, CommonParam param, AssignPrescriptionADO data)
            : base()
        {
            this.assignPrescriptionADO = data;
            this.Module = module;
        }

        object IAssignPrescription.Run()
        {
            try
            {
                return new frmAssignPrescription(this.Module, assignPrescriptionADO);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                return null;
            }
        }
    }
}
