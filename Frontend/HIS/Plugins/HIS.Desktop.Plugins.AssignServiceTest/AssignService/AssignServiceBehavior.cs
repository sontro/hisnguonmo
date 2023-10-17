using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AssignServiceTest;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.Plugins.AssignServiceTest.ADO;
using HIS.Desktop.ADO;
using HIS.Desktop.Plugins.AssignServiceTest.AssignService;

namespace HIS.Desktop.Plugins.AssignServiceTest.AssignService
{
    public sealed class AssignServiceBehavior : Tool<IDesktopToolContext>, IAssignService
    {
        AssignServiceTestADO entity;
        Inventec.Desktop.Common.Modules.Module Module;
        public AssignServiceBehavior()
            : base()
        {
        }

        public AssignServiceBehavior(CommonParam param, AssignServiceTestADO data, Inventec.Desktop.Common.Modules.Module module)
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
