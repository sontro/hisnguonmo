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
        internal List<V_HIS_SERVICE_CONDITION> GetView(HisServiceConditionViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceConditionDAO.GetView(filter.Query(), param);
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
