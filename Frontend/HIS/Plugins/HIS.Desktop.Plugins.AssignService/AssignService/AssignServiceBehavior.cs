using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AssignService;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.AssignService.ADO;
using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignService.AssignService;

namespace Inventec.Desktop.Plugins.AssignService.AssignService
{
    public sealed class AssignServiceBehavior : Tool<IDesktopToolContext>, IAssignService
    {
        AssignServiceADO entity;
        Inventec.Desktop.Common.Modules.Module Module;
        public AssignServiceBehavior()
            : base()
        {
        }

        public AssignServiceBehavior(CommonParam param, AssignServiceADO data, Inventec.Desktop.Common.Modules.Module module)
            : base()
        {
            this.entity = data;
            this.Module = module;
        }

        object IAssignService.Run()
        {
            try
            {
                return new frmAssignService(this.Module, this.entity);
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
