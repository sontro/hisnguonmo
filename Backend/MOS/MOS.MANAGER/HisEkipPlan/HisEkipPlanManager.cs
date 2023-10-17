using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlan
{
    public partial class HisEkipPlanManager : BusinessBase
    {
        public HisEkipPlanManager()
            : base()
        {

        }
        
        public HisEkipPlanManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EKIP_PLAN>> Get(HisEkipPlanFilterQuery filter)
        {
            ApiResultObject<List<HIS_EKIP_PLAN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EKIP_PLAN> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipPlanGet(param).Get(filter);
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
        public ApiResultObject<HIS_EKIP_PLAN> Create(HIS_EKIP_PLAN data)
        {
            ApiResultObject<HIS_EKIP_PLAN> result = new ApiResultObject<HIS_EKIP_PLAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipPlanCreate(param).Create(data);
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
        public ApiResultObject<HIS_EKIP_PLAN> Update(HIS_EKIP_PLAN data)
        {
            ApiResultObject<HIS_EKIP_PLAN> result = new ApiResultObject<HIS_EKIP_PLAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipPlanUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EKIP_PLAN> ChangeLock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN> result = new ApiResultObject<HIS_EKIP_PLAN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_PLAN> Lock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_PLAN> Unlock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEkipPlanTruncate(param).Truncate(id);
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
