using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    partial class HisServiceNumOrderGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_NUM_ORDER> GetView(HisServiceNumOrderViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceNumOrderDAO.GetView(filter.Query(), param);
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
