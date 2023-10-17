using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisIcdService
{
    public partial class HisIcdServiceManager : BusinessBase
    {
        public HisIcdServiceManager()
            : base()
        {

        }

        public HisIcdServiceManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_ICD_SERVICE>> Get(HisIcdServiceFilterQuery filter)
        {
            ApiResultObject<List<HIS_ICD_SERVICE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ICD_SERVICE> resultData = null;
                if (valid)
                {
                    resultData = new HisIcdServiceGet(param).Get(filter);
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
        public ApiResultObject<HIS_ICD_SERVICE> Create(HIS_ICD_SERVICE data)
        {
            ApiResultObject<HIS_ICD_SERVICE> result = new ApiResultObject<HIS_ICD_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD_SERVICE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceCreate(param).Create(data);
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
        public ApiResultObject<HIS_ICD_SERVICE> Update(HIS_ICD_SERVICE data)
        {
            ApiResultObject<HIS_ICD_SERVICE> result = new ApiResultObject<HIS_ICD_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ICD_SERVICE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ICD_SERVICE> ChangeLock(long id)
        {
            ApiResultObject<HIS_ICD_SERVICE> result = new ApiResultObject<HIS_ICD_SERVICE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_SERVICE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ICD_SERVICE> Lock(long id)
        {
            ApiResultObject<HIS_ICD_SERVICE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_SERVICE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ICD_SERVICE> Unlock(long id)
        {
            ApiResultObject<HIS_ICD_SERVICE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ICD_SERVICE resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisIcdServiceTruncate(param).Truncate(id);
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
        public ApiResultObject<List<HIS_ICD_SERVICE>> CreateList(List<HIS_ICD_SERVICE> listData)
        {
            ApiResultObject<List<HIS_ICD_SERVICE>> result = new ApiResultObject<List<HIS_ICD_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ICD_SERVICE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceCreate(param).CreateList(listData);
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
        public ApiResultObject<List<HIS_ICD_SERVICE>> UpdateList(List<HIS_ICD_SERVICE> listData)
        {
            ApiResultObject<List<HIS_ICD_SERVICE>> result = new ApiResultObject<List<HIS_ICD_SERVICE>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNullOrEmpty(listData);
                List<HIS_ICD_SERVICE> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisIcdServiceUpdate(param).UpdateList(listData);
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
        public ApiResultObject<bool> DeleteList(List<long> listId)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisIcdServiceTruncate(param).TruncateList(listId);
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
