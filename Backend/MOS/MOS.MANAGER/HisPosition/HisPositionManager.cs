using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisPosition
{
    public partial class HisPositionManager : BusinessBase
    {
        public HisPositionManager()
            : base()
        {

        }
        
        public HisPositionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_POSITION>> Get(HisPositionFilterQuery filter)
        {
            ApiResultObject<List<HIS_POSITION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_POSITION> resultData = null;
                if (valid)
                {
                    resultData = new HisPositionGet(param).Get(filter);
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
        public ApiResultObject<HIS_POSITION> Create(HIS_POSITION data)
        {
            ApiResultObject<HIS_POSITION> result = new ApiResultObject<HIS_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_POSITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPositionCreate(param).Create(data);
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
        public ApiResultObject<HIS_POSITION> Update(HIS_POSITION data)
        {
            ApiResultObject<HIS_POSITION> result = new ApiResultObject<HIS_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_POSITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisPositionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_POSITION> ChangeLock(long id)
        {
            ApiResultObject<HIS_POSITION> result = new ApiResultObject<HIS_POSITION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_POSITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPositionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_POSITION> Lock(long id)
        {
            ApiResultObject<HIS_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_POSITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPositionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_POSITION> Unlock(long id)
        {
            ApiResultObject<HIS_POSITION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_POSITION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisPositionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisPositionTruncate(param).Truncate(id);
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
