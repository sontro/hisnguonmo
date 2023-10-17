using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdCm
{
    public partial class HisIcdCmManager : BusinessBase
    {
        public HisIcdCmManager()
            : base()
        {

        }
        
        public HisIcdCmManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ICD_CM>> Get(HisIcdCmFilterQuery filter)
        {
            ApiResultObject<List<HIS_ICD_CM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ICD_CM> resultData = null;
                if (valid)
                {
                    resultData = new HisIcdCmGet(param).Get(filter);
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
        public ApiResultObject<HIS_ICD_CM> Create(HIS_ICD_CM data)
        {
            ApiResultObject<HIS_ICD_CM> result = new ApiResultObject<HIS_ICD_CM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD_CM resultData = null;
                if (valid && new HisIcdCmCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<List<HIS_ICD_CM>> CreateList(List<HIS_ICD_CM> data)
        {
            ApiResultObject<List<HIS_ICD_CM>> result = new ApiResultObject<List<HIS_ICD_CM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_ICD_CM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdCmCreate(param).CreateList(data);
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
        public ApiResultObject<HIS_ICD_CM> Update(HIS_ICD_CM data)
        {
            ApiResultObject<HIS_ICD_CM> result = new ApiResultObject<HIS_ICD_CM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD_CM resultData = null;
                if (valid && new HisIcdCmUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_ICD_CM> ChangeLock(long id)
        {
            ApiResultObject<HIS_ICD_CM> result = new ApiResultObject<HIS_ICD_CM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_CM resultData = null;
                if (valid)
                {
                    new HisIcdCmLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ICD_CM> Lock(long id)
        {
            ApiResultObject<HIS_ICD_CM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_CM resultData = null;
                if (valid)
                {
                    new HisIcdCmLock(param).Lock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_ICD_CM> Unlock(long id)
        {
            ApiResultObject<HIS_ICD_CM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_CM resultData = null;
                if (valid)
                {
                    new HisIcdCmLock(param).Unlock(id, ref resultData);
                }
                result = this.PackSingleResult(resultData);
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
                    resultData = new HisIcdCmTruncate(param).Truncate(id);
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
