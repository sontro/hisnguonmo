using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContact
{
    public partial class HisContactManager : BusinessBase
    {
        public HisContactManager()
            : base()
        {

        }
        
        public HisContactManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CONTACT>> Get(HisContactFilterQuery filter)
        {
            ApiResultObject<List<HIS_CONTACT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CONTACT> resultData = null;
                if (valid)
                {
                    resultData = new HisContactGet(param).Get(filter);
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
        public ApiResultObject<HIS_CONTACT> Create(HIS_CONTACT data)
        {
            ApiResultObject<HIS_CONTACT> result = new ApiResultObject<HIS_CONTACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContactCreate(param).Create(data);
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
        public ApiResultObject<HIS_CONTACT> Update(HIS_CONTACT data)
        {
            ApiResultObject<HIS_CONTACT> result = new ApiResultObject<HIS_CONTACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContactUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CONTACT> ChangeLock(long id)
        {
            ApiResultObject<HIS_CONTACT> result = new ApiResultObject<HIS_CONTACT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTACT> Lock(long id)
        {
            ApiResultObject<HIS_CONTACT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTACT> Unlock(long id)
        {
            ApiResultObject<HIS_CONTACT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTACT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContactLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisContactTruncate(param).Truncate(id);
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
