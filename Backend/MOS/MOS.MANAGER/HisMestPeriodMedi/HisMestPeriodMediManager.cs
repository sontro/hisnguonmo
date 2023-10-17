using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMedi
{
    public class HisMestPeriodMediManager : BusinessBase
    {
        public HisMestPeriodMediManager()
            : base()
        {

        }
        
        public HisMestPeriodMediManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_PERIOD_MEDI>> Get(HisMestPeriodMediFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PERIOD_MEDI>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PERIOD_MEDI> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMediGet(param).Get(filter);
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

        public ApiResultObject<List<V_HIS_MEST_PERIOD_MEDI>> GetView(HisMestPeriodMediViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PERIOD_MEDI>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PERIOD_MEDI> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMediGet(param).GetView(filter);
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

        public ApiResultObject<HIS_MEST_PERIOD_MEDI> GetById(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_MEDI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_MEST_PERIOD_MEDI resultData = null;
                if (valid)
                {
                    HisMestPeriodMediFilterQuery filter = new HisMestPeriodMediFilterQuery();
                    resultData = new HisMestPeriodMediGet(param).GetById(id, filter);
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
        
        public ApiResultObject<V_HIS_MEST_PERIOD_MEDI> GetViewById(long id)
        {
            ApiResultObject<V_HIS_MEST_PERIOD_MEDI> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                V_HIS_MEST_PERIOD_MEDI resultData = null;
                if (valid)
                {
                    HisMestPeriodMediViewFilterQuery filter = new HisMestPeriodMediViewFilterQuery();
                    resultData = new HisMestPeriodMediGet(param).GetViewById(id, filter);
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

        public ApiResultObject<HIS_MEST_PERIOD_MEDI> Create(HIS_MEST_PERIOD_MEDI data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MEDI> result = new ApiResultObject<HIS_MEST_PERIOD_MEDI>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MEDI resultData = null;
                if (valid && new HisMestPeriodMediCreate(param).Create(data))
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

        public ApiResultObject<HIS_MEST_PERIOD_MEDI> Update(HIS_MEST_PERIOD_MEDI data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MEDI> result = new ApiResultObject<HIS_MEST_PERIOD_MEDI>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MEDI resultData = null;
                if (valid && new HisMestPeriodMediUpdate(param).Update(data))
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

        public ApiResultObject<HIS_MEST_PERIOD_MEDI> ChangeLock(HIS_MEST_PERIOD_MEDI data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MEDI> result = new ApiResultObject<HIS_MEST_PERIOD_MEDI>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEST_PERIOD_MEDI resultData = null;
                if (valid && new HisMestPeriodMediLock(param).ChangeLock(data))
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
