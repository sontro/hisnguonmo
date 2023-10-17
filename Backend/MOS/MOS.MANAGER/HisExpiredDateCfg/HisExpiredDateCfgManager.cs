using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpiredDateCfg
{
    public partial class HisExpiredDateCfgManager : BusinessBase
    {
        public HisExpiredDateCfgManager()
            : base()
        {

        }
        
        public HisExpiredDateCfgManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXPIRED_DATE_CFG>> Get(HisExpiredDateCfgFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXPIRED_DATE_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXPIRED_DATE_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisExpiredDateCfgGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXPIRED_DATE_CFG> Create(HIS_EXPIRED_DATE_CFG data)
        {
            ApiResultObject<HIS_EXPIRED_DATE_CFG> result = new ApiResultObject<HIS_EXPIRED_DATE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXPIRED_DATE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpiredDateCfgCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXPIRED_DATE_CFG> Update(HIS_EXPIRED_DATE_CFG data)
        {
            ApiResultObject<HIS_EXPIRED_DATE_CFG> result = new ApiResultObject<HIS_EXPIRED_DATE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXPIRED_DATE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExpiredDateCfgUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXPIRED_DATE_CFG> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXPIRED_DATE_CFG> result = new ApiResultObject<HIS_EXPIRED_DATE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXPIRED_DATE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpiredDateCfgLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXPIRED_DATE_CFG> Lock(long id)
        {
            ApiResultObject<HIS_EXPIRED_DATE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXPIRED_DATE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpiredDateCfgLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXPIRED_DATE_CFG> Unlock(long id)
        {
            ApiResultObject<HIS_EXPIRED_DATE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXPIRED_DATE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExpiredDateCfgLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExpiredDateCfgTruncate(param).Truncate(id);
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
