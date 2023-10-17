using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceCondition
{
    partial class HisServiceConditionGet : BusinessBase
    {
        internal HIS_SERVICE_CONDITION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisServiceConditionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_CONDITION GetByCode(string code, HisServiceConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceConditionDAO.GetByCode(code, filter.Query());
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
