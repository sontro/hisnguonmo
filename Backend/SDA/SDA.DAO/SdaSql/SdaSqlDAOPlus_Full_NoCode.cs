using SDA.DAO.StagingObject;
using SDA.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace SDA.DAO.SdaSql
{
    public partial class SdaSqlDAO : EntityBase
    {
        public List<V_SDA_SQL> GetView(SdaSqlSO search, CommonParam param)
        {
            List<V_SDA_SQL> result = new List<V_SDA_SQL>();
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

        public V_SDA_SQL GetViewById(long id, SdaSqlSO search)
        {
            V_SDA_SQL result = null;

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
