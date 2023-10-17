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
        internal HisExeServiceModuleGet()
            : base()
        {

        }

        internal HisExeServiceModuleGet(CommonParam paramGet)
            : base(paramGet)
        {

        }

        internal List<HIS_EXE_SERVICE_MODULE> Get(HisExeServiceModuleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExeServiceModuleDAO.Get(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXE_SERVICE_MODULE GetById(long id)
        {
            try
            {
                return GetById(id, new HisExeServiceModuleFilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal HIS_EXE_SERVICE_MODULE GetById(long id, HisExeServiceModuleFilterQuery filter)
        {
            try
            {
                return DAOWorker.HisExeServiceModuleDAO.GetById(id, filter.Query());
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
