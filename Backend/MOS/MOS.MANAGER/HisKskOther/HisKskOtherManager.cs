using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskOther
{
    public partial class HisKskOtherManager : BusinessBase
    {
        public HisKskOtherManager()
            : base()
        {

        }
        
        public HisKskOtherManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_OTHER>> Get(HisKskOtherFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_OTHER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_OTHER> resultData = null;
                if (valid)
                {
                    resultData = new HisKskOtherGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_OTHER> Create(HIS_KSK_OTHER data)
        {
            ApiResultObject<HIS_KSK_OTHER> result = new ApiResultObject<HIS_KSK_OTHER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OTHER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOtherCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_OTHER> Update(HIS_KSK_OTHER data)
        {
            ApiResultObject<HIS_KSK_OTHER> result = new ApiResultObject<HIS_KSK_OTHER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_OTHER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskOtherUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_OTHER> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_OTHER> result = new ApiResultObject<HIS_KSK_OTHER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OTHER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOtherLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OTHER> Lock(long id)
        {
            ApiResultObject<HIS_KSK_OTHER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OTHER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOtherLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_OTHER> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_OTHER> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_OTHER resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskOtherLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskOtherTruncate(param).Truncate(id);
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
