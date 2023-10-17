using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisCaroAccountBook
{
    public partial class HisCaroAccountBookManager : BusinessBase
    {
        public HisCaroAccountBookManager()
            : base()
        {

        }
        
        public HisCaroAccountBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> Get(HisCaroAccountBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_CARO_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisCaroAccountBookGet(param).Get(filter);
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
        public ApiResultObject<HIS_CARO_ACCOUNT_BOOK> Create(HIS_CARO_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_CARO_ACCOUNT_BOOK> result = new ApiResultObject<HIS_CARO_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCaroAccountBookCreate(param).Create(data);
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
        public ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> CreateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_CARO_ACCOUNT_BOOK> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCaroAccountBookCreate(param).CreateList(listData);
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
        public ApiResultObject<HIS_CARO_ACCOUNT_BOOK> Update(HIS_CARO_ACCOUNT_BOOK data)
        {
            ApiResultObject<HIS_CARO_ACCOUNT_BOOK> result = new ApiResultObject<HIS_CARO_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_CARO_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisCaroAccountBookUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> UpdateList(List<HIS_CARO_ACCOUNT_BOOK> listData)
        {
            ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(listData);
                List<HIS_CARO_ACCOUNT_BOOK> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCaroAccountBookUpdate(param).UpdateList(listData);
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
        public ApiResultObject<HIS_CARO_ACCOUNT_BOOK> ChangeLock(long id)
        {
            ApiResultObject<HIS_CARO_ACCOUNT_BOOK> result = new ApiResultObject<HIS_CARO_ACCOUNT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARO_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCaroAccountBookLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_CARO_ACCOUNT_BOOK> Lock(long id)
        {
            ApiResultObject<HIS_CARO_ACCOUNT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARO_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCaroAccountBookLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_CARO_ACCOUNT_BOOK> Unlock(long id)
        {
            ApiResultObject<HIS_CARO_ACCOUNT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_CARO_ACCOUNT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisCaroAccountBookLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisCaroAccountBookTruncate(param).Truncate(id);
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
                    resultData = new HisCaroAccountBookTruncate(param).TruncateList(ids);
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
        public ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> CopyByCashierRoom(HisCaroAcboCopyByCashierRoomSDO data)
        {
            ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CARO_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    new HisCaroAccountBookCopyByCashierRoom(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> CopyByAccountBook(HisCaroAcboCopyByAccountBookSDO data)
        {
            ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>> result = new ApiResultObject<List<HIS_CARO_ACCOUNT_BOOK>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_CARO_ACCOUNT_BOOK> resultData = null;
                if (valid)
                {
                    new HisCaroAccountBookCopyByAccountBook(param).Run(data, ref resultData);
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
