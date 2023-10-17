using Inventec.Common.Logging;
using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisMestPeriodMate
{
    public class HisMestPeriodMateManager : BusinessBase
    {
        public HisMestPeriodMateManager()
            : base()
        {

        }
        
        public HisMestPeriodMateManager(CommonParam param)
            : base(param)
        {

        }

        [Logger]
        public ApiResultObject<List<HIS_MEST_PERIOD_MATE>> Get(HisMestPeriodMateFilterQuery filter)
        {
            ApiResultObject<List<HIS_MEST_PERIOD_MATE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_MEST_PERIOD_MATE> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMateGet(param).Get(filter);
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

        public ApiResultObject<List<V_HIS_MEST_PERIOD_MATE>> GetView(HisMestPeriodMateViewFilterQuery filter)
        {
            ApiResultObject<List<V_HIS_MEST_PERIOD_MATE>> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<V_HIS_MEST_PERIOD_MATE> resultData = null;
                if (valid)
                {
                    resultData = new HisMestPeriodMateGet(param).GetView(filter);
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

        public ApiResultObject<HIS_MEST_PERIOD_MATE> GetById(long id)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                HIS_MEST_PERIOD_MATE resultData = null;
                if (valid)
                {
                    HisMestPeriodMateFilterQuery filter = new HisMestPeriodMateFilterQuery();
                    resultData = new HisMestPeriodMateGet(param).GetById(id, filter);
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
        
        public ApiResultObject<V_HIS_MEST_PERIOD_MATE> GetViewById(long id)
        {
            ApiResultObject<V_HIS_MEST_PERIOD_MATE> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(id);
                V_HIS_MEST_PERIOD_MATE resultData = null;
                if (valid)
                {
                    HisMestPeriodMateViewFilterQuery filter = new HisMestPeriodMateViewFilterQuery();
                    resultData = new HisMestPeriodMateGet(param).GetViewById(id, filter);
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

        public ApiResultObject<HIS_MEST_PERIOD_MATE> Create(HIS_MEST_PERIOD_MATE data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATE> result = new ApiResultObject<HIS_MEST_PERIOD_MATE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MATE resultData = null;
                if (valid && new HisMestPeriodMateCreate(param).Create(data))
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

        public ApiResultObject<HIS_MEST_PERIOD_MATE> Update(HIS_MEST_PERIOD_MATE data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATE> result = new ApiResultObject<HIS_MEST_PERIOD_MATE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_MEST_PERIOD_MATE resultData = null;
                if (valid && new HisMestPeriodMateUpdate(param).Update(data))
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

        public ApiResultObject<HIS_MEST_PERIOD_MATE> ChangeLock(HIS_MEST_PERIOD_MATE data)
        {
            ApiResultObject<HIS_MEST_PERIOD_MATE> result = new ApiResultObject<HIS_MEST_PERIOD_MATE>(null);
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsGreaterThanZero(data.ID);
                HIS_MEST_PERIOD_MATE resultData = null;
                if (valid && new HisMestPeriodMateLock(param).ChangeLock(data))
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
