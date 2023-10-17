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
        internal HIS_SPEED_UNIT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisSpeedUnitFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SPEED_UNIT GetByCode(string code, HisSpeedUnitFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisSpeedUnitDAO.GetByCode(code, filter.Query());
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
