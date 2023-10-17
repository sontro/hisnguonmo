using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisContraindication
{
    public partial class HisContraindicationManager : BusinessBase
    {
        public HisContraindicationManager()
            : base()
        {

        }
        
        public HisContraindicationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CONTRAINDICATION>> Get(HisContraindicationFilterQuery filter)
        {
            ApiResultObject<List<HIS_CONTRAINDICATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CONTRAINDICATION> resultData = null;
                if (valid)
                {
                    resultData = new HisContraindicationGet(param).Get(filter);
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
        public ApiResultObject<HIS_CONTRAINDICATION> Create(HIS_CONTRAINDICATION data)
        {
            ApiResultObject<HIS_CONTRAINDICATION> result = new ApiResultObject<HIS_CONTRAINDICATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTRAINDICATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContraindicationCreate(param).Create(data);
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
        public ApiResultObject<HIS_CONTRAINDICATION> Update(HIS_CONTRAINDICATION data)
        {
            ApiResultObject<HIS_CONTRAINDICATION> result = new ApiResultObject<HIS_CONTRAINDICATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CONTRAINDICATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisContraindicationUpdate(param).Update(data);
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
        public ApiResultObject<HIS_CONTRAINDICATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_CONTRAINDICATION> result = new ApiResultObject<HIS_CONTRAINDICATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTRAINDICATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContraindicationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTRAINDICATION> Lock(long id)
        {
            ApiResultObject<HIS_CONTRAINDICATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTRAINDICATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContraindicationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CONTRAINDICATION> Unlock(long id)
        {
            ApiResultObject<HIS_CONTRAINDICATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CONTRAINDICATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisContraindicationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisContraindicationTruncate(param).Truncate(id);
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
