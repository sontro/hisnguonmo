using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    partial class HisSpeedUnitGet : BusinessBase
    {
        internal List<V_HIS_SPEED_UNIT> GetView(HisSpeedUnitViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpeedUnitDAO.GetView(filter.Query(), param);
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
