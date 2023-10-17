using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisSaleProfitCfg
{
    public partial class HisSaleProfitCfgManager : BusinessBase
    {
        public HisSaleProfitCfgManager()
            : base()
        {

        }
        
        public HisSaleProfitCfgManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_SALE_PROFIT_CFG>> Get(HisSaleProfitCfgFilterQuery filter)
        {
            ApiResultObject<List<HIS_SALE_PROFIT_CFG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_SALE_PROFIT_CFG> resultData = null;
                if (valid)
                {
                    resultData = new HisSaleProfitCfgGet(param).Get(filter);
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
        public ApiResultObject<HIS_SALE_PROFIT_CFG> Create(HIS_SALE_PROFIT_CFG data)
        {
            ApiResultObject<HIS_SALE_PROFIT_CFG> result = new ApiResultObject<HIS_SALE_PROFIT_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SALE_PROFIT_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSaleProfitCfgCreate(param).Create(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_SALE_PROFIT_CFG> Update(HIS_SALE_PROFIT_CFG data)
        {
            ApiResultObject<HIS_SALE_PROFIT_CFG> result = new ApiResultObject<HIS_SALE_PROFIT_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_SALE_PROFIT_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisSaleProfitCfgUpdate(param).Update(data);
                    resultData = isSuccess ? data : null;
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }

		[Logger]
        public ApiResultObject<HIS_SALE_PROFIT_CFG> ChangeLock(long id)
        {
            ApiResultObject<HIS_SALE_PROFIT_CFG> result = new ApiResultObject<HIS_SALE_PROFIT_CFG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SALE_PROFIT_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSaleProfitCfgLock(param).ChangeLock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                Inventec.Common.Logging.LogSystem.Error(ex);
                param.HasException = true;
            }
            
            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_SALE_PROFIT_CFG> Lock(long id)
        {
            ApiResultObject<HIS_SALE_PROFIT_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SALE_PROFIT_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSaleProfitCfgLock(param).Lock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
            }
            catch (Exception ex)
            {
                LogSystem.Error(ex);
                param.HasException = true;
            }

            return result;
        }
		
		[Logger]
        public ApiResultObject<HIS_SALE_PROFIT_CFG> Unlock(long id)
        {
            ApiResultObject<HIS_SALE_PROFIT_CFG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_SALE_PROFIT_CFG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisSaleProfitCfgLock(param).Unlock(id, ref resultData);
                }
                result = this.PackResult(resultData, isSuccess);
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
                    resultData = new HisSaleProfitCfgTruncate(param).Truncate(id);
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
