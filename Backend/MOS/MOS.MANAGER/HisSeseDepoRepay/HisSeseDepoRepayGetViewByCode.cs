using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayGet : BusinessBase
    {
        internal V_HIS_SESE_DEPO_REPAY GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSeseDepoRepayViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SESE_DEPO_REPAY GetViewByCode(string code, HisSeseDepoRepayViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseDepoRepayDAO.GetViewByCode(code, filter.Query());
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
