using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisRejectAlert
{
    public partial class HisRejectAlertManager : BusinessBase
    {
        public HisRejectAlertManager()
            : base()
        {

        }
        
        public HisRejectAlertManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_REJECT_ALERT>> Get(HisRejectAlertFilterQuery filter)
        {
            ApiResultObject<List<HIS_REJECT_ALERT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_REJECT_ALERT> resultData = null;
                if (valid)
                {
                    resultData = new HisRejectAlertGet(param).Get(filter);
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
        public ApiResultObject<HIS_REJECT_ALERT> Create(HIS_REJECT_ALERT data)
        {
            ApiResultObject<HIS_REJECT_ALERT> result = new ApiResultObject<HIS_REJECT_ALERT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REJECT_ALERT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRejectAlertCreate(param).Create(data);
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
        public ApiResultObject<HIS_REJECT_ALERT> Update(HIS_REJECT_ALERT data)
        {
            ApiResultObject<HIS_REJECT_ALERT> result = new ApiResultObject<HIS_REJECT_ALERT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_REJECT_ALERT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisRejectAlertUpdate(param).Update(data);
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
        public ApiResultObject<HIS_REJECT_ALERT> ChangeLock(long id)
        {
            ApiResultObject<HIS_REJECT_ALERT> result = new ApiResultObject<HIS_REJECT_ALERT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REJECT_ALERT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRejectAlertLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_REJECT_ALERT> Lock(long id)
        {
            ApiResultObject<HIS_REJECT_ALERT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REJECT_ALERT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRejectAlertLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_REJECT_ALERT> Unlock(long id)
        {
            ApiResultObject<HIS_REJECT_ALERT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_REJECT_ALERT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRejectAlertLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisRejectAlertTruncate(param).Truncate(id);
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
