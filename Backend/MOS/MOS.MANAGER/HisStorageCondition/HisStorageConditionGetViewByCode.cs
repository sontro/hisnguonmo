using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisStorageCondition
{
    partial class HisStorageConditionGet : BusinessBase
    {
        internal V_HIS_STORAGE_CONDITION GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisStorageConditionViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_STORAGE_CONDITION GetViewByCode(string code, HisStorageConditionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStorageConditionDAO.GetViewByCode(code, filter.Query());
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
