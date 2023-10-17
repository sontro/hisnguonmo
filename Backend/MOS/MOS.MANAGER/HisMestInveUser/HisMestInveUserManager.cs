using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestInveUser
{
    public partial class HisMestInveUserManager : BusinessBase
    {
        public HisMestInveUserManager()
            : base()
        {

        }
        
        public HisMestInveUserManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_INVE_USER>> Get(HisMestInveUserFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_INVE_USER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_INVE_USER> resultData = null;
                if (valid)
                {
                    resultData = new HisMestInveUserGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_INVE_USER> Create(HIS_MEST_INVE_USER data)
        {
            ApiResultObject<HIS_MEST_INVE_USER> result = new ApiResultObject<HIS_MEST_INVE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_INVE_USER resultData = null;
                if (valid && new HisMestInveUserCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEST_INVE_USER> Update(HIS_MEST_INVE_USER data)
        {
            ApiResultObject<HIS_MEST_INVE_USER> result = new ApiResultObject<HIS_MEST_INVE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_INVE_USER resultData = null;
                if (valid && new HisMestInveUserUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_INVE_USER> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_INVE_USER> result = new ApiResultObject<HIS_MEST_INVE_USER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVE_USER resultData = null;
                if (valid)
                {
                    new HisMestInveUserLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_INVE_USER> Lock(long id)
        {
            ApiResultObject<HIS_MEST_INVE_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVE_USER resultData = null;
                if (valid)
                {
                    new HisMestInveUserLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_INVE_USER> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_INVE_USER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_INVE_USER resultData = null;
                if (valid)
                {
                    new HisMestInveUserLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestInveUserTruncate(param).Truncate(id);
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
