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
        internal V_HIS_MR_CHECK_ITEM_TYPE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisMrCheckItemTypeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_MR_CHECK_ITEM_TYPE GetViewByCode(string code, HisMrCheckItemTypeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemTypeDAO.GetViewByCode(code, filter.Query());
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
