using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestStt
{
    public class HisImpMestSttManager : BusinessBase
    {
        public HisImpMestSttManager()
            : base()
        {

        }

        public HisImpMestSttManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST_STT>> Get(HisImpMestSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_STT>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestSttGet(param).Get(filter);
                }
                result = this.PackSuccess(resultData);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
                result = null;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<HIS_IMP_MEST_STT> Create(HIS_IMP_MEST_STT data)
        {
            ApiResultObject<HIS_IMP_MEST_STT> result = new ApiResultObject<HIS_IMP_MEST_STT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_STT resultData = null;
                if (valid && new HisImpMestSttCreate(param).Create(data))
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
        public ApiResultObject<HIS_IMP_MEST_STT> Update(HIS_IMP_MEST_STT data)
        {
            ApiResultObject<HIS_IMP_MEST_STT> result = new ApiResultObject<HIS_IMP_MEST_STT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_STT resultData = null;
                if (valid && new HisImpMestSttUpdate(param).Update(data))
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
        public ApiResultObject<HIS_IMP_MEST_STT> ChangeLock(HIS_IMP_MEST_STT data)
        {
            ApiResultObject<HIS_IMP_MEST_STT> result = new ApiResultObject<HIS_IMP_MEST_STT>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    HisImpMestSttLock lockConcrete = new HisImpMestSttLock(param);
                    if (lockConcrete.ChangeLock(data.ID))
                    {
                        result = lockConcrete.PackSingleResult(data);
                    }
                }
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }

        [Logger]
        public ApiResultObject<bool> Delete(HIS_IMP_MEST_STT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisImpMestSttTruncate deleteConcrete = new HisImpMestSttTruncate(param);
                    result = deleteConcrete.PackSingleResult(deleteConcrete.Truncate(data));
                }
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
