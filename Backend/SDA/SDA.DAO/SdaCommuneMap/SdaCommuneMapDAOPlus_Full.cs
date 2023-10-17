using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaCommuneMap
{
    public partial class SdaCommuneMapDAO : EntityBase
    {
        public List<V_SDA_COMMUNE_MAP> GetView(SdaCommuneMapSO search, CommonParam param)
        {
            List<V_SDA_COMMUNE_MAP> result = new List<V_SDA_COMMUNE_MAP>();

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

        public SDA_COMMUNE_MAP GetByCode(string code, SdaCommuneMapSO search)
        {
            SDA_COMMUNE_MAP result = null;

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
        
        public V_SDA_COMMUNE_MAP GetViewById(long id, SdaCommuneMapSO search)
        {
            V_SDA_COMMUNE_MAP result = null;

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

        public V_SDA_COMMUNE_MAP GetViewByCode(string code, SdaCommuneMapSO search)
        {
            V_SDA_COMMUNE_MAP result = null;

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

        public Dictionary<string, SDA_COMMUNE_MAP> GetDicByCode(SdaCommuneMapSO search, CommonParam param)
        {
            Dictionary<string, SDA_COMMUNE_MAP> result = new Dictionary<string, SDA_COMMUNE_MAP>();
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
