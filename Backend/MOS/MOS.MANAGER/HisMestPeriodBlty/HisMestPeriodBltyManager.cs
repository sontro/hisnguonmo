using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodBlty
{
    public partial class HisMestPeriodBltyManager : BusinessBase
    {
        public HisMestPeriodBltyManager()
            : base()
        {

        }
        
        public HisMestPeriodBltyManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_MEST_PERIOD_BLTY>> Get(HisMestPeriodBltyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PERIOD_BLTY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PERIOD_BLTY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodBltyGet(param).Get(filter);
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
        public ApiResultObject<HIS_MEST_PERIOD_BLTY> Create(HIS_MEST_PERIOD_BLTY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLTY> result = new ApiResultObject<HIS_MEST_PERIOD_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLTY resultData = null;
                if (valid && new HisMestPeriodBltyCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEST_PERIOD_BLTY> Update(HIS_MEST_PERIOD_BLTY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLTY> result = new ApiResultObject<HIS_MEST_PERIOD_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLTY resultData = null;
                if (valid && new HisMestPeriodBltyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_PERIOD_BLTY> ChangeLock(HIS_MEST_PERIOD_BLTY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLTY> result = new ApiResultObject<HIS_MEST_PERIOD_BLTY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_BLTY resultData = null;
                if (valid && new HisMestPeriodBltyLock(param).ChangeLock(data))
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
        public ApiResultObject<HIS_MEST_PERIOD_BLTY> Lock(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PERIOD_BLTY resultData = null;
                if (valid)
                {
                    new HisMestPeriodBltyLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_MEST_PERIOD_BLTY> Unlock(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_BLTY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_MEST_PERIOD_BLTY resultData = null;
                if (valid)
                {
                    new HisMestPeriodBltyLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisMestPeriodBltyTruncate(param).Truncate(id);
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
