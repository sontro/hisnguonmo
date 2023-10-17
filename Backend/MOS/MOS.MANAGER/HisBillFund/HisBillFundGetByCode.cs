using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBillFund
{
    partial class HisBillFundGet : BusinessBase
    {
        internal HIS_BILL_FUND GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisBillFundFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_BILL_FUND GetByCode(string code, HisBillFundFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBillFundDAO.GetByCode(code, filter.Query());
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
