using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisHoreHandoverStt
{
    public partial class HisHoreHandoverSttManager : BusinessBase
    {
        public HisHoreHandoverSttManager()
            : base()
        {

        }
        
        public HisHoreHandoverSttManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_HORE_HANDOVER_STT>> Get(HisHoreHandoverSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_HORE_HANDOVER_STT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HORE_HANDOVER_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisHoreHandoverSttGet(param).Get(filter);
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
        public ApiResultObject<HIS_HORE_HANDOVER_STT> Create(HIS_HORE_HANDOVER_STT data)
        {
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HANDOVER_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreHandoverSttCreate(param).Create(data);
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
        public ApiResultObject<HIS_HORE_HANDOVER_STT> Update(HIS_HORE_HANDOVER_STT data)
        {
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HORE_HANDOVER_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisHoreHandoverSttUpdate(param).Update(data);
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
        public ApiResultObject<HIS_HORE_HANDOVER_STT> ChangeLock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = new ApiResultObject<HIS_HORE_HANDOVER_STT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverSttLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HANDOVER_STT> Lock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverSttLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HORE_HANDOVER_STT> Unlock(long id)
        {
            ApiResultObject<HIS_HORE_HANDOVER_STT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HORE_HANDOVER_STT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoreHandoverSttLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHoreHandoverSttTruncate(param).Truncate(id);
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
