using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEmployeeSchedule
{
    public partial class HisEmployeeScheduleManager : BusinessBase
    {
        public HisEmployeeScheduleManager()
            : base()
        {

        }
        
        public HisEmployeeScheduleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EMPLOYEE_SCHEDULE>> Get(HisEmployeeScheduleFilterQuery filter)
        {
            ApiResultObject<List<HIS_EMPLOYEE_SCHEDULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EMPLOYEE_SCHEDULE> resultData = null;
                if (valid)
                {
                    resultData = new HisEmployeeScheduleGet(param).Get(filter);
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
        public ApiResultObject<HIS_EMPLOYEE_SCHEDULE> Create(HIS_EMPLOYEE_SCHEDULE data)
        {
            ApiResultObject<HIS_EMPLOYEE_SCHEDULE> result = new ApiResultObject<HIS_EMPLOYEE_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMPLOYEE_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmployeeScheduleCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_EMPLOYEE_SCHEDULE>> CreateList(List<HIS_EMPLOYEE_SCHEDULE> data)
        {
            ApiResultObject<List<HIS_EMPLOYEE_SCHEDULE>> result = new ApiResultObject<List<HIS_EMPLOYEE_SCHEDULE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_EMPLOYEE_SCHEDULE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmployeeScheduleCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_EMPLOYEE_SCHEDULE> Update(HIS_EMPLOYEE_SCHEDULE data)
        {
            ApiResultObject<HIS_EMPLOYEE_SCHEDULE> result = new ApiResultObject<HIS_EMPLOYEE_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EMPLOYEE_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEmployeeScheduleUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EMPLOYEE_SCHEDULE> ChangeLock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE_SCHEDULE> result = new ApiResultObject<HIS_EMPLOYEE_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmployeeScheduleLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EMPLOYEE_SCHEDULE> Lock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmployeeScheduleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EMPLOYEE_SCHEDULE> Unlock(long id)
        {
            ApiResultObject<HIS_EMPLOYEE_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EMPLOYEE_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEmployeeScheduleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEmployeeScheduleTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> CheckSchedule()
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisEmployeeScheduleCheck(param).CheckSchedule();
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
