using Inventec.Core;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisTransactionType
{
    public partial class HisTransactionTypeManager : BusinessBase
    {
        public HisTransactionTypeManager()
            : base()
        {

        }
        
        public HisTransactionTypeManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_TRANSACTION_TYPE>> Get(HisTransactionTypeFilterQuery filter)
        {
            ApiResultObject<List<HIS_TRANSACTION_TYPE>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_TRANSACTION_TYPE> resultData = null;
                if (valid)
                {
                    resultData = new HisTransactionTypeGet(param).Get(filter);
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
        public ApiResultObject<HIS_TRANSACTION_TYPE> Create(HIS_TRANSACTION_TYPE data)
        {
            ApiResultObject<HIS_TRANSACTION_TYPE> result = new ApiResultObject<HIS_TRANSACTION_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_TYPE resultData = null;
                if (valid && new HisTransactionTypeCreate(param).Create(data))
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
        public ApiResultObject<HIS_TRANSACTION_TYPE> Update(HIS_TRANSACTION_TYPE data)
        {
            ApiResultObject<HIS_TRANSACTION_TYPE> result = new ApiResultObject<HIS_TRANSACTION_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_TYPE resultData = null;
                if (valid && new HisTransactionTypeUpdate(param).Update(data))
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
        public ApiResultObject<HIS_TRANSACTION_TYPE> ChangeLock(HIS_TRANSACTION_TYPE data)
        {
            ApiResultObject<HIS_TRANSACTION_TYPE> result = new ApiResultObject<HIS_TRANSACTION_TYPE>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_TRANSACTION_TYPE resultData = null;
                if (valid && new HisTransactionTypeLock(param).ChangeLock(data.ID))
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
        public ApiResultObject<bool> Delete(HIS_TRANSACTION_TYPE data)
        {
            ApiResultObject<bool> result = new ApiResultObject<bool>(false);

            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                bool resultData = false;
                if (valid)
                {
                    resultData = new HisTransactionTypeTruncate(param).Truncate(data);
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
