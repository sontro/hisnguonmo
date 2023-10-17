using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTreatmentFile
{
    public partial class HisTreatmentFileManager : BusinessBase
    {
        public HisTreatmentFileManager()
            : base()
        {

        }
        
        public HisTreatmentFileManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TREATMENT_FILE>> Get(HisTreatmentFileFilterQuery filter)
        {
            ApiResultObject<List<HIS_TREATMENT_FILE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TREATMENT_FILE> resultData = null;
                if (valid)
                {
                    resultData = new HisTreatmentFileGet(param).Get(filter);
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
        public ApiResultObject<HIS_TREATMENT_FILE> Create(HIS_TREATMENT_FILE data)
        {
            ApiResultObject<HIS_TREATMENT_FILE> result = new ApiResultObject<HIS_TREATMENT_FILE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_FILE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentFileCreate(param).Create(data);
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
        public ApiResultObject<HIS_TREATMENT_FILE> Update(HIS_TREATMENT_FILE data)
        {
            ApiResultObject<HIS_TREATMENT_FILE> result = new ApiResultObject<HIS_TREATMENT_FILE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TREATMENT_FILE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTreatmentFileUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TREATMENT_FILE> ChangeLock(long id)
        {
            ApiResultObject<HIS_TREATMENT_FILE> result = new ApiResultObject<HIS_TREATMENT_FILE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_FILE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentFileLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_FILE> Lock(long id)
        {
            ApiResultObject<HIS_TREATMENT_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_FILE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentFileLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TREATMENT_FILE> Unlock(long id)
        {
            ApiResultObject<HIS_TREATMENT_FILE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TREATMENT_FILE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTreatmentFileLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTreatmentFileTruncate(param).Truncate(id);
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
