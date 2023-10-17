using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipPlanUser
{
    public partial class HisEkipPlanUserManager : BusinessBase
    {
        public HisEkipPlanUserManager()
            : base()
        {

        }
        
        public HisEkipPlanUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EKIP_PLAN_USER>> Get(HisEkipPlanUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_EKIP_PLAN_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EKIP_PLAN_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipPlanUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_EKIP_PLAN_USER> Create(HIS_EKIP_PLAN_USER data)
        {
            ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipPlanUserCreate(param).Create(data);
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
        public ApiResultObject<HIS_EKIP_PLAN_USER> Update(HIS_EKIP_PLAN_USER data)
        {
            ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_PLAN_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipPlanUserUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EKIP_PLAN_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN_USER> result = new ApiResultObject<HIS_EKIP_PLAN_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_PLAN_USER> Lock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_PLAN_USER> Unlock(long id)
        {
            ApiResultObject<HIS_EKIP_PLAN_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_PLAN_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipPlanUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEkipPlanUserTruncate(param).Truncate(id);
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
