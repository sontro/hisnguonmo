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
        internal V_HIS_MR_CHECK_ITEM GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMrCheckItemViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MR_CHECK_ITEM GetViewByCode(string code, HisMrCheckItemViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemDAO.GetViewByCode(code, filter.Query());
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
