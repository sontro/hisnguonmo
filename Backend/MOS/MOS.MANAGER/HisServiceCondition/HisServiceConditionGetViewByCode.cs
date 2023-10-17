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
        internal V_HIS_SERVICE_CONDITION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisServiceConditionViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_CONDITION GetViewByCode(string code, HisServiceConditionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceConditionDAO.GetViewByCode(code, filter.Query());
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
