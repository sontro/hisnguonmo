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
        internal HisTransactionExpGet()
            : base()
        {

        }

        internal HisTransactionExpGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_TRANSACTION_EXP> Get(HisTransactionExpFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionExpDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSACTION_EXP GetById(long id)
        {
            try
            {
                return GetById(id, new HisTransactionExpFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_TRANSACTION_EXP GetById(long id, HisTransactionExpFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisTransactionExpDAO.GetById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<HIS_TRANSACTION_EXP> GetByTransactionId(long transactionId)
        {
            HisTransactionExpFilterQuery filter = new HisTransactionExpFilterQuery();
            filter.TRANSACTION_ID = transactionId;
            return this.Get(filter);
        }
    }
}
