using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    partial class HisDataStoreGet : GetBase
    {
        internal List<V_HIS_DATA_STORE_1> GetView1(HisDataStoreView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDataStoreDAO.GetView1(filter.Query(), param);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DATA_STORE_1 GetView1ById(long id)
        {
            try
            {
                return GetView1ById(id, new HisDataStoreView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DATA_STORE_1 GetView1ById(long id, HisDataStoreView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDataStoreDAO.GetView1ById(id, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DATA_STORE_1 GetView1ByCode(string code)
        {
            try
            {
                return GetView1ByCode(code, new HisDataStoreView1FilterQuery());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal V_HIS_DATA_STORE_1 GetView1ByCode(string code, HisDataStoreView1FilterQuery filter)
        {
            try
            {
                return DAOWorker.HisDataStoreDAO.GetView1ByCode(code, filter.Query());
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                return null;
            }
        }

        internal List<V_HIS_DATA_STORE_1> GetView1Active()
        {
            HisDataStoreView1FilterQuery filter = new HisDataStoreView1FilterQuery();
            filter.IS_ACTIVE = IMSys.DbConfig.HIS_RS.COMMON.IS_ACTIVE__TRUE;
            return this.GetView1(filter);
        }
    }
}
