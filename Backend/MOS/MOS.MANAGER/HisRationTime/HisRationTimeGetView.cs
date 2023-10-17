using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    partial class HisRationTimeGet : BusinessBase
    {
        internal List<V_HIS_RATION_TIME> GetView(HisRationTimeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationTimeDAO.GetView(filter.Query(), param);
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
