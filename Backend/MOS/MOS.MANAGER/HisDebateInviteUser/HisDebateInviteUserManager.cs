using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebateInviteUser
{
    public partial class HisDebateInviteUserManager : BusinessBase
    {
        public HisDebateInviteUserManager()
            : base()
        {

        }
        
        public HisDebateInviteUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBATE_INVITE_USER>> Get(HisDebateInviteUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBATE_INVITE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBATE_INVITE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisDebateInviteUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBATE_INVITE_USER> Create(HIS_DEBATE_INVITE_USER data)
        {
            ApiResultObject<HIS_DEBATE_INVITE_USER> result = new ApiResultObject<HIS_DEBATE_INVITE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_INVITE_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateInviteUserCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBATE_INVITE_USER> Update(HIS_DEBATE_INVITE_USER data)
        {
            ApiResultObject<HIS_DEBATE_INVITE_USER> result = new ApiResultObject<HIS_DEBATE_INVITE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBATE_INVITE_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebateInviteUserUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBATE_INVITE_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBATE_INVITE_USER> result = new ApiResultObject<HIS_DEBATE_INVITE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_INVITE_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateInviteUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_INVITE_USER> Lock(long id)
        {
            ApiResultObject<HIS_DEBATE_INVITE_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_INVITE_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateInviteUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBATE_INVITE_USER> Unlock(long id)
        {
            ApiResultObject<HIS_DEBATE_INVITE_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBATE_INVITE_USER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebateInviteUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebateInviteUserTruncate(param).Truncate(id);
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
