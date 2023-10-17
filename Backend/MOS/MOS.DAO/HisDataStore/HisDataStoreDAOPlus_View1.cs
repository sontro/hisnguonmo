using MOS.DAO.StagingObject;
using MOS.EFMODEL.DataModels;
using Inventec.Core;
using System;
using System.Collections.Generic;

namespace MOS.DAO.HisDataStore
{
    public partial class HisDataStoreDAO : EntityBase
    {
        public List<V_HIS_DATA_STORE_1> GetView1(HisDataStoreSO search, CommonParam param)
        {
            List<V_HIS_DATA_STORE_1> result = new List<V_HIS_DATA_STORE_1>();

            try
            {
                result = GetWorker.GetView1(search, param);
            }
            catch (Exception ex)
            {
                param.HasException = true;
                Inventec.Common.Logging.LogSystem.Error(ex);
                result.Clear();
            }

            return result;
        }
        
        public V_HIS_DATA_STORE_1 GetView1ById(long id, HisDataStoreSO search)
        {
            V_HIS_DATA_STORE_1 result = null;

            try
            {
                result = GetWorker.GetView1ById(id, search);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                result = null;
            }

            return result;
        }

        public V_HIS_DATA_STORE_1 GetView1ByCode(string code, HisDataStoreSO search)
        {
            V_HIS_DATA_STORE_1 result = null;

            try
            {
                result = GetWorker.GetView1ByCode(code, search);
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
