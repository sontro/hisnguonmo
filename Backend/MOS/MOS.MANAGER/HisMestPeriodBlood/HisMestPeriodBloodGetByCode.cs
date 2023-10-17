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
        internal HIS_MEST_PERIOD_BLOOD GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMestPeriodBloodFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MEST_PERIOD_BLOOD GetByCode(string code, HisMestPeriodBloodFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMestPeriodBloodDAO.GetByCode(code, filter.Query());
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
