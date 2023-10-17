using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    partial class HisSeseDepoRepayGet : BusinessBase
    {
        internal HIS_SESE_DEPO_REPAY GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSeseDepoRepayFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SESE_DEPO_REPAY GetByCode(string code, HisSeseDepoRepayFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSeseDepoRepayDAO.GetByCode(code, filter.Query());
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
