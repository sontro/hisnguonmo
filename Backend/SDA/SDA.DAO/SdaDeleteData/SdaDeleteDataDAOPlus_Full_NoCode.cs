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
    }
}
