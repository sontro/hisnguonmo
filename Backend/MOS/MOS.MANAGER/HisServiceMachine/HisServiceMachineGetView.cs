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
        internal List<V_HIS_SERVICE_MACHINE> GetView(HisServiceMachineViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisServiceMachineDAO.GetView(filter.Query(), param);
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
