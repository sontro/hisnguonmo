using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisInfusionSum
{
    public partial class HisInfusionSumManager : BusinessBase
    {
        public HisInfusionSumManager()
            : base()
        {

        }
        
        public HisInfusionSumManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_INFUSION_SUM>> Get(HisInfusionSumFilterQuery filter)
        {
            ApiResultObject<List<HIS_INFUSION_SUM>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_INFUSION_SUM> resultData = null;
                if (valid)
                {
                    resultData = new HisInfusionSumGet(param).Get(filter);
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
        public ApiResultObject<HIS_INFUSION_SUM> Create(HIS_INFUSION_SUM data)
        {
            ApiResultObject<HIS_INFUSION_SUM> result = new ApiResultObject<HIS_INFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INFUSION_SUM resultData = null;
                if (valid && new HisInfusionSumCreate(param).Create(data))
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
        public ApiResultObject<HIS_INFUSION_SUM> Update(HIS_INFUSION_SUM data)
        {
            ApiResultObject<HIS_INFUSION_SUM> result = new ApiResultObject<HIS_INFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_INFUSION_SUM resultData = null;
                if (valid && new HisInfusionSumUpdate(param).Update(data))
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
        public ApiResultObject<HIS_INFUSION_SUM> ChangeLock(long id)
        {
            ApiResultObject<HIS_INFUSION_SUM> result = new ApiResultObject<HIS_INFUSION_SUM>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INFUSION_SUM resultData = null;
                if (valid)
                {
                    new HisInfusionSumLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_INFUSION_SUM> Lock(long id)
        {
            ApiResultObject<HIS_INFUSION_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INFUSION_SUM resultData = null;
                if (valid)
                {
                    new HisInfusionSumLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_INFUSION_SUM> Unlock(long id)
        {
            ApiResultObject<HIS_INFUSION_SUM> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_INFUSION_SUM resultData = null;
                if (valid)
                {
                    new HisInfusionSumLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisInfusionSumTruncate(param).Truncate(id);
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
