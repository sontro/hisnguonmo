using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRejectAlert
{
    partial class HisRejectAlertGet : BusinessBase
    {
        internal V_HIS_REJECT_ALERT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRejectAlertViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_REJECT_ALERT GetViewByCode(string code, HisRejectAlertViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRejectAlertDAO.GetViewByCode(code, filter.Query());
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
