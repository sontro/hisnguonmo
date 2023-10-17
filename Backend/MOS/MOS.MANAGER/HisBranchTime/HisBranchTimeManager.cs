using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBranchTime
{
    public partial class HisBranchTimeManager : BusinessBase
    {
        public HisBranchTimeManager()
            : base()
        {

        }
        
        public HisBranchTimeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BRANCH_TIME>> Get(HisBranchTimeFilterQuery filter)
        {
            ApiResultObject<List<HIS_BRANCH_TIME>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BRANCH_TIME> resultData = null;
                if (valid)
                {
                    resultData = new HisBranchTimeGet(param).Get(filter);
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
        public ApiResultObject<HIS_BRANCH_TIME> Create(HIS_BRANCH_TIME data)
        {
            ApiResultObject<HIS_BRANCH_TIME> result = new ApiResultObject<HIS_BRANCH_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBranchTimeCreate(param).Create(data);
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
        public ApiResultObject<HIS_BRANCH_TIME> Update(HIS_BRANCH_TIME data)
        {
            ApiResultObject<HIS_BRANCH_TIME> result = new ApiResultObject<HIS_BRANCH_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BRANCH_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBranchTimeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BRANCH_TIME> ChangeLock(long id)
        {
            ApiResultObject<HIS_BRANCH_TIME> result = new ApiResultObject<HIS_BRANCH_TIME>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BRANCH_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBranchTimeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BRANCH_TIME> Lock(long id)
        {
            ApiResultObject<HIS_BRANCH_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BRANCH_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBranchTimeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BRANCH_TIME> Unlock(long id)
        {
            ApiResultObject<HIS_BRANCH_TIME> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BRANCH_TIME resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBranchTimeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBranchTimeTruncate(param).Truncate(id);
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

        [Logger]
        public ApiResultObject<List<HIS_BRANCH_TIME>> CreateList(List<HIS_BRANCH_TIME> listData)
        {
            ApiResultObject<List<HIS_BRANCH_TIME>> result = new ApiResultObject<List<HIS_BRANCH_TIME>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_BRANCH_TIME> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBranchTimeCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<List<HIS_BRANCH_TIME>> UpdateList(List<HIS_BRANCH_TIME> listData)
        {
            ApiResultObject<List<HIS_BRANCH_TIME>> result = new ApiResultObject<List<HIS_BRANCH_TIME>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_BRANCH_TIME> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBranchTimeUpdate(param).UpdateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisBranchTimeTruncate(param).TruncateList(ids);
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
