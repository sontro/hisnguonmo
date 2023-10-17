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
        internal V_HIS_EXE_SERVICE_MODULE GetViewByCode(string code)
        {
            try
            {
                return GetViewByCode(code, new HisExeServiceModuleViewFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_EXE_SERVICE_MODULE GetViewByCode(string code, HisExeServiceModuleViewFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExeServiceModuleDAO.GetViewByCode(code, filter.Query());
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
