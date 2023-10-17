using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;

namespace MOS.MANAGER.HisServiceRati
{
    public partial class HisServiceRatiManager : BusinessBase
    {
        public HisServiceRatiManager()
            : base()
        {

        }

        public HisServiceRatiManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_SERVICE_RATI>> Get(HisServiceRatiFilterQuery filter)
        {
            ApiResultObject<List<HIS_SERVICE_RATI>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SERVICE_RATI> resultData = null;
                if (valid)
                {
                    resultData = new HisServiceRatiGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_SERVICE_RATI>> Create(HisServiceRatiSDO data)
        {
            ApiResultObject<List<HIS_SERVICE_RATI>> result = new ApiResultObject<List<HIS_SERVICE_RATI>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_RATI> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRatiCreateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<List<HIS_SERVICE_RATI>> CreateList(List<HIS_SERVICE_RATI> data)
        {
            ApiResultObject<List<HIS_SERVICE_RATI>> result = new ApiResultObject<List<HIS_SERVICE_RATI>>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_SERVICE_RATI> resultData = null;
                if (valid && new HisServiceRatiCreate(param).CreateList(data))
                {
                    resultData = data;
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_SERVICE_RATI> Update(HIS_SERVICE_RATI data)
        {
            ApiResultObject<HIS_SERVICE_RATI> result = new ApiResultObject<HIS_SERVICE_RATI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SERVICE_RATI resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRatiUpdate(param).Update(data);
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
        public ApiResultObject<HIS_SERVICE_RATI> ChangeLock(long id)
        {
            ApiResultObject<HIS_SERVICE_RATI> result = new ApiResultObject<HIS_SERVICE_RATI>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RATI resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRatiLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_RATI> Lock(long id)
        {
            ApiResultObject<HIS_SERVICE_RATI> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RATI resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRatiLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SERVICE_RATI> Unlock(long id)
        {
            ApiResultObject<HIS_SERVICE_RATI> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SERVICE_RATI resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisServiceRatiLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisServiceRatiTruncate(param).Truncate(id);
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
                    resultData = new HisServiceRatiTruncate(param).TruncateList(ids);
                }
                result = this.PackSingleResult(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
    }
}
