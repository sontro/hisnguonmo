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
        internal HIS_EKIP_PLAN GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisEkipPlanFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EKIP_PLAN GetByCode(string code, HisEkipPlanFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEkipPlanDAO.GetByCode(code, filter.Query());
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
