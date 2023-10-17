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
        internal HIS_EXE_SERVICE_MODULE GetByCode(string code)
        {
            try
            {
                return GetByCode(code, new HisExeServiceModuleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXE_SERVICE_MODULE GetByCode(string code, HisExeServiceModuleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExeServiceModuleDAO.GetByCode(code, filter.Query());
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
