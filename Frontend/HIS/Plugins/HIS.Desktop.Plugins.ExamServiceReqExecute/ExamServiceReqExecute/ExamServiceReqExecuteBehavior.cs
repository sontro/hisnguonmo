using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.ExamServiceReqExecute;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;
using System.Windows.Forms;
using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;

namespace Inventec.Desktop.Plugins.ExamServiceReqExecute.ExamServiceReqExecute
{
    public sealed class ExamServiceReqExecuteBehavior : Tool<IDesktopToolContext>, IExamServiceReqExecute
    {
        long treatmentId;
        long intructionTime;
        long serviceReqId;
        MOS.EFMODEL.DataModels.V_HIS_SERVICE_REQ serviceReq;
        List<HIS_SERE_SERV> sereServCurrentTreatment;
        Inventec.Desktop.Common.Modules.Module moduleData;
        bool isChronic;
        DelegateSelectData reLoadServiceReq;

        public ExamServiceReqExecuteBehavior()
            : base()
        {
        }

        public ExamServiceReqExecuteBehavior(CommonParam param, V_HIS_SERVICE_REQ data, Inventec.Desktop.Common.Modules.Module moduleData, bool isChronic, DelegateSelectData reLoadServiceReq, List<HIS_SERE_SERV> sereServCurrent)
            : base()
        {
            this.serviceReq = data;
            this.moduleData = moduleData;
            this.isChronic = isChronic;
            this.reLoadServiceReq = reLoadServiceReq;
            this.sereServCurrentTreatment = sereServCurrent;
        }

        object IExamServiceReqExecute.Run()
        {
            try
            {
                return new ExamServiceReqExecuteControl(this.moduleData, serviceReq, isChronic, reLoadServiceReq, this.sereServCurrentTreatment);
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
