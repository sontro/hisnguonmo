using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisGender
{
    public class HisGenderManager : BusinessBase
    {
        public HisGenderManager()
            : base()
        {

        }

        public HisGenderManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_GENDER>> Get(HisGenderFilterQuery filter)
        {
            ApiResultObject<List<HIS_GENDER>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_GENDER> resultData = null;
                if (valid)
                {
                    resultData = new HisGenderGet(param).Get(filter);
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
        public ApiResultObject<HIS_GENDER> Create(HIS_GENDER data)
        {
            ApiResultObject<HIS_GENDER> result = new ApiResultObject<HIS_GENDER>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_GENDER resultData = null;
                if (valid && new HisGenderCreate(param).Create(data))
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
        public ApiResultObject<HIS_GENDER> Update(HIS_GENDER data)
        {
            ApiResultObject<HIS_GENDER> result = new ApiResultObject<HIS_GENDER>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_GENDER resultData = null;
                if (valid && new HisGenderUpdate(param).Update(data))
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
        public ApiResultObject<HIS_GENDER> ChangeLock(HIS_GENDER data)
        {
            ApiResultObject<HIS_GENDER> result = new ApiResultObject<HIS_GENDER>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                if (valid)
                {
                    HisGenderLock lockConcrete = new HisGenderLock(param);
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
        public ApiResultObject<bool> Delete(HIS_GENDER data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                if (valid)
                {
                    HisGenderTruncate deleteConcrete = new HisGenderTruncate(param);
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
