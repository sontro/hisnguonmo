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
        internal HIS_STORAGE_CONDITION GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisStorageConditionFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_STORAGE_CONDITION GetByCode(string code, HisStorageConditionFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStorageConditionDAO.GetByCode(code, filter.Query());
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
