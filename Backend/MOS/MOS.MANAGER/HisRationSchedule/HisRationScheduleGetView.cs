using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationSchedule
{
    partial class HisRationScheduleGet : BusinessBase
    {
        internal List<V_HIS_RATION_SCHEDULE> GetView(HisRationScheduleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationScheduleDAO.GetView(filter.Query(), param);
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
