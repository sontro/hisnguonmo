using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaProvinceMap
{
    public partial class SdaProvinceMapDAO : EntityBase
    {
        public List<V_SDA_PROVINCE_MAP> GetView(SdaProvinceMapSO search, CommonParam param)
        {
            List<V_SDA_PROVINCE_MAP> result = new List<V_SDA_PROVINCE_MAP>();
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

        public V_SDA_PROVINCE_MAP GetViewById(long id, SdaProvinceMapSO search)
        {
            V_SDA_PROVINCE_MAP result = null;

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
