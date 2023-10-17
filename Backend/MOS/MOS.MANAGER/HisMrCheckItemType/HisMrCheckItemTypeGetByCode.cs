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
        internal HIS_MR_CHECK_ITEM_TYPE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMrCheckItemTypeFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM_TYPE GetByCode(string code, HisMrCheckItemTypeFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemTypeDAO.GetByCode(code, filter.Query());
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
