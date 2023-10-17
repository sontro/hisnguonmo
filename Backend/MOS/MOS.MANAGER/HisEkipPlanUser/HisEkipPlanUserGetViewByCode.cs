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
        internal V_HIS_EKIP_PLAN_USER GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisEkipPlanUserViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EKIP_PLAN_USER GetViewByCode(string code, HisEkipPlanUserViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanUserDAO.GetViewByCode(code, filter.Query());
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
