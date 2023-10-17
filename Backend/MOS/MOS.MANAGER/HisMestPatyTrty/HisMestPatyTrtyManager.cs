using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPatyTrty
{
    public partial class HisMestPatyTrtyManager : BusinessBase
    {
        public HisMestPatyTrtyManager()
            : base()
        {

        }
        
        public HisMestPatyTrtyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_PATY_TRTY>> Get(HisMestPatyTrtyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PATY_TRTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PATY_TRTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPatyTrtyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_PATY_TRTY> Create(HIS_MEST_PATY_TRTY data)
        {
            ApiResultObject<HIS_MEST_PATY_TRTY> result = new ApiResultObject<HIS_MEST_PATY_TRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_TRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMestPatyTrtyCreate(param).Create(data);
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
        public ApiResultObject<HIS_MEST_PATY_TRTY> Update(HIS_MEST_PATY_TRTY data)
        {
            ApiResultObject<HIS_MEST_PATY_TRTY> result = new ApiResultObject<HIS_MEST_PATY_TRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PATY_TRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMestPatyTrtyUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MEST_PATY_TRTY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MEST_PATY_TRTY> result = new ApiResultObject<HIS_MEST_PATY_TRTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PATY_TRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestPatyTrtyLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_PATY_TRTY> Lock(long id)
        {
            ApiResultObject<HIS_MEST_PATY_TRTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PATY_TRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestPatyTrtyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_PATY_TRTY> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_PATY_TRTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PATY_TRTY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMestPatyTrtyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestPatyTrtyTruncate(param).Truncate(id);
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
