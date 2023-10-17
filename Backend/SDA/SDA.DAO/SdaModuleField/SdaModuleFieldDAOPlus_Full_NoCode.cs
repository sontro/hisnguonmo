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
    }
}
