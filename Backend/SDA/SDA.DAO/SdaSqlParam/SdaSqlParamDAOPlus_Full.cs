using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaSqlParam
{
    public partial class SdaSqlParamDAO : EntityBase
    {
        public List<V_SDA_SQL_PARAM> GetView(SdaSqlParamSO search, CommonParam param)
        {
            List<V_SDA_SQL_PARAM> result = new List<V_SDA_SQL_PARAM>();

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

        public SDA_SQL_PARAM GetByCode(string code, SdaSqlParamSO search)
        {
            SDA_SQL_PARAM result = null;

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
        
        public V_SDA_SQL_PARAM GetViewById(long id, SdaSqlParamSO search)
        {
            V_SDA_SQL_PARAM result = null;

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

        public V_SDA_SQL_PARAM GetViewByCode(string code, SdaSqlParamSO search)
        {
            V_SDA_SQL_PARAM result = null;

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

        public Dictionary<string, SDA_SQL_PARAM> GetDicByCode(SdaSqlParamSO search, CommonParam param)
        {
            Dictionary<string, SDA_SQL_PARAM> result = new Dictionary<string, SDA_SQL_PARAM>();
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
