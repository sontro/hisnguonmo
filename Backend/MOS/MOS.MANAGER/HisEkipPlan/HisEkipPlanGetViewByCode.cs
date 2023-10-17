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
        internal V_HIS_EKIP_PLAN GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEkipPlanViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EKIP_PLAN GetViewByCode(string code, HisEkipPlanViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanDAO.GetViewByCode(code, filter.Query());
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
