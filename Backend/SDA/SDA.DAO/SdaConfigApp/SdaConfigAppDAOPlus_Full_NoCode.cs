using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaConfigApp
{
    public partial class SdaConfigAppDAO : EntityBase
    {
        public List<V_SDA_CONFIG_APP> GetView(SdaConfigAppSO search, CommonParam param)
        {
            List<V_SDA_CONFIG_APP> result = new List<V_SDA_CONFIG_APP>();
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

        public V_SDA_CONFIG_APP GetViewById(long id, SdaConfigAppSO search)
        {
            V_SDA_CONFIG_APP result = null;

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
