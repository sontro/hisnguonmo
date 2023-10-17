using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrCheckItemType
{
    partial class HisMrCheckItemTypeGet : BusinessBase
    {
        internal List<V_HIS_MR_CHECK_ITEM_TYPE> GetView(HisMrCheckItemTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemTypeDAO.GetView(filter.Query(), param);
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
