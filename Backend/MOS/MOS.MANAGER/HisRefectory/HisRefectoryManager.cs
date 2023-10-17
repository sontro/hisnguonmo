using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisRefectory
{
    public partial class HisRefectoryManager : BusinessBase
    {
        public HisRefectoryManager()
            : base()
        {

        }
        
        public HisRefectoryManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REFECTORY>> Get(HisRefectoryFilterQuery filter)
        {
            ApiResultObject<List<HIS_REFECTORY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REFECTORY> resultData = null;
                if (valid)
                {
                    resultData = new HisRefectoryGet(param).Get(filter);
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
        public ApiResultObject<HisRefectorySDO> Create(HisRefectorySDO data)
        {
            ApiResultObject<HisRefectorySDO> result = new ApiResultObject<HisRefectorySDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisRefectorySDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRefectoryCreate(param).Create(data);
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
        public ApiResultObject<HisRefectorySDO> Update(HisRefectorySDO data)
        {
            ApiResultObject<HisRefectorySDO> result = new ApiResultObject<HisRefectorySDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HisRefectorySDO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRefectoryUpdate(param).Update(data);
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
        public ApiResultObject<HIS_REFECTORY> ChangeLock(long id)
        {
            ApiResultObject<HIS_REFECTORY> result = new ApiResultObject<HIS_REFECTORY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REFECTORY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRefectoryLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REFECTORY> Lock(long id)
        {
            ApiResultObject<HIS_REFECTORY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REFECTORY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRefectoryLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REFECTORY> Unlock(long id)
        {
            ApiResultObject<HIS_REFECTORY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REFECTORY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRefectoryLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRefectoryTruncate(param).Truncate(id);
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
