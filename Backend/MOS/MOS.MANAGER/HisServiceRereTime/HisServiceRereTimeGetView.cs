using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceRereTime
{
    partial class HisServiceRereTimeGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_RERE_TIME> GetView(HisServiceRereTimeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceRereTimeDAO.GetView(filter.Query(), param);
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
