using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoldReturn
{
    partial class HisHoldReturnGet : BusinessBase
    {
        internal List<V_HIS_HOLD_RETURN> GetView(HisHoldReturnViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisHoldReturnDAO.GetView(filter.Query(), param);
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
