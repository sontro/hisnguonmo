using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHealthExamRank
{
    public partial class HisHealthExamRankManager : BusinessBase
    {
        public HisHealthExamRankManager()
            : base()
        {

        }
        
        public HisHealthExamRankManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HEALTH_EXAM_RANK>> Get(HisHealthExamRankFilterQuery filter)
        {
            ApiResultObject<List<HIS_HEALTH_EXAM_RANK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HEALTH_EXAM_RANK> resultData = null;
                if (valid)
                {
                    resultData = new HisHealthExamRankGet(param).Get(filter);
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
        public ApiResultObject<HIS_HEALTH_EXAM_RANK> Create(HIS_HEALTH_EXAM_RANK data)
        {
            ApiResultObject<HIS_HEALTH_EXAM_RANK> result = new ApiResultObject<HIS_HEALTH_EXAM_RANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEALTH_EXAM_RANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHealthExamRankCreate(param).Create(data);
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
        public ApiResultObject<HIS_HEALTH_EXAM_RANK> Update(HIS_HEALTH_EXAM_RANK data)
        {
            ApiResultObject<HIS_HEALTH_EXAM_RANK> result = new ApiResultObject<HIS_HEALTH_EXAM_RANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HEALTH_EXAM_RANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHealthExamRankUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HEALTH_EXAM_RANK> ChangeLock(long id)
        {
            ApiResultObject<HIS_HEALTH_EXAM_RANK> result = new ApiResultObject<HIS_HEALTH_EXAM_RANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HEALTH_EXAM_RANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHealthExamRankLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HEALTH_EXAM_RANK> Lock(long id)
        {
            ApiResultObject<HIS_HEALTH_EXAM_RANK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HEALTH_EXAM_RANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHealthExamRankLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HEALTH_EXAM_RANK> Unlock(long id)
        {
            ApiResultObject<HIS_HEALTH_EXAM_RANK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HEALTH_EXAM_RANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHealthExamRankLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHealthExamRankTruncate(param).Truncate(id);
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
