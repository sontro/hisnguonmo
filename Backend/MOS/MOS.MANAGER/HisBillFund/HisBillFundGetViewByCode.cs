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
        internal V_HIS_BILL_FUND GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisBillFundViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_BILL_FUND GetViewByCode(string code, HisBillFundViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisBillFundDAO.GetViewByCode(code, filter.Query());
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
