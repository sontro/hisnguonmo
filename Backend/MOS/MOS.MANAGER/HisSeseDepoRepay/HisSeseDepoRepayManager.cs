using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSeseDepoRepay
{
    public partial class HisSeseDepoRepayManager : BusinessBase
    {
        public HisSeseDepoRepayManager()
            : base()
        {

        }
        
        public HisSeseDepoRepayManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SESE_DEPO_REPAY>> Get(HisSeseDepoRepayFilterQuery filter)
        {
            ApiResultObject<List<HIS_SESE_DEPO_REPAY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SESE_DEPO_REPAY> resultData = null;
                if (valid)
                {
                    resultData = new HisSeseDepoRepayGet(param).Get(filter);
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
        public ApiResultObject<HIS_SESE_DEPO_REPAY> Create(HIS_SESE_DEPO_REPAY data)
        {
            ApiResultObject<HIS_SESE_DEPO_REPAY> result = new ApiResultObject<HIS_SESE_DEPO_REPAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_DEPO_REPAY resultData = null;
                if (valid && new HisSeseDepoRepayCreate(param).Create(data))
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
        public ApiResultObject<HIS_SESE_DEPO_REPAY> Update(HIS_SESE_DEPO_REPAY data)
        {
            ApiResultObject<HIS_SESE_DEPO_REPAY> result = new ApiResultObject<HIS_SESE_DEPO_REPAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SESE_DEPO_REPAY resultData = null;
                if (valid && new HisSeseDepoRepayUpdate(param).Update(data))
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
        public ApiResultObject<HIS_SESE_DEPO_REPAY> ChangeLock(long id)
        {
            ApiResultObject<HIS_SESE_DEPO_REPAY> result = new ApiResultObject<HIS_SESE_DEPO_REPAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_DEPO_REPAY resultData = null;
                if (valid)
                {
                    new HisSeseDepoRepayLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_DEPO_REPAY> Lock(long id)
        {
            ApiResultObject<HIS_SESE_DEPO_REPAY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_DEPO_REPAY resultData = null;
                if (valid)
                {
                    new HisSeseDepoRepayLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_SESE_DEPO_REPAY> Unlock(long id)
        {
            ApiResultObject<HIS_SESE_DEPO_REPAY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SESE_DEPO_REPAY resultData = null;
                if (valid)
                {
                    new HisSeseDepoRepayLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisSeseDepoRepayTruncate(param).Truncate(id);
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
