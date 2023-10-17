using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAtc
{
    public partial class HisAtcManager : BusinessBase
    {
        public HisAtcManager()
            : base()
        {

        }
        
        public HisAtcManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ATC>> Get(HisAtcFilterQuery filter)
        {
            ApiResultObject<List<HIS_ATC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ATC> resultData = null;
                if (valid)
                {
                    resultData = new HisAtcGet(param).Get(filter);
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
        public ApiResultObject<HIS_ATC> Create(HIS_ATC data)
        {
            ApiResultObject<HIS_ATC> result = new ApiResultObject<HIS_ATC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ATC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAtcCreate(param).Create(data);
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
        public ApiResultObject<HIS_ATC> Update(HIS_ATC data)
        {
            ApiResultObject<HIS_ATC> result = new ApiResultObject<HIS_ATC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ATC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAtcUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ATC> ChangeLock(long id)
        {
            ApiResultObject<HIS_ATC> result = new ApiResultObject<HIS_ATC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ATC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAtcLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ATC> Lock(long id)
        {
            ApiResultObject<HIS_ATC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ATC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAtcLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ATC> Unlock(long id)
        {
            ApiResultObject<HIS_ATC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ATC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAtcLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAtcTruncate(param).Truncate(id);
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
