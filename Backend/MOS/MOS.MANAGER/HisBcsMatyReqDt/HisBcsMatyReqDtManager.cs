using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBcsMatyReqDt
{
    public partial class HisBcsMatyReqDtManager : BusinessBase
    {
        public HisBcsMatyReqDtManager()
            : base()
        {

        }
        
        public HisBcsMatyReqDtManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BCS_MATY_REQ_DT>> Get(HisBcsMatyReqDtFilterQuery filter)
        {
            ApiResultObject<List<HIS_BCS_MATY_REQ_DT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BCS_MATY_REQ_DT> resultData = null;
                if (valid)
                {
                    resultData = new HisBcsMatyReqDtGet(param).Get(filter);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_DT> Create(HIS_BCS_MATY_REQ_DT data)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_DT> result = new ApiResultObject<HIS_BCS_MATY_REQ_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_MATY_REQ_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMatyReqDtCreate(param).Create(data);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_DT> Update(HIS_BCS_MATY_REQ_DT data)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_DT> result = new ApiResultObject<HIS_BCS_MATY_REQ_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BCS_MATY_REQ_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBcsMatyReqDtUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_DT> ChangeLock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_DT> result = new ApiResultObject<HIS_BCS_MATY_REQ_DT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqDtLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_DT> Lock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqDtLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BCS_MATY_REQ_DT> Unlock(long id)
        {
            ApiResultObject<HIS_BCS_MATY_REQ_DT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BCS_MATY_REQ_DT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBcsMatyReqDtLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBcsMatyReqDtTruncate(param).Truncate(id);
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
