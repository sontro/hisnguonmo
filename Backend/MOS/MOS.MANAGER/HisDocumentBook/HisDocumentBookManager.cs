using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDocumentBook
{
    public partial class HisDocumentBookManager : BusinessBase
    {
        public HisDocumentBookManager()
            : base()
        {

        }
        
        public HisDocumentBookManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DOCUMENT_BOOK>> Get(HisDocumentBookFilterQuery filter)
        {
            ApiResultObject<List<HIS_DOCUMENT_BOOK>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DOCUMENT_BOOK> resultData = null;
                if (valid)
                {
                    resultData = new HisDocumentBookGet(param).Get(filter);
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
        public ApiResultObject<HIS_DOCUMENT_BOOK> Create(HIS_DOCUMENT_BOOK data)
        {
            ApiResultObject<HIS_DOCUMENT_BOOK> result = new ApiResultObject<HIS_DOCUMENT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DOCUMENT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDocumentBookCreate(param).Create(data);
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
        public ApiResultObject<HIS_DOCUMENT_BOOK> Update(HIS_DOCUMENT_BOOK data)
        {
            ApiResultObject<HIS_DOCUMENT_BOOK> result = new ApiResultObject<HIS_DOCUMENT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DOCUMENT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDocumentBookUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DOCUMENT_BOOK> ChangeLock(long id)
        {
            ApiResultObject<HIS_DOCUMENT_BOOK> result = new ApiResultObject<HIS_DOCUMENT_BOOK>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOCUMENT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDocumentBookLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DOCUMENT_BOOK> Lock(long id)
        {
            ApiResultObject<HIS_DOCUMENT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOCUMENT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDocumentBookLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DOCUMENT_BOOK> Unlock(long id)
        {
            ApiResultObject<HIS_DOCUMENT_BOOK> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DOCUMENT_BOOK resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDocumentBookLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDocumentBookTruncate(param).Truncate(id);
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
