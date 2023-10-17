using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.ADO;
using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignPrescriptionYHCT.AssignPrescription;

namespace HIS.Desktop.Plugins.AssignPrescriptionYHCT.AssignPrescription
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
