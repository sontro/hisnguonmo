using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisArea
{
    public partial class HisAreaManager : BusinessBase
    {
        public HisAreaManager()
            : base()
        {

        }
        
        public HisAreaManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_AREA>> Get(HisAreaFilterQuery filter)
        {
            ApiResultObject<List<HIS_AREA>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_AREA> resultData = null;
                if (valid)
                {
                    resultData = new HisAreaGet(param).Get(filter);
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
        public ApiResultObject<HIS_AREA> Create(HIS_AREA data)
        {
            ApiResultObject<HIS_AREA> result = new ApiResultObject<HIS_AREA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_AREA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAreaCreate(param).Create(data);
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
        public ApiResultObject<HIS_AREA> Update(HIS_AREA data)
        {
            ApiResultObject<HIS_AREA> result = new ApiResultObject<HIS_AREA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_AREA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAreaUpdate(param).Update(data);
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
        public ApiResultObject<HIS_AREA> ChangeLock(long id)
        {
            ApiResultObject<HIS_AREA> result = new ApiResultObject<HIS_AREA>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_AREA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAreaLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_AREA> Lock(long id)
        {
            ApiResultObject<HIS_AREA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_AREA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAreaLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_AREA> Unlock(long id)
        {
            ApiResultObject<HIS_AREA> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_AREA resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAreaLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAreaTruncate(param).Truncate(id);
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
