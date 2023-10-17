using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPetroleum
{
    public partial class HisPetroleumManager : BusinessBase
    {
        public HisPetroleumManager()
            : base()
        {

        }
        
        public HisPetroleumManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_PETROLEUM>> Get(HisPetroleumFilterQuery filter)
        {
            ApiResultObject<List<HIS_PETROLEUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_PETROLEUM> resultData = null;
                if (valid)
                {
                    resultData = new HisPetroleumGet(param).Get(filter);
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
        public ApiResultObject<HIS_PETROLEUM> Create(HIS_PETROLEUM data)
        {
            ApiResultObject<HIS_PETROLEUM> result = new ApiResultObject<HIS_PETROLEUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PETROLEUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPetroleumCreate(param).Create(data);
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
        public ApiResultObject<HIS_PETROLEUM> Update(HIS_PETROLEUM data)
        {
            ApiResultObject<HIS_PETROLEUM> result = new ApiResultObject<HIS_PETROLEUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_PETROLEUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPetroleumUpdate(param).Update(data);
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
        public ApiResultObject<HIS_PETROLEUM> ChangeLock(long id)
        {
            ApiResultObject<HIS_PETROLEUM> result = new ApiResultObject<HIS_PETROLEUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PETROLEUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPetroleumLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_PETROLEUM> Lock(long id)
        {
            ApiResultObject<HIS_PETROLEUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PETROLEUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPetroleumLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_PETROLEUM> Unlock(long id)
        {
            ApiResultObject<HIS_PETROLEUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_PETROLEUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPetroleumLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPetroleumTruncate(param).Truncate(id);
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
