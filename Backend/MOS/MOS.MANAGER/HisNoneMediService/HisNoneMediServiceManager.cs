using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisNoneMediService
{
    public partial class HisNoneMediServiceManager : BusinessBase
    {
        public HisNoneMediServiceManager()
            : base()
        {

        }
        
        public HisNoneMediServiceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_NONE_MEDI_SERVICE>> Get(HisNoneMediServiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_NONE_MEDI_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_NONE_MEDI_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisNoneMediServiceGet(param).Get(filter);
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
        public ApiResultObject<HIS_NONE_MEDI_SERVICE> Create(HIS_NONE_MEDI_SERVICE data)
        {
            ApiResultObject<HIS_NONE_MEDI_SERVICE> result = new ApiResultObject<HIS_NONE_MEDI_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NONE_MEDI_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNoneMediServiceCreate(param).Create(data);
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
        public ApiResultObject<HIS_NONE_MEDI_SERVICE> Update(HIS_NONE_MEDI_SERVICE data)
        {
            ApiResultObject<HIS_NONE_MEDI_SERVICE> result = new ApiResultObject<HIS_NONE_MEDI_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_NONE_MEDI_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisNoneMediServiceUpdate(param).Update(data);
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
        public ApiResultObject<HIS_NONE_MEDI_SERVICE> ChangeLock(long id)
        {
            ApiResultObject<HIS_NONE_MEDI_SERVICE> result = new ApiResultObject<HIS_NONE_MEDI_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NONE_MEDI_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNoneMediServiceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_NONE_MEDI_SERVICE> Lock(long id)
        {
            ApiResultObject<HIS_NONE_MEDI_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NONE_MEDI_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNoneMediServiceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_NONE_MEDI_SERVICE> Unlock(long id)
        {
            ApiResultObject<HIS_NONE_MEDI_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_NONE_MEDI_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisNoneMediServiceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisNoneMediServiceTruncate(param).Truncate(id);
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
