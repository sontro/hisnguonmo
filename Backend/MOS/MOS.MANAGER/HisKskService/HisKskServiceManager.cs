using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisKskService
{
    public partial class HisKskServiceManager : BusinessBase
    {
        public HisKskServiceManager()
            : base()
        {

        }
        
        public HisKskServiceManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_KSK_SERVICE>> Get(HisKskServiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisKskServiceGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_SERVICE> Create(HIS_KSK_SERVICE data)
        {
            ApiResultObject<HIS_KSK_SERVICE> result = new ApiResultObject<HIS_KSK_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskServiceCreate(param).Create(data);
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
        public ApiResultObject<HIS_KSK_SERVICE> Update(HIS_KSK_SERVICE data)
        {
            ApiResultObject<HIS_KSK_SERVICE> result = new ApiResultObject<HIS_KSK_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisKskServiceUpdate(param).Update(data);
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
        public ApiResultObject<List<HIS_KSK_SERVICE>> CreateList(List<HIS_KSK_SERVICE> listData)
        {
            ApiResultObject<List<HIS_KSK_SERVICE>> result = new ApiResultObject<List<HIS_KSK_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_KSK_SERVICE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskServiceCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_KSK_SERVICE>> UpdateList(List<HIS_KSK_SERVICE> listData)
        {
            ApiResultObject<List<HIS_KSK_SERVICE>> result = new ApiResultObject<List<HIS_KSK_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_KSK_SERVICE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskServiceUpdate(param).UpdateList(listData);
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
        public ApiResultObject<HIS_KSK_SERVICE> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_SERVICE> result = new ApiResultObject<HIS_KSK_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskServiceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_SERVICE> Lock(long id)
        {
            ApiResultObject<HIS_KSK_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskServiceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_SERVICE> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_SERVICE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_SERVICE resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskServiceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskServiceTruncate(param).Truncate(id);
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
                    resultData = new HisKskServiceTruncate(param).TruncateList(ids);
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
