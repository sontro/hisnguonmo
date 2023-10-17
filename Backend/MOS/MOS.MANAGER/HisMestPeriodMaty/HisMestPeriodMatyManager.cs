using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMaty
{
    public class HisMestPeriodMatyManager : BusinessBase
    {
        public HisMestPeriodMatyManager()
            : base()
        {

        }
        
        public HisMestPeriodMatyManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_PERIOD_MATY>> Get(HisMestPeriodMatyFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PERIOD_MATY>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PERIOD_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMatyGet(param).Get(filter);
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
        public ApiResultObject<List<V_HIS_MEST_PERIOD_MATY>> GetView(HisMestPeriodMatyViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PERIOD_MATY>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PERIOD_MATY> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMatyGet(param).GetView(filter);
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
        public ApiResultObject<HIS_MEST_PERIOD_MATY> GetById(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_MEST_PERIOD_MATY resultData = null;
                if (valid)
                {
                    HisMestPeriodMatyFilterQuery filter = new HisMestPeriodMatyFilterQuery();
                    resultData = new HisMestPeriodMatyGet(param).GetById(id, filter);
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
        public ApiResultObject<V_HIS_MEST_PERIOD_MATY> GetViewById(long id)
        {
            ApiResultObject<V_HIS_MEST_PERIOD_MATY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                V_HIS_MEST_PERIOD_MATY resultData = null;
                if (valid)
                {
                    HisMestPeriodMatyViewFilterQuery filter = new HisMestPeriodMatyViewFilterQuery();
                    resultData = new HisMestPeriodMatyGet(param).GetViewById(id, filter);
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
        public ApiResultObject<HIS_MEST_PERIOD_MATY> Create(HIS_MEST_PERIOD_MATY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATY> result = new ApiResultObject<HIS_MEST_PERIOD_MATY>(null);
           
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MATY resultData = null;
                if (valid && new HisMestPeriodMatyCreate(param).Create(data))
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
        public ApiResultObject<HIS_MEST_PERIOD_MATY> Update(HIS_MEST_PERIOD_MATY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATY> result = new ApiResultObject<HIS_MEST_PERIOD_MATY>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MATY resultData = null;
                if (valid && new HisMestPeriodMatyUpdate(param).Update(data))
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
        public ApiResultObject<HIS_MEST_PERIOD_MATY> ChangeLock(HIS_MEST_PERIOD_MATY data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATY> result = new ApiResultObject<HIS_MEST_PERIOD_MATY>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEST_PERIOD_MATY resultData = null;
                if (valid && new HisMestPeriodMatyLock(param).ChangeLock(data))
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
    }
}
