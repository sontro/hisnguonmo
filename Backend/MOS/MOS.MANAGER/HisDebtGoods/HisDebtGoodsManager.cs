using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisDebtGoods
{
    public partial class HisDebtGoodsManager : BusinessBase
    {
        public HisDebtGoodsManager()
            : base()
        {

        }
        
        public HisDebtGoodsManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_DEBT_GOODS>> Get(HisDebtGoodsFilterQuery filter)
        {
            ApiResultObject<List<HIS_DEBT_GOODS>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_DEBT_GOODS> resultData = null;
                if (valid)
                {
                    resultData = new HisDebtGoodsGet(param).Get(filter);
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
        public ApiResultObject<HIS_DEBT_GOODS> Create(HIS_DEBT_GOODS data)
        {
            ApiResultObject<HIS_DEBT_GOODS> result = new ApiResultObject<HIS_DEBT_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBT_GOODS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebtGoodsCreate(param).Create(data);
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
        public ApiResultObject<HIS_DEBT_GOODS> Update(HIS_DEBT_GOODS data)
        {
            ApiResultObject<HIS_DEBT_GOODS> result = new ApiResultObject<HIS_DEBT_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_DEBT_GOODS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisDebtGoodsUpdate(param).Update(data);
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
        public ApiResultObject<HIS_DEBT_GOODS> ChangeLock(long id)
        {
            ApiResultObject<HIS_DEBT_GOODS> result = new ApiResultObject<HIS_DEBT_GOODS>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBT_GOODS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebtGoodsLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBT_GOODS> Lock(long id)
        {
            ApiResultObject<HIS_DEBT_GOODS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBT_GOODS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebtGoodsLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_DEBT_GOODS> Unlock(long id)
        {
            ApiResultObject<HIS_DEBT_GOODS> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_DEBT_GOODS resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisDebtGoodsLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisDebtGoodsTruncate(param).Truncate(id);
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
