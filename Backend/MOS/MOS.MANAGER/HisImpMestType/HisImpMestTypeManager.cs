using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestType
{
    public class HisImpMestTypeManager : BusinessBase
    {
        public HisImpMestTypeManager()
            : base()
        {

        }

        public HisImpMestTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_IMP_MEST_TYPE>> Get(HisImpMestTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_TYPE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_MEST_TYPE> Create(HIS_IMP_MEST_TYPE data)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE> result = new ApiResultObject<HIS_IMP_MEST_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_TYPE resultData = null;
                if (valid && new HisImpMestTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_IMP_MEST_TYPE> Update(HIS_IMP_MEST_TYPE data)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE> result = new ApiResultObject<HIS_IMP_MEST_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_TYPE resultData = null;
                if (valid && new HisImpMestTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_IMP_MEST_TYPE> ChangeLock(HIS_IMP_MEST_TYPE data)
        {
            ApiResultObject<HIS_IMP_MEST_TYPE> result = new ApiResultObject<HIS_IMP_MEST_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    HisImpMestTypeLock lockConcrete = new HisImpMestTypeLock(param);
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
        public ApiResultObject<bool> Delete(HIS_IMP_MEST_TYPE data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisImpMestTypeTruncate deleteConcrete = new HisImpMestTypeTruncate(param);
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
