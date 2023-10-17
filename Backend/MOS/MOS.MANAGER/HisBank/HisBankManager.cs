using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBank
{
    public partial class HisBankManager : BusinessBase
    {
        public HisBankManager()
            : base()
        {

        }
        
        public HisBankManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BANK>> Get(HisBankFilterQuery filter)
        {
            ApiResultObject<List<HIS_BANK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BANK> resultData = null;
                if (valid)
                {
                    resultData = new HisBankGet(param).Get(filter);
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
        public ApiResultObject<HIS_BANK> Create(HIS_BANK data)
        {
            ApiResultObject<HIS_BANK> result = new ApiResultObject<HIS_BANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBankCreate(param).Create(data);
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
        public ApiResultObject<HIS_BANK> Update(HIS_BANK data)
        {
            ApiResultObject<HIS_BANK> result = new ApiResultObject<HIS_BANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisBankUpdate(param).Update(data);
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
        public ApiResultObject<HIS_BANK> ChangeLock(long id)
        {
            ApiResultObject<HIS_BANK> result = new ApiResultObject<HIS_BANK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBankLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BANK> Lock(long id)
        {
            ApiResultObject<HIS_BANK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBankLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BANK> Unlock(long id)
        {
            ApiResultObject<HIS_BANK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BANK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisBankLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBankTruncate(param).Truncate(id);
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
