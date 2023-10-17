using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlood
{
    public partial class HisMestPeriodBloodManager : BusinessBase
    {
        public HisMestPeriodBloodManager()
            : base()
        {

        }
        
        public HisMestPeriodBloodManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_PERIOD_BLOOD>> Get(HisMestPeriodBloodFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PERIOD_BLOOD>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PERIOD_BLOOD> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodBloodGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_PERIOD_BLOOD> Create(HIS_MEST_PERIOD_BLOOD data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLOOD> result = new ApiResultObject<HIS_MEST_PERIOD_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLOOD resultData = null;
                if (valid && new HisMestPeriodBloodCreate(param).Create(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_MEST_PERIOD_BLOOD> Update(HIS_MEST_PERIOD_BLOOD data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLOOD> result = new ApiResultObject<HIS_MEST_PERIOD_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLOOD resultData = null;
                if (valid && new HisMestPeriodBloodUpdate(param).Update(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_MEST_PERIOD_BLOOD> ChangeLock(HIS_MEST_PERIOD_BLOOD data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLOOD> result = new ApiResultObject<HIS_MEST_PERIOD_BLOOD>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLOOD resultData = null;
                if (valid && new HisMestPeriodBloodLock(param).ChangeLock(data))
                {
                    resultData = data;
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
        public ApiResultObject<HIS_MEST_PERIOD_BLOOD> Lock(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PERIOD_BLOOD resultData = null;
                if (valid)
                {
                    new HisMestPeriodBloodLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_PERIOD_BLOOD> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLOOD> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PERIOD_BLOOD resultData = null;
                if (valid)
                {
                    new HisMestPeriodBloodLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestPeriodBloodTruncate(param).Truncate(id);
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
