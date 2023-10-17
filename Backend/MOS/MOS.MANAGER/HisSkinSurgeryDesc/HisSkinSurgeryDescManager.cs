using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSkinSurgeryDesc
{
    public partial class HisSkinSurgeryDescManager : BusinessBase
    {
        public HisSkinSurgeryDescManager()
            : base()
        {

        }
        
        public HisSkinSurgeryDescManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SKIN_SURGERY_DESC>> Get(HisSkinSurgeryDescFilterQuery filter)
        {
            ApiResultObject<List<HIS_SKIN_SURGERY_DESC>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SKIN_SURGERY_DESC> resultData = null;
                if (valid)
                {
                    resultData = new HisSkinSurgeryDescGet(param).Get(filter);
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
        public ApiResultObject<HIS_SKIN_SURGERY_DESC> Create(HIS_SKIN_SURGERY_DESC data)
        {
            ApiResultObject<HIS_SKIN_SURGERY_DESC> result = new ApiResultObject<HIS_SKIN_SURGERY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SKIN_SURGERY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSkinSurgeryDescCreate(param).Create(data);
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
        public ApiResultObject<HIS_SKIN_SURGERY_DESC> Update(HIS_SKIN_SURGERY_DESC data)
        {
            ApiResultObject<HIS_SKIN_SURGERY_DESC> result = new ApiResultObject<HIS_SKIN_SURGERY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SKIN_SURGERY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSkinSurgeryDescUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SKIN_SURGERY_DESC> ChangeLock(long id)
        {
            ApiResultObject<HIS_SKIN_SURGERY_DESC> result = new ApiResultObject<HIS_SKIN_SURGERY_DESC>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SKIN_SURGERY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSkinSurgeryDescLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SKIN_SURGERY_DESC> Lock(long id)
        {
            ApiResultObject<HIS_SKIN_SURGERY_DESC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SKIN_SURGERY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSkinSurgeryDescLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SKIN_SURGERY_DESC> Unlock(long id)
        {
            ApiResultObject<HIS_SKIN_SURGERY_DESC> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SKIN_SURGERY_DESC resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSkinSurgeryDescLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSkinSurgeryDescTruncate(param).Truncate(id);
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
