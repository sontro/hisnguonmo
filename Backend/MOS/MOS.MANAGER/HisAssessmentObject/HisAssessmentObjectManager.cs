using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAssessmentObject
{
    public partial class HisAssessmentObjectManager : BusinessBase
    {
        public HisAssessmentObjectManager()
            : base()
        {

        }
        
        public HisAssessmentObjectManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ASSESSMENT_OBJECT>> Get(HisAssessmentObjectFilterQuery filter)
        {
            ApiResultObject<List<HIS_ASSESSMENT_OBJECT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ASSESSMENT_OBJECT> resultData = null;
                if (valid)
                {
                    resultData = new HisAssessmentObjectGet(param).Get(filter);
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
        public ApiResultObject<HIS_ASSESSMENT_OBJECT> Create(HIS_ASSESSMENT_OBJECT data)
        {
            ApiResultObject<HIS_ASSESSMENT_OBJECT> result = new ApiResultObject<HIS_ASSESSMENT_OBJECT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_OBJECT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAssessmentObjectCreate(param).Create(data);
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
        public ApiResultObject<HIS_ASSESSMENT_OBJECT> Update(HIS_ASSESSMENT_OBJECT data)
        {
            ApiResultObject<HIS_ASSESSMENT_OBJECT> result = new ApiResultObject<HIS_ASSESSMENT_OBJECT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ASSESSMENT_OBJECT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAssessmentObjectUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ASSESSMENT_OBJECT> ChangeLock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_OBJECT> result = new ApiResultObject<HIS_ASSESSMENT_OBJECT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_OBJECT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentObjectLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ASSESSMENT_OBJECT> Lock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_OBJECT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_OBJECT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentObjectLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ASSESSMENT_OBJECT> Unlock(long id)
        {
            ApiResultObject<HIS_ASSESSMENT_OBJECT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ASSESSMENT_OBJECT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAssessmentObjectLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAssessmentObjectTruncate(param).Truncate(id);
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
