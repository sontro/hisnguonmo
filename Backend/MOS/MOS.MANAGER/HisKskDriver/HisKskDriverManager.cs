using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisKskDriver.Create;
using MOS.MANAGER.HisKskDriver.Update;
using MOS.MANAGER.HisKskDriver.Sync;

namespace MOS.MANAGER.HisKskDriver
{
    public partial class HisKskDriverManager : BusinessBase
    {
        public HisKskDriverManager()
            : base()
        {

        }

        public HisKskDriverManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_KSK_DRIVER>> Get(HisKskDriverFilterQuery filter)
        {
            ApiResultObject<List<HIS_KSK_DRIVER>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_KSK_DRIVER> resultData = null;
                if (valid)
                {
                    resultData = new HisKskDriverGet(param).Get(filter);
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
        public ApiResultObject<HIS_KSK_DRIVER> ChangeLock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_DRIVER> Lock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_KSK_DRIVER> Unlock(long id)
        {
            ApiResultObject<HIS_KSK_DRIVER> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_KSK_DRIVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisKskDriverTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_KSK_DRIVER> Create(HisKskDriverSDO data)
        {
            ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverCreateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_KSK_DRIVER> Update(HisKskDriverSDO data)
        {
            ApiResultObject<HIS_KSK_DRIVER> result = new ApiResultObject<HIS_KSK_DRIVER>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_KSK_DRIVER resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisKskDriverUpdateSdo(param).Run(data, ref resultData);
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
        public ApiResultObject<bool> Sync(List<KskDriverSyncSDO> data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisKskDriverSync(param).Run(data);
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
