using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionExp
{
    public partial class HisTransactionExpManager : BusinessBase
    {
        public HisTransactionExpManager()
            : base()
        {

        }
        
        public HisTransactionExpManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRANSACTION_EXP>> Get(HisTransactionExpFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANSACTION_EXP>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANSACTION_EXP> resultData = null;
                if (valid)
                {
                    resultData = new HisTransactionExpGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRANSACTION_EXP> Create(HIS_TRANSACTION_EXP data)
        {
            ApiResultObject<HIS_TRANSACTION_EXP> result = new ApiResultObject<HIS_TRANSACTION_EXP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_EXP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransactionExpCreate(param).Create(data);
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
        public ApiResultObject<HIS_TRANSACTION_EXP> Update(HIS_TRANSACTION_EXP data)
        {
            ApiResultObject<HIS_TRANSACTION_EXP> result = new ApiResultObject<HIS_TRANSACTION_EXP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_EXP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisTransactionExpUpdate(param).Update(data);
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
        public ApiResultObject<HIS_TRANSACTION_EXP> ChangeLock(long id)
        {
            ApiResultObject<HIS_TRANSACTION_EXP> result = new ApiResultObject<HIS_TRANSACTION_EXP>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSACTION_EXP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransactionExpLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION_EXP> Lock(long id)
        {
            ApiResultObject<HIS_TRANSACTION_EXP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSACTION_EXP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransactionExpLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_TRANSACTION_EXP> Unlock(long id)
        {
            ApiResultObject<HIS_TRANSACTION_EXP> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_TRANSACTION_EXP resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisTransactionExpLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisTransactionExpTruncate(param).Truncate(id);
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
