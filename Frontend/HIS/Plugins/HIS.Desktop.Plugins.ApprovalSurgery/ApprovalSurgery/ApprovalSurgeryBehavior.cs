using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ApprovalSurgery;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.ApprovalSurgery.ApprovalSurgery
{
    public sealed class ApprovalSurgeryBehavior : Tool<IDesktopToolContext>, IApprovalSurgery
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        public ApprovalSurgeryBehavior()
            : base()
        {
        }

        public ApprovalSurgeryBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.moduleData = moduleData;
        }

        object IApprovalSurgery.Run()
        {
            try
            {
                return new UCApprovalSurgery(this.moduleData);
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
