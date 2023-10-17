using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisEkipTemp
{
    public partial class HisEkipTempManager : BusinessBase
    {
        public HisEkipTempManager()
            : base()
        {

        }
        
        public HisEkipTempManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_EKIP_TEMP>> Get(HisEkipTempFilterQuery filter)
        {
            ApiResultObject<List<HIS_EKIP_TEMP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EKIP_TEMP> resultData = null;
                if (valid)
                {
                    resultData = new HisEkipTempGet(param).Get(filter);
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
        public ApiResultObject<HIS_EKIP_TEMP> Create(HIS_EKIP_TEMP data)
        {
            ApiResultObject<HIS_EKIP_TEMP> result = new ApiResultObject<HIS_EKIP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipTempCreate(param).Create(data);
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
        public ApiResultObject<HIS_EKIP_TEMP> Update(HIS_EKIP_TEMP data)
        {
            ApiResultObject<HIS_EKIP_TEMP> result = new ApiResultObject<HIS_EKIP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EKIP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisEkipTempUpdate(param).Update(data);
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
        public ApiResultObject<HIS_EKIP_TEMP> ChangeLock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP> result = new ApiResultObject<HIS_EKIP_TEMP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_TEMP> Lock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_EKIP_TEMP> Unlock(long id)
        {
            ApiResultObject<HIS_EKIP_TEMP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_EKIP_TEMP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisEkipTempLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisEkipTempTruncate(param).Truncate(id);
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
