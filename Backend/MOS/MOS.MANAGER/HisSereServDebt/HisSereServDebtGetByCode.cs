using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSereServDebt
{
    partial class HisSereServDebtGet : BusinessBase
    {
        internal HIS_SERE_SERV_DEBT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSereServDebtFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERE_SERV_DEBT GetByCode(string code, HisSereServDebtFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDebtDAO.GetByCode(code, filter.Query());
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
