using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisRationSchedule
{
    public partial class HisRationScheduleManager : BusinessBase
    {
        public HisRationScheduleManager()
            : base()
        {

        }
        
        public HisRationScheduleManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_RATION_SCHEDULE>> Get(HisRationScheduleFilterQuery filter)
        {
            ApiResultObject<List<HIS_RATION_SCHEDULE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RATION_SCHEDULE> resultData = null;
                if (valid)
                {
                    resultData = new HisRationScheduleGet(param).Get(filter);
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
        public ApiResultObject<HIS_RATION_SCHEDULE> Create(RationScheduleSDO sdo)
        {
            ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_RATION_SCHEDULE resultData = null;
                if (valid)
                {
                    isSuccess = new HisRationScheduleCreate(param).Create(sdo, ref resultData);
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
        public ApiResultObject<HIS_RATION_SCHEDULE> Update(RationScheduleSDO sdo)
        {
            ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                HIS_RATION_SCHEDULE resultData = null;
                if (valid)
                {
                    isSuccess = new HisRationScheduleUpdate(param).Update(sdo, ref resultData);
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
        public ApiResultObject<HIS_RATION_SCHEDULE> ChangeLock(long id)
        {
            ApiResultObject<HIS_RATION_SCHEDULE> result = new ApiResultObject<HIS_RATION_SCHEDULE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationScheduleLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SCHEDULE> Lock(long id)
        {
            ApiResultObject<HIS_RATION_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationScheduleLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SCHEDULE> Unlock(long id)
        {
            ApiResultObject<HIS_RATION_SCHEDULE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SCHEDULE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationScheduleLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRationScheduleTruncate(param).Truncate(id);
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
        public ApiResultObject<List<V_HIS_SERVICE_REQ_10>> Execute(RationScheduleExecuteSDO sdo)
        {
            ApiResultObject<List<V_HIS_SERVICE_REQ_10>> result = new ApiResultObject<List<V_HIS_SERVICE_REQ_10>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool isSuccess = false;
                List<V_HIS_SERVICE_REQ_10> resultData = null;
                if (valid)
                {
                    isSuccess = new HisRationScheduleExecute(param).Run(sdo, ref resultData);
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

    }
}
