using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMetyProduct
{
    public partial class HisMetyProductManager : BusinessBase
    {
        public HisMetyProductManager()
            : base()
        {

        }
        
        public HisMetyProductManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_METY_PRODUCT>> Get(HisMetyProductFilterQuery filter)
        {
            ApiResultObject<List<HIS_METY_PRODUCT>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_METY_PRODUCT> resultData = null;
                if (valid)
                {
                    resultData = new HisMetyProductGet(param).Get(filter);
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
        public ApiResultObject<HIS_METY_PRODUCT> Create(HIS_METY_PRODUCT data)
        {
            ApiResultObject<HIS_METY_PRODUCT> result = new ApiResultObject<HIS_METY_PRODUCT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_PRODUCT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMetyProductCreate(param).Create(data);
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
        public ApiResultObject<HIS_METY_PRODUCT> Update(HIS_METY_PRODUCT data)
        {
            ApiResultObject<HIS_METY_PRODUCT> result = new ApiResultObject<HIS_METY_PRODUCT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_METY_PRODUCT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMetyProductUpdate(param).Update(data);
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
        public ApiResultObject<HIS_METY_PRODUCT> ChangeLock(long id)
        {
            ApiResultObject<HIS_METY_PRODUCT> result = new ApiResultObject<HIS_METY_PRODUCT>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_PRODUCT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyProductLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_PRODUCT> Lock(long id)
        {
            ApiResultObject<HIS_METY_PRODUCT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_PRODUCT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyProductLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_METY_PRODUCT> Unlock(long id)
        {
            ApiResultObject<HIS_METY_PRODUCT> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_METY_PRODUCT resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMetyProductLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMetyProductTruncate(param).Truncate(id);
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
