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
    public sealed class AppointmentServiceBehavior : Tool<IDesktopToolContext>, IAppointmentService
    {
        Inventec.Desktop.Common.Modules.Module moduleData;
        long treatmentId { get; set; }
        AppointmentServiceADO appointmentServiceADO;
        public AppointmentServiceBehavior()
            : base()
        {
        }

        public AppointmentServiceBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, AppointmentServiceADO appointmentServiceADO)
            : base()
        {
            this.moduleData = moduleData;
            this.treatmentId = appointmentServiceADO != null ? appointmentServiceADO.TreatmentId : 0;
            this.appointmentServiceADO = appointmentServiceADO;
        }

        object IAppointmentService.Run()
        {
            try
            {
                return new frmAppointmentService(this.moduleData, treatmentId);
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
