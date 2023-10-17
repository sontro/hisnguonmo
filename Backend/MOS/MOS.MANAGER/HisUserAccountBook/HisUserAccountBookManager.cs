using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisUserAccountBook
{
    public partial class HisUserAccountBookManager : BusinessBase
    {
        public HisUserAccountBookManager()
            : base()
        {

        }
        
        public HisUserAccountBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> Get(HisUserAccountBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_USER_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisUserAccountBookGet(param).Get(filter);
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
        public ApiResultObject<HIS_USER_ACCOUNT_BOOK> Create(HIS_USER_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_USER_ACCOUNT_BOOK> result = new ApiResultObject<HIS_USER_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserAccountBookCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> CreateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_USER_ACCOUNT_BOOK> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserAccountBookCreate(param).CreateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<HIS_USER_ACCOUNT_BOOK> Update(HIS_USER_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_USER_ACCOUNT_BOOK> result = new ApiResultObject<HIS_USER_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_USER_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisUserAccountBookUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> UpdateList(List<HIS_USER_ACCOUNT_BOOK> listData)
        {
            ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_USER_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_USER_ACCOUNT_BOOK> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserAccountBookUpdate(param).UpdateList(listData);
                    resultData = isSuccess ? listData : null;
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
        public ApiResultObject<HIS_USER_ACCOUNT_BOOK> ChangeLock(long id)
        {
            ApiResultObject<HIS_USER_ACCOUNT_BOOK> result = new ApiResultObject<HIS_USER_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserAccountBookLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_ACCOUNT_BOOK> Lock(long id)
        {
            ApiResultObject<HIS_USER_ACCOUNT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserAccountBookLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_USER_ACCOUNT_BOOK> Unlock(long id)
        {
            ApiResultObject<HIS_USER_ACCOUNT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_USER_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisUserAccountBookLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisUserAccountBookTruncate(param).Truncate(id);
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
        public ApiResultObject<bool> DeleteList(List<long> ids)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisUserAccountBookTruncate(param).TruncateList(ids);
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
