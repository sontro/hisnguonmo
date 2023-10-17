using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathCause
{
    public class HisDeathCauseManager : BusinessBase
    {
        public HisDeathCauseManager()
            : base()
        {

        }

        public HisDeathCauseManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DEATH_CAUSE>> Get(HisDeathCauseFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEATH_CAUSE>> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEATH_CAUSE> resultData = null;
                if (valid)
                {
                    resultData = new HisDeathCauseGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEATH_CAUSE> Create(HIS_DEATH_CAUSE data)
        {
            ApiResultObject<HIS_DEATH_CAUSE> result = null;

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_CAUSE resultData = null;
                if (valid && new HisDeathCauseCreate(param).Create(data))
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
        public ApiResultObject<HIS_DEATH_CAUSE> Update(HIS_DEATH_CAUSE data)
        {
            ApiResultObject<HIS_DEATH_CAUSE> result = new ApiResultObject<HIS_DEATH_CAUSE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_CAUSE resultData = null;
                if (valid && new HisDeathCauseUpdate(param).Update(data))
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
        public ApiResultObject<HIS_DEATH_CAUSE> ChangeLock(HIS_DEATH_CAUSE data)
        {
            ApiResultObject<HIS_DEATH_CAUSE> result = new ApiResultObject<HIS_DEATH_CAUSE>(null);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DEATH_CAUSE resultData = null;
                if (valid && new HisDeathCauseLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_DEATH_CAUSE data)
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
                    resultData = new HisDeathCauseTruncate(param).Truncate(data);
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
