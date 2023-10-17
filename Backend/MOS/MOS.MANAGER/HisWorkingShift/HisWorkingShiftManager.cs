using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWorkingShift
{
    public partial class HisWorkingShiftManager : BusinessBase
    {
        public HisWorkingShiftManager()
            : base()
        {

        }
        
        public HisWorkingShiftManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_WORKING_SHIFT>> Get(HisWorkingShiftFilterQuery filter)
        {
            ApiResultObject<List<HIS_WORKING_SHIFT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_WORKING_SHIFT> resultData = null;
                if (valid)
                {
                    resultData = new HisWorkingShiftGet(param).Get(filter);
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
        public ApiResultObject<HIS_WORKING_SHIFT> Create(HIS_WORKING_SHIFT data)
        {
            ApiResultObject<HIS_WORKING_SHIFT> result = new ApiResultObject<HIS_WORKING_SHIFT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WORKING_SHIFT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisWorkingShiftCreate(param).Create(data);
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
        public ApiResultObject<HIS_WORKING_SHIFT> Update(HIS_WORKING_SHIFT data)
        {
            ApiResultObject<HIS_WORKING_SHIFT> result = new ApiResultObject<HIS_WORKING_SHIFT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WORKING_SHIFT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisWorkingShiftUpdate(param).Update(data);
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
        public ApiResultObject<HIS_WORKING_SHIFT> ChangeLock(long id)
        {
            ApiResultObject<HIS_WORKING_SHIFT> result = new ApiResultObject<HIS_WORKING_SHIFT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WORKING_SHIFT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWorkingShiftLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_WORKING_SHIFT> Lock(long id)
        {
            ApiResultObject<HIS_WORKING_SHIFT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WORKING_SHIFT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWorkingShiftLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_WORKING_SHIFT> Unlock(long id)
        {
            ApiResultObject<HIS_WORKING_SHIFT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WORKING_SHIFT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWorkingShiftLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisWorkingShiftTruncate(param).Truncate(id);
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
