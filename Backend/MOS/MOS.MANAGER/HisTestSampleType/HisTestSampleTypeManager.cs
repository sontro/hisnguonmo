using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestSampleType
{
    public partial class HisTestSampleTypeManager : BusinessBase
    {
        public HisTestSampleTypeManager()
            : base()
        {

        }
        
        public HisTestSampleTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TEST_SAMPLE_TYPE>> Get(HisTestSampleTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_TEST_SAMPLE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TEST_SAMPLE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisTestSampleTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_TEST_SAMPLE_TYPE> Create(HIS_TEST_SAMPLE_TYPE data)
        {
            ApiResultObject<HIS_TEST_SAMPLE_TYPE> result = new ApiResultObject<HIS_TEST_SAMPLE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_SAMPLE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTestSampleTypeCreate(param).Create(data);
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
        public ApiResultObject<HIS_TEST_SAMPLE_TYPE> Update(HIS_TEST_SAMPLE_TYPE data)
        {
            ApiResultObject<HIS_TEST_SAMPLE_TYPE> result = new ApiResultObject<HIS_TEST_SAMPLE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_SAMPLE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTestSampleTypeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TEST_SAMPLE_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_TEST_SAMPLE_TYPE> result = new ApiResultObject<HIS_TEST_SAMPLE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_SAMPLE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestSampleTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TEST_SAMPLE_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_TEST_SAMPLE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_SAMPLE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestSampleTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TEST_SAMPLE_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_TEST_SAMPLE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_SAMPLE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestSampleTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTestSampleTypeTruncate(param).Truncate(id);
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
