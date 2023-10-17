using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDepositReason
{
    partial class HisDepositReasonGet : BusinessBase
    {
        internal HisDepositReasonGet()
            : base()
        {

        }

        internal HisDepositReasonGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_DEPOSIT_REASON> Get(HisDepositReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepositReasonDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPOSIT_REASON GetById(long id)
        {
            try
            {
                return GetById(id, new HisDepositReasonFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_DEPOSIT_REASON GetById(long id, HisDepositReasonFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDepositReasonDAO.GetById(id, filter.Query());
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
