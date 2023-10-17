using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceFollow
{
    partial class HisServiceFollowGet : BusinessBase
    {
        internal List<V_HIS_SERVICE_FOLLOW> GetView(HisServiceFollowViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceFollowDAO.GetView(filter.Query(), param);
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
