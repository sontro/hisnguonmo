using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisLocationStore
{
    public partial class HisLocationStoreManager : BusinessBase
    {
        public HisLocationStoreManager()
            : base()
        {

        }
        
        public HisLocationStoreManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_LOCATION_STORE>> Get(HisLocationStoreFilterQuery filter)
        {
            ApiResultObject<List<HIS_LOCATION_STORE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_LOCATION_STORE> resultData = null;
                if (valid)
                {
                    resultData = new HisLocationStoreGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_LOCATION_STORE> Create(HIS_LOCATION_STORE data)
        {
            ApiResultObject<HIS_LOCATION_STORE> result = new ApiResultObject<HIS_LOCATION_STORE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_LOCATION_STORE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisLocationStoreCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_LOCATION_STORE> Update(HIS_LOCATION_STORE data)
        {
            ApiResultObject<HIS_LOCATION_STORE> result = new ApiResultObject<HIS_LOCATION_STORE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_LOCATION_STORE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisLocationStoreUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_LOCATION_STORE> ChangeLock(long id)
        {
            ApiResultObject<HIS_LOCATION_STORE> result = new ApiResultObject<HIS_LOCATION_STORE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LOCATION_STORE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLocationStoreLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_LOCATION_STORE> Lock(long id)
        {
            ApiResultObject<HIS_LOCATION_STORE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LOCATION_STORE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLocationStoreLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_LOCATION_STORE> Unlock(long id)
        {
            ApiResultObject<HIS_LOCATION_STORE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_LOCATION_STORE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisLocationStoreLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

		[Logger]
        public ApiResultObject<bool> Delete(long id)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisLocationStoreTruncate(param).Truncate(id);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
    }
}
