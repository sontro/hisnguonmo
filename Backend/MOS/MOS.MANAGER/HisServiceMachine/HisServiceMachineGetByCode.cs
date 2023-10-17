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
        internal HIS_SERVICE_MACHINE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisServiceMachineFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_SERVICE_MACHINE GetByCode(string code, HisServiceMachineFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceMachineDAO.GetByCode(code, filter.Query());
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
