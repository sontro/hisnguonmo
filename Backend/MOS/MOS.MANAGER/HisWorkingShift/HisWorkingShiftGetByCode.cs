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
        internal HIS_WORKING_SHIFT GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisWorkingShiftFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_WORKING_SHIFT GetByCode(string code, HisWorkingShiftFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisWorkingShiftDAO.GetByCode(code, filter.Query());
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
