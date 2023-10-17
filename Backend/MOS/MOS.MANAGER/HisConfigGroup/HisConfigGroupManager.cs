using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisConfigGroup
{
    public partial class HisConfigGroupManager : BusinessBase
    {
        public HisConfigGroupManager()
            : base()
        {

        }
        
        public HisConfigGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CONFIG_GROUP>> Get(HisConfigGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_CONFIG_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CONFIG_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisConfigGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_CONFIG_GROUP> Create(HIS_CONFIG_GROUP data)
        {
            ApiResultObject<HIS_CONFIG_GROUP> result = new ApiResultObject<HIS_CONFIG_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONFIG_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisConfigGroupCreate(param).Create(data);
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
        public ApiResultObject<HIS_CONFIG_GROUP> Update(HIS_CONFIG_GROUP data)
        {
            ApiResultObject<HIS_CONFIG_GROUP> result = new ApiResultObject<HIS_CONFIG_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONFIG_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisConfigGroupUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CONFIG_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_CONFIG_GROUP> result = new ApiResultObject<HIS_CONFIG_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisConfigGroupLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CONFIG_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_CONFIG_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisConfigGroupLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CONFIG_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_CONFIG_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONFIG_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisConfigGroupLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisConfigGroupTruncate(param).Truncate(id);
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
