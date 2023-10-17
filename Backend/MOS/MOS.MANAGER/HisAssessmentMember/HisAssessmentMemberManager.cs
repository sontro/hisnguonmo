using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentMember
{
    public partial class HisAssessmentMemberManager : BusinessBase
    {
        public HisAssessmentMemberManager()
            : base()
        {

        }
        
        public HisAssessmentMemberManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ASSESSMENT_MEMBER>> Get(HisAssessmentMemberFilterQuery filter)
        {
            ApiResultObject<List<HIS_ASSESSMENT_MEMBER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ASSESSMENT_MEMBER> resultData = null;
                if (valid)
                {
                    resultData = new HisAssessmentMemberGet(param).Get(filter);
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
        public ApiResultObject<HIS_ASSESSMENT_MEMBER> Create(HIS_ASSESSMENT_MEMBER data)
        {
            ApiResultObject<HIS_ASSESSMENT_MEMBER> result = new ApiResultObject<HIS_ASSESSMENT_MEMBER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_MEMBER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAssessmentMemberCreate(param).Create(data);
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
        public ApiResultObject<HIS_ASSESSMENT_MEMBER> Update(HIS_ASSESSMENT_MEMBER data)
        {
            ApiResultObject<HIS_ASSESSMENT_MEMBER> result = new ApiResultObject<HIS_ASSESSMENT_MEMBER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_MEMBER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAssessmentMemberUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ASSESSMENT_MEMBER> ChangeLock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_MEMBER> result = new ApiResultObject<HIS_ASSESSMENT_MEMBER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_MEMBER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentMemberLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ASSESSMENT_MEMBER> Lock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_MEMBER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_MEMBER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentMemberLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ASSESSMENT_MEMBER> Unlock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_MEMBER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_MEMBER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentMemberLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAssessmentMemberTruncate(param).Truncate(id);
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
