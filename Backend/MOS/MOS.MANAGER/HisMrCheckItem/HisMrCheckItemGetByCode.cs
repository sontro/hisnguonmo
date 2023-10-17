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
        internal HIS_MR_CHECK_ITEM GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisMrCheckItemFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_MR_CHECK_ITEM GetByCode(string code, HisMrCheckItemFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisMrCheckItemDAO.GetByCode(code, filter.Query());
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
