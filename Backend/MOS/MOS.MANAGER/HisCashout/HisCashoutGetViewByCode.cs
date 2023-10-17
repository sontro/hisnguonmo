using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashout
{
    partial class HisCashoutGet : BusinessBase
    {
        internal V_HIS_CASHOUT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisCashoutViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_CASHOUT GetViewByCode(string code, HisCashoutViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashoutDAO.GetViewByCode(code, filter.Query());
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
