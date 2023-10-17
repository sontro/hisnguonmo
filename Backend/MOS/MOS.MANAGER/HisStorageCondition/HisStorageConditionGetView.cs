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
        internal List<V_HIS_STORAGE_CONDITION> GetView(HisStorageConditionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisStorageConditionDAO.GetView(filter.Query(), param);
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
