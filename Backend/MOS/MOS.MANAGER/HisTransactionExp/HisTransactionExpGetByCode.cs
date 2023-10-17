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
        internal HIS_TRANSACTION_EXP GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisTransactionExpFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSACTION_EXP GetByCode(string code, HisTransactionExpFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionExpDAO.GetByCode(code, filter.Query());
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
