using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    partial class HisMestPeriodBloodGet : BusinessBase
    {
        internal V_HIS_MEST_PERIOD_BLOOD GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMestPeriodBloodViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MEST_PERIOD_BLOOD GetViewByCode(string code, HisMestPeriodBloodViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPeriodBloodDAO.GetViewByCode(code, filter.Query());
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
