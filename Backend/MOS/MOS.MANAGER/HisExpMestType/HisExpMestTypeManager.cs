using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisExpMestType
{
    public class HisExpMestTypeManager : BusinessBase
    {
        public HisExpMestTypeManager()
            : base()
        {

        }

        public HisExpMestTypeManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_EXP_MEST_TYPE>> Get(HisExpMestTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_EXP_MEST_TYPE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_EXP_MEST_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisExpMestTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_EXP_MEST_TYPE> Create(HIS_EXP_MEST_TYPE data)
        {
            ApiResultObject<HIS_EXP_MEST_TYPE> result = new ApiResultObject<HIS_EXP_MEST_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_TYPE resultData = null;
                if (valid && new HisExpMestTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_EXP_MEST_TYPE> Update(HIS_EXP_MEST_TYPE data)
        {
            ApiResultObject<HIS_EXP_MEST_TYPE> result = new ApiResultObject<HIS_EXP_MEST_TYPE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_EXP_MEST_TYPE resultData = null;
                if (valid && new HisExpMestTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_EXP_MEST_TYPE> ChangeLock(HIS_EXP_MEST_TYPE data)
        {
            ApiResultObject<HIS_EXP_MEST_TYPE> result = new ApiResultObject<HIS_EXP_MEST_TYPE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    HisExpMestTypeLock lockConcrete = new HisExpMestTypeLock(param);
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
        public ApiResultObject<bool> Delete(HIS_EXP_MEST_TYPE data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisExpMestTypeTruncate deleteConcrete = new HisExpMestTypeTruncate(param);
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
