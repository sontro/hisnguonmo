using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateEkipUser
{
    public partial class HisDebateEkipUserManager : BusinessBase
    {
        public HisDebateEkipUserManager()
            : base()
        {

        }
        
        public HisDebateEkipUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBATE_EKIP_USER>> Get(HisDebateEkipUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBATE_EKIP_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBATE_EKIP_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateEkipUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBATE_EKIP_USER> Create(HIS_DEBATE_EKIP_USER data)
        {
            ApiResultObject<HIS_DEBATE_EKIP_USER> result = new ApiResultObject<HIS_DEBATE_EKIP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_EKIP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateEkipUserCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBATE_EKIP_USER> Update(HIS_DEBATE_EKIP_USER data)
        {
            ApiResultObject<HIS_DEBATE_EKIP_USER> result = new ApiResultObject<HIS_DEBATE_EKIP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_EKIP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateEkipUserUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBATE_EKIP_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBATE_EKIP_USER> result = new ApiResultObject<HIS_DEBATE_EKIP_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_EKIP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateEkipUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_EKIP_USER> Lock(long id)
        {
            ApiResultObject<HIS_DEBATE_EKIP_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_EKIP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateEkipUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_EKIP_USER> Unlock(long id)
        {
            ApiResultObject<HIS_DEBATE_EKIP_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_EKIP_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateEkipUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebateEkipUserTruncate(param).Truncate(id);
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
