using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    partial class HisEkipPlanUserGet : BusinessBase
    {
        internal List<V_HIS_EKIP_PLAN_USER> GetView(HisEkipPlanUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanUserDAO.GetView(filter.Query(), param);
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
