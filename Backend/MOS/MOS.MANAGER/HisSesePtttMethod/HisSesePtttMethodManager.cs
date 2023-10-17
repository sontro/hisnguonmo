using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSesePtttMethod
{
    public partial class HisSesePtttMethodManager : BusinessBase
    {
        public HisSesePtttMethodManager()
            : base()
        {

        }
        
        public HisSesePtttMethodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SESE_PTTT_METHOD>> Get(HisSesePtttMethodFilterQuery filter)
        {
            ApiResultObject<List<HIS_SESE_PTTT_METHOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SESE_PTTT_METHOD> resultData = null;
                if (valid)
                {
                    resultData = new HisSesePtttMethodGet(param).Get(filter);
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
        public ApiResultObject<HIS_SESE_PTTT_METHOD> Create(HIS_SESE_PTTT_METHOD data)
        {
            ApiResultObject<HIS_SESE_PTTT_METHOD> result = new ApiResultObject<HIS_SESE_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_PTTT_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSesePtttMethodCreate(param).Create(data);
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
        public ApiResultObject<HIS_SESE_PTTT_METHOD> Update(HIS_SESE_PTTT_METHOD data)
        {
            ApiResultObject<HIS_SESE_PTTT_METHOD> result = new ApiResultObject<HIS_SESE_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_PTTT_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSesePtttMethodUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SESE_PTTT_METHOD> ChangeLock(long id)
        {
            ApiResultObject<HIS_SESE_PTTT_METHOD> result = new ApiResultObject<HIS_SESE_PTTT_METHOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_PTTT_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSesePtttMethodLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_PTTT_METHOD> Lock(long id)
        {
            ApiResultObject<HIS_SESE_PTTT_METHOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_PTTT_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSesePtttMethodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_PTTT_METHOD> Unlock(long id)
        {
            ApiResultObject<HIS_SESE_PTTT_METHOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_PTTT_METHOD resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSesePtttMethodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSesePtttMethodTruncate(param).Truncate(id);
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
