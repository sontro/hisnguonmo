using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFormTypeCfg
{
    public partial class HisFormTypeCfgManager : BusinessBase
    {
        public HisFormTypeCfgManager()
            : base()
        {

        }
        
        public HisFormTypeCfgManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_FORM_TYPE_CFG>> Get(HisFormTypeCfgFilterQuery filter)
        {
            ApiResultObject<List<HIS_FORM_TYPE_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_FORM_TYPE_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisFormTypeCfgGet(param).Get(filter);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG> Create(HIS_FORM_TYPE_CFG data)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG> result = new ApiResultObject<HIS_FORM_TYPE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFormTypeCfgCreate(param).Create(data);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG> Update(HIS_FORM_TYPE_CFG data)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG> result = new ApiResultObject<HIS_FORM_TYPE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FORM_TYPE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFormTypeCfgUpdate(param).Update(data);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG> ChangeLock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG> result = new ApiResultObject<HIS_FORM_TYPE_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG> Lock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_FORM_TYPE_CFG> Unlock(long id)
        {
            ApiResultObject<HIS_FORM_TYPE_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FORM_TYPE_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFormTypeCfgLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisFormTypeCfgTruncate(param).Truncate(id);
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
