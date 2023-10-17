using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNumOrderIssue
{
    public partial class HisNumOrderIssueManager : BusinessBase
    {
        public HisNumOrderIssueManager()
            : base()
        {

        }
        
        public HisNumOrderIssueManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_NUM_ORDER_ISSUE>> Get(HisNumOrderIssueFilterQuery filter)
        {
            ApiResultObject<List<HIS_NUM_ORDER_ISSUE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_NUM_ORDER_ISSUE> resultData = null;
                if (valid)
                {
                    resultData = new HisNumOrderIssueGet(param).Get(filter);
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
        public ApiResultObject<HIS_NUM_ORDER_ISSUE> Create(HIS_NUM_ORDER_ISSUE data)
        {
            ApiResultObject<HIS_NUM_ORDER_ISSUE> result = new ApiResultObject<HIS_NUM_ORDER_ISSUE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_ISSUE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNumOrderIssueCreate(param).Create(data);
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
        public ApiResultObject<HIS_NUM_ORDER_ISSUE> Update(HIS_NUM_ORDER_ISSUE data)
        {
            ApiResultObject<HIS_NUM_ORDER_ISSUE> result = new ApiResultObject<HIS_NUM_ORDER_ISSUE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NUM_ORDER_ISSUE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNumOrderIssueUpdate(param).Update(data);
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
        public ApiResultObject<HIS_NUM_ORDER_ISSUE> ChangeLock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_ISSUE> result = new ApiResultObject<HIS_NUM_ORDER_ISSUE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_ISSUE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderIssueLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_NUM_ORDER_ISSUE> Lock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_ISSUE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_ISSUE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderIssueLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_NUM_ORDER_ISSUE> Unlock(long id)
        {
            ApiResultObject<HIS_NUM_ORDER_ISSUE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NUM_ORDER_ISSUE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNumOrderIssueLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisNumOrderIssueTruncate(param).Truncate(id);
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
