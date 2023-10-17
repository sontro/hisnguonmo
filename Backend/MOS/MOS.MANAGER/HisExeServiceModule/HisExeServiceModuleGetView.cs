using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExeServiceModule
{
    partial class HisExeServiceModuleGet : BusinessBase
    {
        internal List<V_HIS_EXE_SERVICE_MODULE> GetView(HisExeServiceModuleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExeServiceModuleDAO.GetView(filter.Query(), param);
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
