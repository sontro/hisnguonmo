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
        internal HIS_EKIP_PLAN_USER GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEkipPlanUserFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_PLAN_USER GetByCode(string code, HisEkipPlanUserFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanUserDAO.GetByCode(code, filter.Query());
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
