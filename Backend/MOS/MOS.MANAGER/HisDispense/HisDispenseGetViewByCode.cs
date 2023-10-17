using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDispense
{
    partial class HisDispenseGet : BusinessBase
    {
        internal V_HIS_DISPENSE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisDispenseViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DISPENSE GetViewByCode(string code, HisDispenseViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDispenseDAO.GetViewByCode(code, filter.Query());
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
