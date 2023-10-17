using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisExeServiceModule
{
    public partial class HisExeServiceModuleDAO : EntityBase
    {
        public List<V_HIS_EXE_SERVICE_MODULE> GetView(HisExeServiceModuleSO search, CommonParam param)
        {
            List<V_HIS_EXE_SERVICE_MODULE> result = new List<V_HIS_EXE_SERVICE_MODULE>();
            try
            {
                result = GetWorker.GetView(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }
            return result;
        }

        public V_HIS_EXE_SERVICE_MODULE GetViewById(long id, HisExeServiceModuleSO search)
        {
            V_HIS_EXE_SERVICE_MODULE result = null;

            try
            {
                result = GetWorker.GetViewById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }
    }
}
