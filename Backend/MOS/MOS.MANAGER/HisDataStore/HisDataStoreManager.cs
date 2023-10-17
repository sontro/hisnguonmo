using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDataStore
{
    public partial class HisDataStoreManager : BusinessBase
    {
        public HisDataStoreManager()
            : base()
        {

        }
        
        public HisDataStoreManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DATA_STORE>> Get(HisDataStoreFilterQuery filter)
        {
            ApiResultObject<List<HIS_DATA_STORE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DATA_STORE> resultData = null;
                if (valid)
                {
                    resultData = new HisDataStoreGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<List<V_HIS_DATA_STORE>> GetView(HisDataStoreViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_DATA_STORE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_DATA_STORE> resultData = null;
                if (valid)
                {
                    resultData = new HisDataStoreGet(param).GetView(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HisDataStoreSDO> Create(HisDataStoreSDO data)
        {
            ApiResultObject<HisDataStoreSDO> result = new ApiResultObject<HisDataStoreSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDataStoreSDO resultData = null;
                if (valid && new HisDataStoreCreate(param).Create(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<HisDataStoreSDO> Update(HisDataStoreSDO data)
        {
            ApiResultObject<HisDataStoreSDO> result = new ApiResultObject<HisDataStoreSDO>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisDataStoreSDO resultData = null;
                if (valid && new HisDataStoreUpdate(param).Update(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<HIS_DATA_STORE> ChangeLock(HIS_DATA_STORE data)
        {
            ApiResultObject<HIS_DATA_STORE> result = new ApiResultObject<HIS_DATA_STORE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DATA_STORE resultData = null;
                if (valid && new HisDataStoreLock(param).ChangeLock(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_DATA_STORE data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisDataStoreTruncate(param).Truncate(data);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
    }
}
