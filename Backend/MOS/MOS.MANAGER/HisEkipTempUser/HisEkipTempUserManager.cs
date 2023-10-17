using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTempUser
{
    public partial class HisEkipTempUserManager : BusinessBase
    {
        public HisEkipTempUserManager()
            : base()
        {

        }
        
        public HisEkipTempUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EKIP_TEMP_USER>> Get(HisEkipTempUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_EKIP_TEMP_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EKIP_TEMP_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipTempUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_EKIP_TEMP_USER> Create(HIS_EKIP_TEMP_USER data)
        {
            ApiResultObject<HIS_EKIP_TEMP_USER> result = new ApiResultObject<HIS_EKIP_TEMP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_TEMP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipTempUserCreate(param).Create(data);
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
        public ApiResultObject<HIS_EKIP_TEMP_USER> Update(HIS_EKIP_TEMP_USER data)
        {
            ApiResultObject<HIS_EKIP_TEMP_USER> result = new ApiResultObject<HIS_EKIP_TEMP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_TEMP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipTempUserUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EKIP_TEMP_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP_USER> result = new ApiResultObject<HIS_EKIP_TEMP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_TEMP_USER> Lock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_TEMP_USER> Unlock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEkipTempUserTruncate(param).Truncate(id);
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
