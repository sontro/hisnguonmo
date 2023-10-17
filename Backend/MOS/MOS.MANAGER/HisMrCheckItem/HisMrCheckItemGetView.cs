using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItem
{
    partial class HisMrCheckItemGet : BusinessBase
    {
        internal List<V_HIS_MR_CHECK_ITEM> GetView(HisMrCheckItemViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemDAO.GetView(filter.Query(), param);
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
