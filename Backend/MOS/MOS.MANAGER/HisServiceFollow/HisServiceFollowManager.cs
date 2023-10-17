using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisServiceFollow
{
    public partial class HisServiceFollowManager : BusinessBase
    {
        public HisServiceFollowManager()
            : base()
        {

        }
        
        public HisServiceFollowManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SERVICE_FOLLOW>> Get(HisServiceFollowFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_FOLLOW>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_FOLLOW> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceFollowGet(param).Get(filter);
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
        public ApiResultObject<HIS_SERVICE_FOLLOW> Create(HIS_SERVICE_FOLLOW data)
        {
            ApiResultObject<HIS_SERVICE_FOLLOW> result = new ApiResultObject<HIS_SERVICE_FOLLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_FOLLOW resultData = null;
                if (valid && new HisServiceFollowCreate(param).Create(data))
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
        public ApiResultObject<HIS_SERVICE_FOLLOW> Update(HIS_SERVICE_FOLLOW data)
        {
            ApiResultObject<HIS_SERVICE_FOLLOW> result = new ApiResultObject<HIS_SERVICE_FOLLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_FOLLOW resultData = null;
                if (valid && new HisServiceFollowUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SERVICE_FOLLOW> ChangeLock(long data)
        {
            ApiResultObject<HIS_SERVICE_FOLLOW> result = new ApiResultObject<HIS_SERVICE_FOLLOW>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_FOLLOW resultData = null;
                if (valid)
                {
                    new HisServiceFollowLock(param).ChangeLock(data, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_FOLLOW> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_FOLLOW> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_FOLLOW resultData = null;
                if (valid)
                {
                    new HisServiceFollowLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_FOLLOW> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_FOLLOW> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_FOLLOW resultData = null;
                if (valid)
                {
                    new HisServiceFollowLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceFollowTruncate(param).Truncate(id);
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
