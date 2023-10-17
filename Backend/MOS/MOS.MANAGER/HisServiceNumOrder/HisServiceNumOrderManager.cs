using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceNumOrder
{
    public partial class HisServiceNumOrderManager : BusinessBase
    {
        public HisServiceNumOrderManager()
            : base()
        {

        }
        
        public HisServiceNumOrderManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_NUM_ORDER>> Get(HisServiceNumOrderFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_NUM_ORDER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_NUM_ORDER> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceNumOrderGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_NUM_ORDER> Create(HIS_SERVICE_NUM_ORDER data)
        {
            ApiResultObject<HIS_SERVICE_NUM_ORDER> result = new ApiResultObject<HIS_SERVICE_NUM_ORDER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_NUM_ORDER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceNumOrderCreate(param).Create(data);
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
        public ApiResultObject<HIS_SERVICE_NUM_ORDER> Update(HIS_SERVICE_NUM_ORDER data)
        {
            ApiResultObject<HIS_SERVICE_NUM_ORDER> result = new ApiResultObject<HIS_SERVICE_NUM_ORDER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_NUM_ORDER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisServiceNumOrderUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERVICE_NUM_ORDER> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_NUM_ORDER> result = new ApiResultObject<HIS_SERVICE_NUM_ORDER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_NUM_ORDER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceNumOrderLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_NUM_ORDER> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_NUM_ORDER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_NUM_ORDER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceNumOrderLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_NUM_ORDER> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_NUM_ORDER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_NUM_ORDER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceNumOrderLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceNumOrderTruncate(param).Truncate(id);
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
