using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisCashierAddConfig
{
    public partial class HisCashierAddConfigManager : BusinessBase
    {
        public HisCashierAddConfigManager()
            : base()
        {

        }
        
        public HisCashierAddConfigManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CASHIER_ADD_CONFIG>> Get(HisCashierAddConfigFilterQuery filter)
        {
            ApiResultObject<List<HIS_CASHIER_ADD_CONFIG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CASHIER_ADD_CONFIG> resultData = null;
                if (valid)
                {
                    resultData = new HisCashierAddConfigGet(param).Get(filter);
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
        public ApiResultObject<HIS_CASHIER_ADD_CONFIG> Create(HIS_CASHIER_ADD_CONFIG data)
        {
            ApiResultObject<HIS_CASHIER_ADD_CONFIG> result = new ApiResultObject<HIS_CASHIER_ADD_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CASHIER_ADD_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCashierAddConfigCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_CASHIER_ADD_CONFIG>> CreateList(List<HIS_CASHIER_ADD_CONFIG> data)
        {
            ApiResultObject<List<HIS_CASHIER_ADD_CONFIG>> result = new ApiResultObject<List<HIS_CASHIER_ADD_CONFIG>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CASHIER_ADD_CONFIG> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCashierAddConfigCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_CASHIER_ADD_CONFIG> Update(HIS_CASHIER_ADD_CONFIG data)
        {
            ApiResultObject<HIS_CASHIER_ADD_CONFIG> result = new ApiResultObject<HIS_CASHIER_ADD_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CASHIER_ADD_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCashierAddConfigUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CASHIER_ADD_CONFIG> ChangeLock(long id)
        {
            ApiResultObject<HIS_CASHIER_ADD_CONFIG> result = new ApiResultObject<HIS_CASHIER_ADD_CONFIG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHIER_ADD_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCashierAddConfigLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CASHIER_ADD_CONFIG> Lock(long id)
        {
            ApiResultObject<HIS_CASHIER_ADD_CONFIG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHIER_ADD_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCashierAddConfigLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CASHIER_ADD_CONFIG> Unlock(long id)
        {
            ApiResultObject<HIS_CASHIER_ADD_CONFIG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CASHIER_ADD_CONFIG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCashierAddConfigLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCashierAddConfigTruncate(param).Truncate(id);
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
