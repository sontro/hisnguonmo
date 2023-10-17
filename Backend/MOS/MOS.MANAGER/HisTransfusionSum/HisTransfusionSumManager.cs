using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using MOS.SDO;
using MOS.MANAGER.HisTransfusionSum.CreateOrUpdateSdo;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransfusionSum
{
    public partial class HisTransfusionSumManager : BusinessBase
    {
        public HisTransfusionSumManager()
            : base()
        {

        }
        
        public HisTransfusionSumManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRANSFUSION_SUM>> Get(HisTransfusionSumFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANSFUSION_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANSFUSION_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisTransfusionSumGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> Create(HIS_TRANSFUSION_SUM data)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = new ApiResultObject<HIS_TRANSFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION_SUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransfusionSumCreate(param).Create(data);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> Update(HIS_TRANSFUSION_SUM data)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = new ApiResultObject<HIS_TRANSFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION_SUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransfusionSumUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> CreateOrUpdateSdo(HisTransfusionSumSDO data)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = new ApiResultObject<HIS_TRANSFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSFUSION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionSumCreateOrUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> ChangeLock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = new ApiResultObject<HIS_TRANSFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION_SUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionSumLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> Lock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION_SUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionSumLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSFUSION_SUM> Unlock(long id)
        {
            ApiResultObject<HIS_TRANSFUSION_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSFUSION_SUM resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransfusionSumLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTransfusionSumTruncate(param).Truncate(id);
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
