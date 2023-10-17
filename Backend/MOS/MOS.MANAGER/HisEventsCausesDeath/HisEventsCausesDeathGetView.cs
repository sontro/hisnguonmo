using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    partial class HisEventsCausesDeathGet : BusinessBase
    {
        internal List<V_HIS_EVENTS_CAUSES_DEATH> GetView(HisEventsCausesDeathViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisEventsCausesDeathDAO.GetView(filter.Query(), param);
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
