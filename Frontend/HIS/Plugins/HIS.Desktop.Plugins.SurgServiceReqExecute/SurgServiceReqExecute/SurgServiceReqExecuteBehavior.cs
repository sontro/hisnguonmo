using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.SurgServiceReqExecute;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.SurgServiceReqExecute.SurgServiceReqExecute
{
    public sealed class SurgServiceReqExecuteBehavior : Tool<IDesktopToolContext>, ISurgServiceReqExecute
    {
        long treatmentId;
        long intructionTime;
        long serviceReqId;
        MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ serviceReq;
        Inventec.Desktop.Common.Modules.Module moduleData;

        public SurgServiceReqExecuteBehavior()
            : base()
        {
        }

        public SurgServiceReqExecuteBehavior(CommonParam param,V_HIS_SERVICE_REQ data, Inventec.Desktop.Common.Modules.Module moduleData)
            : base()
        {
            this.serviceReq = data;
            this.moduleData = moduleData;
        }

        object ISurgServiceReqExecute.Run()
        {
            try
            {
                return new SurgServiceReqExecuteControl(this.moduleData, serviceReq);
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
