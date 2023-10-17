using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceMachine
{
    partial class HisServiceMachineGet : BusinessBase
    {
        internal V_HIS_SERVICE_MACHINE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisServiceMachineViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_SERVICE_MACHINE GetViewByCode(string code, HisServiceMachineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceMachineDAO.GetViewByCode(code, filter.Query());
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
