using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRationGroup
{
    public partial class HisRationGroupManager : BusinessBase
    {
        public HisRationGroupManager()
            : base()
        {

        }
        
        public HisRationGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_RATION_GROUP>> Get(HisRationGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_RATION_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RATION_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisRationGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_RATION_GROUP> Create(HIS_RATION_GROUP data)
        {
            ApiResultObject<HIS_RATION_GROUP> result = new ApiResultObject<HIS_RATION_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationGroupCreate(param).Create(data);
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
        public ApiResultObject<HIS_RATION_GROUP> Update(HIS_RATION_GROUP data)
        {
            ApiResultObject<HIS_RATION_GROUP> result = new ApiResultObject<HIS_RATION_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRationGroupUpdate(param).Update(data);
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
        public ApiResultObject<HIS_RATION_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_RATION_GROUP> result = new ApiResultObject<HIS_RATION_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationGroupLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_RATION_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationGroupLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_RATION_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationGroupLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRationGroupTruncate(param).Truncate(id);
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
