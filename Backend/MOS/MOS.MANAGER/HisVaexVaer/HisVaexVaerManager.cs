using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisVaexVaer
{
    public partial class HisVaexVaerManager : BusinessBase
    {
        public HisVaexVaerManager()
            : base()
        {

        }
        
        public HisVaexVaerManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_VAEX_VAER>> Get(HisVaexVaerFilterQuery filter)
        {
            ApiResultObject<List<HIS_VAEX_VAER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_VAEX_VAER> resultData = null;
                if (valid)
                {
                    resultData = new HisVaexVaerGet(param).Get(filter);
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
        public ApiResultObject<HIS_VAEX_VAER> Create(HIS_VAEX_VAER data)
        {
            ApiResultObject<HIS_VAEX_VAER> result = new ApiResultObject<HIS_VAEX_VAER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VAEX_VAER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaexVaerCreate(param).Create(data);
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
        public ApiResultObject<HIS_VAEX_VAER> Update(HIS_VAEX_VAER data)
        {
            ApiResultObject<HIS_VAEX_VAER> result = new ApiResultObject<HIS_VAEX_VAER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_VAEX_VAER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisVaexVaerUpdate(param).Update(data);
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
        public ApiResultObject<HIS_VAEX_VAER> ChangeLock(long id)
        {
            ApiResultObject<HIS_VAEX_VAER> result = new ApiResultObject<HIS_VAEX_VAER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VAEX_VAER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaexVaerLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_VAEX_VAER> Lock(long id)
        {
            ApiResultObject<HIS_VAEX_VAER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VAEX_VAER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaexVaerLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_VAEX_VAER> Unlock(long id)
        {
            ApiResultObject<HIS_VAEX_VAER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_VAEX_VAER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisVaexVaerLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisVaexVaerTruncate(param).Truncate(id);
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
