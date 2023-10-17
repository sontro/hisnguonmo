using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlan
{
    partial class HisEkipPlanGet : BusinessBase
    {
        internal List<V_HIS_EKIP_PLAN> GetView(HisEkipPlanViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanDAO.GetView(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }
    }
}
