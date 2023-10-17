using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisMrCheckSummary
{
    public partial class HisMrCheckSummaryManager : BusinessBase
    {
        public HisMrCheckSummaryManager()
            : base()
        {

        }
        
        public HisMrCheckSummaryManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MR_CHECK_SUMMARY>> Get(HisMrCheckSummaryFilterQuery filter)
        {
            ApiResultObject<List<HIS_MR_CHECK_SUMMARY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MR_CHECK_SUMMARY> resultData = null;
                if (valid)
                {
                    resultData = new HisMrCheckSummaryGet(param).Get(filter);
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
        public ApiResultObject<HIS_MR_CHECK_SUMMARY> Create(HIS_MR_CHECK_SUMMARY data)
        {
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_SUMMARY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckSummaryCreate(param).Create(data);
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
        public ApiResultObject<HIS_MR_CHECK_SUMMARY> Update(HIS_MR_CHECK_SUMMARY data)
        {
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MR_CHECK_SUMMARY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisMrCheckSummaryUpdate(param).Update(data);
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
        public ApiResultObject<HIS_MR_CHECK_SUMMARY> ChangeLock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = new ApiResultObject<HIS_MR_CHECK_SUMMARY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_SUMMARY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckSummaryLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_SUMMARY> Lock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_SUMMARY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckSummaryLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MR_CHECK_SUMMARY> Unlock(long id)
        {
            ApiResultObject<HIS_MR_CHECK_SUMMARY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MR_CHECK_SUMMARY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckSummaryLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMrCheckSummaryTruncate(param).Truncate(id);
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
        public ApiResultObject<MrCheckSummarySDO> CreateOrUpdate(MrCheckSummarySDO data)
        {
            ApiResultObject<MrCheckSummarySDO> result = new ApiResultObject<MrCheckSummarySDO>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                MrCheckSummarySDO resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisMrCheckSummaryCreateOrUpdate(param).Run(data, ref resultData);
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
    }
}
