using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;
using MOS.SDO;
using MOS.MANAGER.HisHoldReturn.Return;

namespace MOS.MANAGER.HisHoldReturn
{
    public partial class HisHoldReturnManager : BusinessBase
    {
        public HisHoldReturnManager()
            : base()
        {

        }

        public HisHoldReturnManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_HOLD_RETURN>> Get(HisHoldReturnFilterQuery filter)
        {
            ApiResultObject<List<HIS_HOLD_RETURN>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_HOLD_RETURN> resultData = null;
                if (valid)
                {
                    resultData = new HisHoldReturnGet(param).Get(filter);
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
        public ApiResultObject<HIS_HOLD_RETURN> Create(HisHoldReturnCreateSDO data)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoldReturnCreateSdo(param).Create(data, ref resultData);
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
        public ApiResultObject<HIS_HOLD_RETURN> Update(HisHoldReturnUpdateSDO data)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoldReturnUpdateSdo(param).Update(data, ref resultData);
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
        public ApiResultObject<HIS_HOLD_RETURN> ChangeLock(long id)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoldReturnLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_HOLD_RETURN> Lock(long id)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoldReturnLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_HOLD_RETURN> Unlock(long id)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisHoldReturnLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisHoldReturnTruncate(param).Truncate(id);
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
        public ApiResultObject<HIS_HOLD_RETURN> Return(HisHoldReturnSDO data)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    new HisHoldReturnReturn(param).Run(data, ref resultData);
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
        public ApiResultObject<HIS_HOLD_RETURN> CancelReturn(HisHoldReturnSDO data)
        {
            ApiResultObject<HIS_HOLD_RETURN> result = new ApiResultObject<HIS_HOLD_RETURN>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_HOLD_RETURN resultData = null;
                bool isSuccess = false;
                if (valid)
                {
                    new HisHoldReturnCancelReturn(param).Run(data, ref resultData);
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
    }
}
