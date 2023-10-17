using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMrChecklist
{
    public partial class HisMrChecklistManager : BusinessBase
    {
        public HisMrChecklistManager()
            : base()
        {

        }
        
        public HisMrChecklistManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MR_CHECKLIST>> Get(HisMrChecklistFilterQuery filter)
        {
            ApiResultObject<List<HIS_MR_CHECKLIST>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MR_CHECKLIST> resultData = null;
                if (valid)
                {
                    resultData = new HisMrChecklistGet(param).Get(filter);
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
        public ApiResultObject<HIS_MR_CHECKLIST> Create(HIS_MR_CHECKLIST data)
        {
            ApiResultObject<HIS_MR_CHECKLIST> result = new ApiResultObject<HIS_MR_CHECKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECKLIST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrChecklistCreate(param).Create(data);
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
        public ApiResultObject<HIS_MR_CHECKLIST> Update(HIS_MR_CHECKLIST data)
        {
            ApiResultObject<HIS_MR_CHECKLIST> result = new ApiResultObject<HIS_MR_CHECKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECKLIST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrChecklistUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MR_CHECKLIST> ChangeLock(long id)
        {
            ApiResultObject<HIS_MR_CHECKLIST> result = new ApiResultObject<HIS_MR_CHECKLIST>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECKLIST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrChecklistLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECKLIST> Lock(long id)
        {
            ApiResultObject<HIS_MR_CHECKLIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECKLIST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrChecklistLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECKLIST> Unlock(long id)
        {
            ApiResultObject<HIS_MR_CHECKLIST> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECKLIST resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrChecklistLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMrChecklistTruncate(param).Truncate(id);
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
