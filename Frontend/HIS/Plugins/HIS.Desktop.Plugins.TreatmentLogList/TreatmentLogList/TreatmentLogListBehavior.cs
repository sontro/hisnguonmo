using Inventec.Core;
using HIS.Desktop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HIS.Desktop.Plugins.TreatmentLogList;
using Inventec.Desktop.Core.Actions;
using Inventec.Desktop.Core;
using Inventec.Desktop.Core.Tools;


using HIS.Desktop.ADO;
using MOS.EFMODEL.DataModels;


namespace Inventec.Desktop.Plugins.TreatmentLogList.TreatmentLogList
{
    public sealed class TreatmentLogListBehavior : Tool<IDesktopToolContext>, ITreatmentLogList
    {
        long treatmentId;
        Inventec.Desktop.Common.Modules.Module ModuleData;
long CurrentId = 0;
        public TreatmentLogListBehavior()
            : base()
        {
        }

        public TreatmentLogListBehavior(CommonParam param, Inventec.Desktop.Common.Modules.Module moduleData, TreatmentLogADO TreatmentLogADO)
            : base()
        {
         this.treatmentId = TreatmentLogADO.TreatmentId;
         this.ModuleData = moduleData;
         this.CurrentId = TreatmentLogADO.RoomId;
        }

        object ITreatmentLogList.Run()
        {
            try
            {
             return new frmTreatmentLogList(ModuleData, treatmentId, CurrentId);
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
