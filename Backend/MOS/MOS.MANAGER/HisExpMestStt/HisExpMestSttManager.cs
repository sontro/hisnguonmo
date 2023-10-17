using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestStt
{
    public class HisExpMestSttManager : BusinessBase
    {
        public HisExpMestSttManager()
            : base()
        {

        }

        public HisExpMestSttManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST_STT>> Get(HisExpMestSttFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_STT>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_STT> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestSttGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_STT> Create(HIS_EXP_MEST_STT data)
        {
            ApiResultObject<HIS_EXP_MEST_STT> result = new ApiResultObject<HIS_EXP_MEST_STT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_STT resultData = null;
                if (valid && new HisExpMestSttCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXP_MEST_STT> Update(HIS_EXP_MEST_STT data)
        {
            ApiResultObject<HIS_EXP_MEST_STT> result = new ApiResultObject<HIS_EXP_MEST_STT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_STT resultData = null;
                if (valid && new HisExpMestSttUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_STT> ChangeLock(HIS_EXP_MEST_STT data)
        {
            ApiResultObject<HIS_EXP_MEST_STT> result = new ApiResultObject<HIS_EXP_MEST_STT>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    HisExpMestSttLock lockConcrete = new HisExpMestSttLock(param);
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
        public ApiResultObject<bool> Delete(HIS_EXP_MEST_STT data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisExpMestSttTruncate deleteConcrete = new HisExpMestSttTruncate(param);
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
