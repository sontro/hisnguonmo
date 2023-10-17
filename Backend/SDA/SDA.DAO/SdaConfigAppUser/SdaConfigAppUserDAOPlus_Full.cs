using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfigAppUser
{
    public partial class SdaConfigAppUserDAO : EntityBase
    {
        public List<V_SDA_CONFIG_APP_USER> GetView(SdaConfigAppUserSO search, CommonParam param)
        {
            List<V_SDA_CONFIG_APP_USER> result = new List<V_SDA_CONFIG_APP_USER>();

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

        public SDA_CONFIG_APP_USER GetByCode(string code, SdaConfigAppUserSO search)
        {
            SDA_CONFIG_APP_USER result = null;

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
        
        public V_SDA_CONFIG_APP_USER GetViewById(long id, SdaConfigAppUserSO search)
        {
            V_SDA_CONFIG_APP_USER result = null;

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

        public V_SDA_CONFIG_APP_USER GetViewByCode(string code, SdaConfigAppUserSO search)
        {
            V_SDA_CONFIG_APP_USER result = null;

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

        public Dictionary<string, SDA_CONFIG_APP_USER> GetDicByCode(SdaConfigAppUserSO search, CommonParam param)
        {
            Dictionary<string, SDA_CONFIG_APP_USER> result = new Dictionary<string, SDA_CONFIG_APP_USER>();
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
