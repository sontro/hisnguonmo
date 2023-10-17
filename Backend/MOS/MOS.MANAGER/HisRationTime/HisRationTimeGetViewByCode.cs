using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationTime
{
    partial class HisRationTimeGet : BusinessBase
    {
        internal V_HIS_RATION_TIME GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisRationTimeViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_RATION_TIME GetViewByCode(string code, HisRationTimeViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisRationTimeDAO.GetViewByCode(code, filter.Query());
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
