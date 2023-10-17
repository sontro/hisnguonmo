using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    partial class HisWorkingShiftGet : BusinessBase
    {
        internal V_HIS_WORKING_SHIFT GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisWorkingShiftViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_WORKING_SHIFT GetViewByCode(string code, HisWorkingShiftViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWorkingShiftDAO.GetViewByCode(code, filter.Query());
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
