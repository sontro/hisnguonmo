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

        public HIS_EXE_SERVICE_MODULE GetByCode(string code, HisExeServiceModuleSO search)
        {
            HIS_EXE_SERVICE_MODULE result = null;

            try
            {
                result = GetWorker.GetByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
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

        public V_HIS_EXE_SERVICE_MODULE GetViewByCode(string code, HisExeServiceModuleSO search)
        {
            V_HIS_EXE_SERVICE_MODULE result = null;

            try
            {
                result = GetWorker.GetViewByCode(code, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }
            return result;
        }

        public Dictionary<string, HIS_EXE_SERVICE_MODULE> GetDicByCode(HisExeServiceModuleSO search, CommonParam param)
        {
            Dictionary<string, HIS_EXE_SERVICE_MODULE> result = new Dictionary<string, HIS_EXE_SERVICE_MODULE>();
            try
            {
                result = GetWorker.GetDicByCode(search, param);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }

        public bool ExistsCode(string code, long? id)
        {
            try
            {
                return CheckWorker.ExistsCode(code, id);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                throw;
            }
        }
    }
}
