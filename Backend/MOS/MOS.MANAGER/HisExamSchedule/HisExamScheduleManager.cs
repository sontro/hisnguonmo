using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExamSchedule
{
    public partial class HisExamScheduleManager : BusinessBase
    {
        public HisExamScheduleManager()
            : base()
        {

        }
        
        public HisExamScheduleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXAM_SCHEDULE>> Get(HisExamScheduleFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXAM_SCHEDULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXAM_SCHEDULE> resultData = null;
                if (valid)
                {
                    resultData = new HisExamScheduleGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXAM_SCHEDULE> Create(HIS_EXAM_SCHEDULE data)
        {
            ApiResultObject<HIS_EXAM_SCHEDULE> result = new ApiResultObject<HIS_EXAM_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXAM_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExamScheduleCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXAM_SCHEDULE> Update(HIS_EXAM_SCHEDULE data)
        {
            ApiResultObject<HIS_EXAM_SCHEDULE> result = new ApiResultObject<HIS_EXAM_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXAM_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExamScheduleUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXAM_SCHEDULE> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXAM_SCHEDULE> result = new ApiResultObject<HIS_EXAM_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXAM_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExamScheduleLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXAM_SCHEDULE> Lock(long id)
        {
            ApiResultObject<HIS_EXAM_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXAM_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExamScheduleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXAM_SCHEDULE> Unlock(long id)
        {
            ApiResultObject<HIS_EXAM_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXAM_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExamScheduleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExamScheduleTruncate(param).Truncate(id);
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
