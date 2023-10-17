using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEventsCausesDeath
{
    public partial class HisEventsCausesDeathManager : BusinessBase
    {
        public HisEventsCausesDeathManager()
            : base()
        {

        }
        
        public HisEventsCausesDeathManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EVENTS_CAUSES_DEATH>> Get(HisEventsCausesDeathFilterQuery filter)
        {
            ApiResultObject<List<HIS_EVENTS_CAUSES_DEATH>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EVENTS_CAUSES_DEATH> resultData = null;
                if (valid)
                {
                    resultData = new HisEventsCausesDeathGet(param).Get(filter);
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
        public ApiResultObject<HIS_EVENTS_CAUSES_DEATH> Create(HIS_EVENTS_CAUSES_DEATH data)
        {
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EVENTS_CAUSES_DEATH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEventsCausesDeathCreate(param).Create(data);
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
        public ApiResultObject<HIS_EVENTS_CAUSES_DEATH> Update(HIS_EVENTS_CAUSES_DEATH data)
        {
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EVENTS_CAUSES_DEATH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEventsCausesDeathUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EVENTS_CAUSES_DEATH> ChangeLock(long id)
        {
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = new ApiResultObject<HIS_EVENTS_CAUSES_DEATH>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EVENTS_CAUSES_DEATH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEventsCausesDeathLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EVENTS_CAUSES_DEATH> Lock(long id)
        {
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EVENTS_CAUSES_DEATH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEventsCausesDeathLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EVENTS_CAUSES_DEATH> Unlock(long id)
        {
            ApiResultObject<HIS_EVENTS_CAUSES_DEATH> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EVENTS_CAUSES_DEATH resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEventsCausesDeathLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEventsCausesDeathTruncate(param).Truncate(id);
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
