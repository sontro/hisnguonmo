using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisRationSum.Create;
using MOS.MANAGER.HisRationSum.Truncate;
using MOS.MANAGER.HisRationSum.Approve;
using MOS.MANAGER.HisRationSum.Unapprove;
using MOS.MANAGER.HisRationSum.Reject;
using MOS.MANAGER.HisRationSum.Unreject;
using MOS.MANAGER.HisRationSum.Remove;

namespace MOS.MANAGER.HisRationSum
{
    public partial class HisRationSumManager : BusinessBase
    {
        public HisRationSumManager()
            : base()
        {

        }

        public HisRationSumManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_RATION_SUM>> Get(HisRationSumFilterQuery filter)
        {
            ApiResultObject<List<HIS_RATION_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_RATION_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisRationSumGet(param).Get(filter);
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
        public ApiResultObject<List<HIS_RATION_SUM>> Create(HisRationSumSDO data)
        {
            ApiResultObject<List<HIS_RATION_SUM>> result = new ApiResultObject<List<HIS_RATION_SUM>>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                List<HIS_RATION_SUM> resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumCreateSDO(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Approve(HisRationSumUpdateSDO data)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumApprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Unapprove(HisRationSumUpdateSDO data)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumUnapprove(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Reject(HisRationSumUpdateSDO data)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumReject(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Unreject(HisRationSumUpdateSDO data)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumUnreject(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Update(HIS_RATION_SUM data)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumUpdate(param).Update(data);
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
        public ApiResultObject<HIS_RATION_SUM> ChangeLock(long id)
        {
            ApiResultObject<HIS_RATION_SUM> result = new ApiResultObject<HIS_RATION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Lock(long id)
        {
            ApiResultObject<HIS_RATION_SUM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_RATION_SUM> Unlock(long id)
        {
            ApiResultObject<HIS_RATION_SUM> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_RATION_SUM resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisRationSumLock(param).Unlock(id, ref resultData);
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
        public ApiResultObject<bool> Delete(HisRationSumSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRationSumTruncateSDO(param).Run(data);
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
        public ApiResultObject<bool> Remove(HisRationSumUpdateSDO data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisRationSumRemove(param).Run(data);
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
