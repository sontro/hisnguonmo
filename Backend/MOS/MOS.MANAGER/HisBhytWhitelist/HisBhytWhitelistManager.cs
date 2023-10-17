using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytWhitelist
{
    public partial class HisBhytWhitelistManager : BusinessBase
    {
        public HisBhytWhitelistManager()
            : base()
        {

        }
        
        public HisBhytWhitelistManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BHYT_WHITELIST>> Get(HisBhytWhitelistFilterQuery filter)
        {
            ApiResultObject<List<HIS_BHYT_WHITELIST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BHYT_WHITELIST> resultData = null;
                if (valid)
                {
                    resultData = new HisBhytWhitelistGet(param).Get(filter);
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
        public ApiResultObject<HIS_BHYT_WHITELIST> Create(HIS_BHYT_WHITELIST data)
        {
            ApiResultObject<HIS_BHYT_WHITELIST> result = new ApiResultObject<HIS_BHYT_WHITELIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_WHITELIST resultData = null;
                if (valid && new HisBhytWhitelistCreate(param).Create(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_BHYT_WHITELIST> Update(HIS_BHYT_WHITELIST data)
        {
            ApiResultObject<HIS_BHYT_WHITELIST> result = new ApiResultObject<HIS_BHYT_WHITELIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_WHITELIST resultData = null;
                if (valid && new HisBhytWhitelistUpdate(param).Update(data))
                {
                    resultData = data;
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

		[Logger]
        public ApiResultObject<HIS_BHYT_WHITELIST> ChangeLock(long id)
        {
            ApiResultObject<HIS_BHYT_WHITELIST> result = new ApiResultObject<HIS_BHYT_WHITELIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_WHITELIST resultData = null;
                if (valid)
                {
                    new HisBhytWhitelistLock(param).ChangeLock(id, ref resultData);
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
		
		[Logger]
        public ApiResultObject<HIS_BHYT_WHITELIST> Lock(long id)
        {
            ApiResultObject<HIS_BHYT_WHITELIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_WHITELIST resultData = null;
                if (valid)
                {
                    new HisBhytWhitelistLock(param).Lock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_BHYT_WHITELIST> Unlock(long id)
        {
            ApiResultObject<HIS_BHYT_WHITELIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_WHITELIST resultData = null;
                if (valid)
                {
                    new HisBhytWhitelistLock(param).Unlock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
                    resultData = new HisBhytWhitelistTruncate(param).Truncate(id);
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
