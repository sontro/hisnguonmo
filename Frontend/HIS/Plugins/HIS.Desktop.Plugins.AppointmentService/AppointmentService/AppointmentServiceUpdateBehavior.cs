using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.AppointmentService;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace HIS.Desktop.Plugins.AppointmentService.AppointmentService
{
    public sealed class AppointmentServiceUpdateBehavior : Tool<IDesktopToolContext>, IAppointmentService
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long dispenseId;

        public AppointmentServiceUpdateBehavior()
            : base()
        {
        }

        public AppointmentServiceUpdateBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, long _dispenseId)
            : base()
        {
            this.moduleData = moduleData;
            this.dispenseId = _dispenseId;
        }

        object IAppointmentService.Run()
        {
            try
            {
                return new frmAppointmentService(this.moduleData, this.dispenseId);
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
