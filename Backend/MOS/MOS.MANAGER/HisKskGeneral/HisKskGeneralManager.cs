using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskGeneral
{
    public partial class HisKskGeneralManager : BusinessBase
    {
        public HisKskGeneralManager()
            : base()
        {

        }
        
        public HisKskGeneralManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_GENERAL>> Get(HisKskGeneralFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_GENERAL>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_GENERAL> resultData = null;
                if (valid)
                {
                    resultData = new HisKskGeneralGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_GENERAL> Create(HIS_KSK_GENERAL data)
        {
            ApiResultObject<HIS_KSK_GENERAL> result = new ApiResultObject<HIS_KSK_GENERAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_GENERAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskGeneralCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_GENERAL> Update(HIS_KSK_GENERAL data)
        {
            ApiResultObject<HIS_KSK_GENERAL> result = new ApiResultObject<HIS_KSK_GENERAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_GENERAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskGeneralUpdate(param).Update(data);
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
        public ApiResultObject<HIS_KSK_GENERAL> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_GENERAL> result = new ApiResultObject<HIS_KSK_GENERAL>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_GENERAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskGeneralLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_GENERAL> Lock(long id)
        {
            ApiResultObject<HIS_KSK_GENERAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_GENERAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskGeneralLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_GENERAL> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_GENERAL> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_GENERAL resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskGeneralLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskGeneralTruncate(param).Truncate(id);
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
