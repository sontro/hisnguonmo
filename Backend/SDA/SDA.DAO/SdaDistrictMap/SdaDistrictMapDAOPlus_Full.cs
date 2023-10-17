using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaDistrictMap
{
    public partial class SdaDistrictMapDAO : EntityBase
    {
        public List<V_SDA_DISTRICT_MAP> GetView(SdaDistrictMapSO search, CommonParam param)
        {
            List<V_SDA_DISTRICT_MAP> result = new List<V_SDA_DISTRICT_MAP>();

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

        public SDA_DISTRICT_MAP GetByCode(string code, SdaDistrictMapSO search)
        {
            SDA_DISTRICT_MAP result = null;

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
        
        public V_SDA_DISTRICT_MAP GetViewById(long id, SdaDistrictMapSO search)
        {
            V_SDA_DISTRICT_MAP result = null;

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

        public V_SDA_DISTRICT_MAP GetViewByCode(string code, SdaDistrictMapSO search)
        {
            V_SDA_DISTRICT_MAP result = null;

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

        public Dictionary<string, SDA_DISTRICT_MAP> GetDicByCode(SdaDistrictMapSO search, CommonParam param)
        {
            Dictionary<string, SDA_DISTRICT_MAP> result = new Dictionary<string, SDA_DISTRICT_MAP>();
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
