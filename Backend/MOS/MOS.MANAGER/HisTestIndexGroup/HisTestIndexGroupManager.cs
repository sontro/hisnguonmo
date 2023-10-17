using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTestIndexGroup
{
    public partial class HisTestIndexGroupManager : BusinessBase
    {
        public HisTestIndexGroupManager()
            : base()
        {

        }
        
        public HisTestIndexGroupManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TEST_INDEX_GROUP>> Get(HisTestIndexGroupFilterQuery filter)
        {
            ApiResultObject<List<HIS_TEST_INDEX_GROUP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TEST_INDEX_GROUP> resultData = null;
                if (valid)
                {
                    resultData = new HisTestIndexGroupGet(param).Get(filter);
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
        public ApiResultObject<HIS_TEST_INDEX_GROUP> Create(HIS_TEST_INDEX_GROUP data)
        {
            ApiResultObject<HIS_TEST_INDEX_GROUP> result = new ApiResultObject<HIS_TEST_INDEX_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTestIndexGroupCreate(param).Create(data);
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
        public ApiResultObject<HIS_TEST_INDEX_GROUP> Update(HIS_TEST_INDEX_GROUP data)
        {
            ApiResultObject<HIS_TEST_INDEX_GROUP> result = new ApiResultObject<HIS_TEST_INDEX_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TEST_INDEX_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTestIndexGroupUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TEST_INDEX_GROUP> ChangeLock(long id)
        {
            ApiResultObject<HIS_TEST_INDEX_GROUP> result = new ApiResultObject<HIS_TEST_INDEX_GROUP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_INDEX_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestIndexGroupLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TEST_INDEX_GROUP> Lock(long id)
        {
            ApiResultObject<HIS_TEST_INDEX_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_INDEX_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestIndexGroupLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TEST_INDEX_GROUP> Unlock(long id)
        {
            ApiResultObject<HIS_TEST_INDEX_GROUP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TEST_INDEX_GROUP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTestIndexGroupLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTestIndexGroupTruncate(param).Truncate(id);
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
