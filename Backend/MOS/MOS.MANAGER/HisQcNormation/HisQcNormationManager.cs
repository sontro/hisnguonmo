using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisQcNormation.Create;

namespace MOS.MANAGER.HisQcNormation
{
    public partial class HisQcNormationManager : BusinessBase
    {
        public HisQcNormationManager()
            : base()
        {

        }
        
        public HisQcNormationManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_QC_NORMATION>> Get(HisQcNormationFilterQuery filter)
        {
            ApiResultObject<List<HIS_QC_NORMATION>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_QC_NORMATION> resultData = null;
                if (valid)
                {
                    resultData = new HisQcNormationGet(param).Get(filter);
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
        public ApiResultObject<HIS_QC_NORMATION> Create(HIS_QC_NORMATION data)
        {
            ApiResultObject<HIS_QC_NORMATION> result = new ApiResultObject<HIS_QC_NORMATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_QC_NORMATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisQcNormationCreate(param).Create(data);
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
        public ApiResultObject<bool> CreateSdo(HisQcNormationSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisQcNormationCreateSdo(param).Run(data);
                }
                result = this.PackSingleResult(isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_QC_NORMATION> Update(HIS_QC_NORMATION data)
        {
            ApiResultObject<HIS_QC_NORMATION> result = new ApiResultObject<HIS_QC_NORMATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_QC_NORMATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisQcNormationUpdate(param).Update(data);
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
        public ApiResultObject<HIS_QC_NORMATION> ChangeLock(long id)
        {
            ApiResultObject<HIS_QC_NORMATION> result = new ApiResultObject<HIS_QC_NORMATION>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_QC_NORMATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisQcNormationLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_QC_NORMATION> Lock(long id)
        {
            ApiResultObject<HIS_QC_NORMATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_QC_NORMATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisQcNormationLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_QC_NORMATION> Unlock(long id)
        {
            ApiResultObject<HIS_QC_NORMATION> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_QC_NORMATION resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisQcNormationLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisQcNormationTruncate(param).Truncate(id);
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
