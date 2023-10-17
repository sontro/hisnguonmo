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
        internal V_HIS_SERE_SERV_DEBT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSereServDebtViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERE_SERV_DEBT GetViewByCode(string code, HisSereServDebtViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSereServDebtDAO.GetViewByCode(code, filter.Query());
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
