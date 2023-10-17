using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAlert
{
    partial class HisAlertGet : BusinessBase
    {
        internal V_HIS_ALERT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisAlertViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_ALERT GetViewByCode(string code, HisAlertViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAlertDAO.GetViewByCode(code, filter.Query());
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
