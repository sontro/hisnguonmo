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
        internal List<V_HIS_EXP_MEST_REASON> GetView(HisExpMestReasonViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExpMestReasonDAO.GetView(filter.Query(), param);
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
