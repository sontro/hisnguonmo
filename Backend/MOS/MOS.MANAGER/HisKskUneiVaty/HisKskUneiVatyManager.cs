using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskUneiVaty
{
    public partial class HisKskUneiVatyManager : BusinessBase
    {
        public HisKskUneiVatyManager()
            : base()
        {

        }
        
        public HisKskUneiVatyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_UNEI_VATY>> Get(HisKskUneiVatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_UNEI_VATY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_UNEI_VATY> resultData = null;
                if (valid)
                {
                    resultData = new HisKskUneiVatyGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_UNEI_VATY> Create(HIS_KSK_UNEI_VATY data)
        {
            ApiResultObject<HIS_KSK_UNEI_VATY> result = new ApiResultObject<HIS_KSK_UNEI_VATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNEI_VATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskUneiVatyCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_UNEI_VATY> Update(HIS_KSK_UNEI_VATY data)
        {
            ApiResultObject<HIS_KSK_UNEI_VATY> result = new ApiResultObject<HIS_KSK_UNEI_VATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_UNEI_VATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskUneiVatyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_UNEI_VATY> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_UNEI_VATY> result = new ApiResultObject<HIS_KSK_UNEI_VATY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNEI_VATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUneiVatyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_UNEI_VATY> Lock(long id)
        {
            ApiResultObject<HIS_KSK_UNEI_VATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNEI_VATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUneiVatyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_UNEI_VATY> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_UNEI_VATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_UNEI_VATY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskUneiVatyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskUneiVatyTruncate(param).Truncate(id);
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
