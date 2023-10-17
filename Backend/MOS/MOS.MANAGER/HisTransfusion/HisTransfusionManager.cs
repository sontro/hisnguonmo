using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusion
{
    public partial class HisTransfusionManager : BusinessBase
    {
        public HisTransfusionManager()
            : base()
        {

        }
        
        public HisTransfusionManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRANSFUSION>> Get(HisTransfusionFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANSFUSION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANSFUSION> resultData = null;
                if (valid)
                {
                    resultData = new HisTransfusionGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRANSFUSION> Create(HIS_TRANSFUSION data)
        {
            ApiResultObject<HIS_TRANSFUSION> result = new ApiResultObject<HIS_TRANSFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransfusionCreate(param).Create(data);
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
        public ApiResultObject<HIS_TRANSFUSION> Update(HIS_TRANSFUSION data)
        {
            ApiResultObject<HIS_TRANSFUSION> result = new ApiResultObject<HIS_TRANSFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransfusionUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TRANSFUSION> ChangeLock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION> result = new ApiResultObject<HIS_TRANSFUSION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSFUSION> Lock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSFUSION> Unlock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTransfusionTruncate(param).Truncate(id);
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
