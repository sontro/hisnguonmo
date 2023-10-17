using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExmeReasonCfg
{
    public partial class HisExmeReasonCfgManager : BusinessBase
    {
        public HisExmeReasonCfgManager()
            : base()
        {

        }
        
        public HisExmeReasonCfgManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EXME_REASON_CFG>> Get(HisExmeReasonCfgFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXME_REASON_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXME_REASON_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisExmeReasonCfgGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXME_REASON_CFG> Create(HIS_EXME_REASON_CFG data)
        {
            ApiResultObject<HIS_EXME_REASON_CFG> result = new ApiResultObject<HIS_EXME_REASON_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXME_REASON_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExmeReasonCfgCreate(param).Create(data);
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
        public ApiResultObject<HIS_EXME_REASON_CFG> Update(HIS_EXME_REASON_CFG data)
        {
            ApiResultObject<HIS_EXME_REASON_CFG> result = new ApiResultObject<HIS_EXME_REASON_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXME_REASON_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisExmeReasonCfgUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EXME_REASON_CFG> ChangeLock(long id)
        {
            ApiResultObject<HIS_EXME_REASON_CFG> result = new ApiResultObject<HIS_EXME_REASON_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXME_REASON_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExmeReasonCfgLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EXME_REASON_CFG> Lock(long id)
        {
            ApiResultObject<HIS_EXME_REASON_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXME_REASON_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExmeReasonCfgLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EXME_REASON_CFG> Unlock(long id)
        {
            ApiResultObject<HIS_EXME_REASON_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EXME_REASON_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisExmeReasonCfgLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisExmeReasonCfgTruncate(param).Truncate(id);
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
