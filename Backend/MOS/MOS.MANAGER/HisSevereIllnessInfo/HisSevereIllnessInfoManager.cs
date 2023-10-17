using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSevereIllnessInfo
{
    public partial class HisSevereIllnessInfoManager : BusinessBase
    {
        public HisSevereIllnessInfoManager()
            : base()
        {

        }
        
        public HisSevereIllnessInfoManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SEVERE_ILLNESS_INFO>> Get(HisSevereIllnessInfoFilterQuery filter)
        {
            ApiResultObject<List<HIS_SEVERE_ILLNESS_INFO>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SEVERE_ILLNESS_INFO> resultData = null;
                if (valid)
                {
                    resultData = new HisSevereIllnessInfoGet(param).Get(filter);
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
        public ApiResultObject<HIS_SEVERE_ILLNESS_INFO> Create(HIS_SEVERE_ILLNESS_INFO data)
        {
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SEVERE_ILLNESS_INFO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSevereIllnessInfoCreate(param).Create(data);
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
        public ApiResultObject<HIS_SEVERE_ILLNESS_INFO> Update(HIS_SEVERE_ILLNESS_INFO data)
        {
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SEVERE_ILLNESS_INFO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSevereIllnessInfoUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SEVERE_ILLNESS_INFO> ChangeLock(long id)
        {
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = new ApiResultObject<HIS_SEVERE_ILLNESS_INFO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SEVERE_ILLNESS_INFO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSevereIllnessInfoLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SEVERE_ILLNESS_INFO> Lock(long id)
        {
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SEVERE_ILLNESS_INFO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSevereIllnessInfoLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SEVERE_ILLNESS_INFO> Unlock(long id)
        {
            ApiResultObject<HIS_SEVERE_ILLNESS_INFO> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SEVERE_ILLNESS_INFO resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSevereIllnessInfoLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSevereIllnessInfoTruncate(param).Truncate(id);
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

        [Logger]
        public ApiResultObject<SevereIllnessInfoSDO> CreateOrUpdate(SevereIllnessInfoSDO data)
        {
            ApiResultObject<SevereIllnessInfoSDO> result = new ApiResultObject<SevereIllnessInfoSDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                SevereIllnessInfoSDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSevereIllnessInfoCreateOrUpdate(param).CreateOrUpdate(data);
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
    }
}
