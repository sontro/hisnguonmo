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
        internal HIS_REJECT_ALERT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisRejectAlertFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_REJECT_ALERT GetByCode(string code, HisRejectAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRejectAlertDAO.GetByCode(code, filter.Query());
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
