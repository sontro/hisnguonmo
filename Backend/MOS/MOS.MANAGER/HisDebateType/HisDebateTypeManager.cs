using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateType
{
    public partial class HisDebateTypeManager : BusinessBase
    {
        public HisDebateTypeManager()
            : base()
        {

        }
        
        public HisDebateTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBATE_TYPE>> Get(HisDebateTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBATE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBATE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBATE_TYPE> Create(HIS_DEBATE_TYPE data)
        {
            ApiResultObject<HIS_DEBATE_TYPE> result = new ApiResultObject<HIS_DEBATE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateTypeCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBATE_TYPE> Update(HIS_DEBATE_TYPE data)
        {
            ApiResultObject<HIS_DEBATE_TYPE> result = new ApiResultObject<HIS_DEBATE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateTypeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBATE_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBATE_TYPE> result = new ApiResultObject<HIS_DEBATE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_DEBATE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_DEBATE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebateTypeTruncate(param).Truncate(id);
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
