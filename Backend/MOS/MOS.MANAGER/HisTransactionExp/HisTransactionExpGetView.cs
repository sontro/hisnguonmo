using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionExp
{
    partial class HisTransactionExpGet : BusinessBase
    {
        internal List<V_HIS_TRANSACTION_EXP> GetView(HisTransactionExpViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionExpDAO.GetView(filter.Query(), param);
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
