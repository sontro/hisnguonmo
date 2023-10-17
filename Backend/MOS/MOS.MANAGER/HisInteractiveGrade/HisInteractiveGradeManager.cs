using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInteractiveGrade
{
    public partial class HisInteractiveGradeManager : BusinessBase
    {
        public HisInteractiveGradeManager()
            : base()
        {

        }
        
        public HisInteractiveGradeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INTERACTIVE_GRADE>> Get(HisInteractiveGradeFilterQuery filter)
        {
            ApiResultObject<List<HIS_INTERACTIVE_GRADE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INTERACTIVE_GRADE> resultData = null;
                if (valid)
                {
                    resultData = new HisInteractiveGradeGet(param).Get(filter);
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
        public ApiResultObject<HIS_INTERACTIVE_GRADE> Create(HIS_INTERACTIVE_GRADE data)
        {
            ApiResultObject<HIS_INTERACTIVE_GRADE> result = new ApiResultObject<HIS_INTERACTIVE_GRADE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INTERACTIVE_GRADE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisInteractiveGradeCreate(param).Create(data);
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
        public ApiResultObject<HIS_INTERACTIVE_GRADE> Update(HIS_INTERACTIVE_GRADE data)
        {
            ApiResultObject<HIS_INTERACTIVE_GRADE> result = new ApiResultObject<HIS_INTERACTIVE_GRADE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INTERACTIVE_GRADE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisInteractiveGradeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_INTERACTIVE_GRADE> ChangeLock(long id)
        {
            ApiResultObject<HIS_INTERACTIVE_GRADE> result = new ApiResultObject<HIS_INTERACTIVE_GRADE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INTERACTIVE_GRADE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisInteractiveGradeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_INTERACTIVE_GRADE> Lock(long id)
        {
            ApiResultObject<HIS_INTERACTIVE_GRADE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INTERACTIVE_GRADE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisInteractiveGradeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_INTERACTIVE_GRADE> Unlock(long id)
        {
            ApiResultObject<HIS_INTERACTIVE_GRADE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INTERACTIVE_GRADE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisInteractiveGradeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisInteractiveGradeTruncate(param).Truncate(id);
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
