using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceSame
{
    partial class HisServiceSameGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_SAME> GetView(HisServiceSameViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceSameDAO.GetView(filter.Query(), param);
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
