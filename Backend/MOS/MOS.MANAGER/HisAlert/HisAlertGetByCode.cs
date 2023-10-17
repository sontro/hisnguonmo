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
        internal HIS_ALERT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisAlertFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_ALERT GetByCode(string code, HisAlertFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisAlertDAO.GetByCode(code, filter.Query());
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
