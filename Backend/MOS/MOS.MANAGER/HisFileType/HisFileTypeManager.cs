using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisFileType
{
    public partial class HisFileTypeManager : BusinessBase
    {
        public HisFileTypeManager()
            : base()
        {

        }
        
        public HisFileTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_FILE_TYPE>> Get(HisFileTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_FILE_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_FILE_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisFileTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_FILE_TYPE> Create(HIS_FILE_TYPE data)
        {
            ApiResultObject<HIS_FILE_TYPE> result = new ApiResultObject<HIS_FILE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FILE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFileTypeCreate(param).Create(data);
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
        public ApiResultObject<HIS_FILE_TYPE> Update(HIS_FILE_TYPE data)
        {
            ApiResultObject<HIS_FILE_TYPE> result = new ApiResultObject<HIS_FILE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_FILE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisFileTypeUpdate(param).Update(data);
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
        public ApiResultObject<HIS_FILE_TYPE> ChangeLock(long id)
        {
            ApiResultObject<HIS_FILE_TYPE> result = new ApiResultObject<HIS_FILE_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFileTypeLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_FILE_TYPE> Lock(long id)
        {
            ApiResultObject<HIS_FILE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFileTypeLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_FILE_TYPE> Unlock(long id)
        {
            ApiResultObject<HIS_FILE_TYPE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_FILE_TYPE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisFileTypeLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisFileTypeTruncate(param).Truncate(id);
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
