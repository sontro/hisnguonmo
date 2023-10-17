using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSpeedUnit
{
    partial class HisSpeedUnitGet : BusinessBase
    {
        internal V_HIS_SPEED_UNIT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisSpeedUnitViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SPEED_UNIT GetViewByCode(string code, HisSpeedUnitViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpeedUnitDAO.GetViewByCode(code, filter.Query());
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
