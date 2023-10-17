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
        internal HIS_CASHOUT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisCashoutFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_CASHOUT GetByCode(string code, HisCashoutFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisCashoutDAO.GetByCode(code, filter.Query());
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
