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
    }
}
