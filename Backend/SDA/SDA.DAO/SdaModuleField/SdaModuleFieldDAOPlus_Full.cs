using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaModuleField
{
    public partial class SdaModuleFieldDAO : EntityBase
    {
        public List<V_SDA_MODULE_FIELD> GetView(SdaModuleFieldSO search, CommonParam param)
        {
            List<V_SDA_MODULE_FIELD> result = new List<V_SDA_MODULE_FIELD>();

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

        public SDA_MODULE_FIELD GetByCode(string code, SdaModuleFieldSO search)
        {
            SDA_MODULE_FIELD result = null;

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
        
        public V_SDA_MODULE_FIELD GetViewById(long id, SdaModuleFieldSO search)
        {
            V_SDA_MODULE_FIELD result = null;

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

        public V_SDA_MODULE_FIELD GetViewByCode(string code, SdaModuleFieldSO search)
        {
            V_SDA_MODULE_FIELD result = null;

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

        public Dictionary<string, SDA_MODULE_FIELD> GetDicByCode(SdaModuleFieldSO search, CommonParam param)
        {
            Dictionary<string, SDA_MODULE_FIELD> result = new Dictionary<string, SDA_MODULE_FIELD>();
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
