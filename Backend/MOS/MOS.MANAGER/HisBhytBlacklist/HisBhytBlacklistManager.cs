using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisBhytBlacklist
{
    public partial class HisBhytBlacklistManager : BusinessBase
    {
        public HisBhytBlacklistManager()
            : base()
        {

        }
        
        public HisBhytBlacklistManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_BHYT_BLACKLIST>> Get(HisBhytBlacklistFilterQuery filter)
        {
            ApiResultObject<List<HIS_BHYT_BLACKLIST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_BHYT_BLACKLIST> resultData = null;
                if (valid)
                {
                    resultData = new HisBhytBlacklistGet(param).Get(filter);
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
        public ApiResultObject<HIS_BHYT_BLACKLIST> Create(HIS_BHYT_BLACKLIST data)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_BLACKLIST resultData = null;
                if (valid && new HisBhytBlacklistCreate(param).Create(data))
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
        public ApiResultObject<HIS_BHYT_BLACKLIST> Update(HIS_BHYT_BLACKLIST data)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_BHYT_BLACKLIST resultData = null;
                if (valid && new HisBhytBlacklistUpdate(param).Update(data))
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
        public ApiResultObject<HIS_BHYT_BLACKLIST> ChangeLock(long id)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = new ApiResultObject<HIS_BHYT_BLACKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_BLACKLIST resultData = null;
                if (valid)
                {
                    new HisBhytBlacklistLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_BHYT_BLACKLIST> Lock(long id)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_BLACKLIST resultData = null;
                if (valid)
                {
                    new HisBhytBlacklistLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_BHYT_BLACKLIST> Unlock(long id)
        {
            ApiResultObject<HIS_BHYT_BLACKLIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_BHYT_BLACKLIST resultData = null;
                if (valid)
                {
                    new HisBhytBlacklistLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisBhytBlacklistTruncate(param).Truncate(id);
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
