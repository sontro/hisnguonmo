using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaDeleteData
{
    public partial class SdaDeleteDataDAO : EntityBase
    {
        public List<V_SDA_DELETE_DATA> GetView(SdaDeleteDataSO search, CommonParam param)
        {
            List<V_SDA_DELETE_DATA> result = new List<V_SDA_DELETE_DATA>();

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

        public SDA_DELETE_DATA GetByCode(string code, SdaDeleteDataSO search)
        {
            SDA_DELETE_DATA result = null;

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
        
        public V_SDA_DELETE_DATA GetViewById(long id, SdaDeleteDataSO search)
        {
            V_SDA_DELETE_DATA result = null;

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

        public V_SDA_DELETE_DATA GetViewByCode(string code, SdaDeleteDataSO search)
        {
            V_SDA_DELETE_DATA result = null;

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

        public Dictionary<string, SDA_DELETE_DATA> GetDicByCode(SdaDeleteDataSO search, CommonParam param)
        {
            Dictionary<string, SDA_DELETE_DATA> result = new Dictionary<string, SDA_DELETE_DATA>();
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
