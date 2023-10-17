using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDeathWithin
{
    public class HisDeathWithinManager : BusinessBase
    {
        public HisDeathWithinManager()
            : base()
        {

        }
        
        public HisDeathWithinManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_DEATH_WITHIN>> Get(HisDeathWithinFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEATH_WITHIN>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEATH_WITHIN> resultData = null;
                if (valid)
                {
                    resultData = new HisDeathWithinGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEATH_WITHIN> GetById(long id)
        {
            ApiResultObject<HIS_DEATH_WITHIN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_DEATH_WITHIN resultData = null;
                if (valid)
                {
                    HisDeathWithinFilterQuery filter = new HisDeathWithinFilterQuery();
                    resultData = new HisDeathWithinGet(param).GetById(id, filter);
                }
                result = this.PackSingleResult(resultData);
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
        public ApiResultObject<HIS_DEATH_WITHIN> Create(HIS_DEATH_WITHIN data)
        {
            ApiResultObject<HIS_DEATH_WITHIN> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_WITHIN resultData = null;
                if (valid && new HisDeathWithinCreate(param).Create(data))
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
        public ApiResultObject<HIS_DEATH_WITHIN> Update(HIS_DEATH_WITHIN data)
        {
            ApiResultObject<HIS_DEATH_WITHIN> result = new ApiResultObject<HIS_DEATH_WITHIN>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEATH_WITHIN resultData = null;
                if (valid && new HisDeathWithinUpdate(param).Update(data))
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
        public ApiResultObject<HIS_DEATH_WITHIN> ChangeLock(HIS_DEATH_WITHIN data)
        {
            ApiResultObject<HIS_DEATH_WITHIN> result = new ApiResultObject<HIS_DEATH_WITHIN>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_DEATH_WITHIN resultData = null;
                if (valid && new HisDeathWithinLock(param).ChangeLock(data))
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
        public ApiResultObject<bool> Delete(HIS_DEATH_WITHIN data)
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
                    resultData = new HisDeathWithinTruncate(param).Truncate(data);
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
