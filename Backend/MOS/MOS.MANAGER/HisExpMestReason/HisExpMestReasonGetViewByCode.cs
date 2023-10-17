using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestReason
{
    partial class HisExpMestReasonGet : BusinessBase
    {
        internal V_HIS_EXP_MEST_REASON GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExpMestReasonViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXP_MEST_REASON GetViewByCode(string code, HisExpMestReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestReasonDAO.GetViewByCode(code, filter.Query());
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
