using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisWarningFeeCfg
{
    public partial class HisWarningFeeCfgManager : BusinessBase
    {
        public HisWarningFeeCfgManager()
            : base()
        {

        }
        
        public HisWarningFeeCfgManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_WARNING_FEE_CFG>> Get(HisWarningFeeCfgFilterQuery filter)
        {
            ApiResultObject<List<HIS_WARNING_FEE_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_WARNING_FEE_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisWarningFeeCfgGet(param).Get(filter);
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
        public ApiResultObject<HIS_WARNING_FEE_CFG> Create(HIS_WARNING_FEE_CFG data)
        {
            ApiResultObject<HIS_WARNING_FEE_CFG> result = new ApiResultObject<HIS_WARNING_FEE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WARNING_FEE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisWarningFeeCfgCreate(param).Create(data);
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
        public ApiResultObject<HIS_WARNING_FEE_CFG> Update(HIS_WARNING_FEE_CFG data)
        {
            ApiResultObject<HIS_WARNING_FEE_CFG> result = new ApiResultObject<HIS_WARNING_FEE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_WARNING_FEE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisWarningFeeCfgUpdate(param).Update(data);
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
        public ApiResultObject<HIS_WARNING_FEE_CFG> ChangeLock(long id)
        {
            ApiResultObject<HIS_WARNING_FEE_CFG> result = new ApiResultObject<HIS_WARNING_FEE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WARNING_FEE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWarningFeeCfgLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_WARNING_FEE_CFG> Lock(long id)
        {
            ApiResultObject<HIS_WARNING_FEE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WARNING_FEE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWarningFeeCfgLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_WARNING_FEE_CFG> Unlock(long id)
        {
            ApiResultObject<HIS_WARNING_FEE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_WARNING_FEE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisWarningFeeCfgLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisWarningFeeCfgTruncate(param).Truncate(id);
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
