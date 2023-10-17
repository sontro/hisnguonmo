using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisImpMestPay
{
    public partial class HisImpMestPayManager : BusinessBase
    {
        public HisImpMestPayManager()
            : base()
        {

        }
        
        public HisImpMestPayManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_IMP_MEST_PAY>> Get(HisImpMestPayFilterQuery filter)
        {
            ApiResultObject<List<HIS_IMP_MEST_PAY>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_IMP_MEST_PAY> resultData = null;
                if (valid)
                {
                    resultData = new HisImpMestPayGet(param).Get(filter);
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
        public ApiResultObject<HIS_IMP_MEST_PAY> Create(HIS_IMP_MEST_PAY data)
        {
            ApiResultObject<HIS_IMP_MEST_PAY> result = new ApiResultObject<HIS_IMP_MEST_PAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_PAY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpMestPayCreate(param).Create(data);
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
        public ApiResultObject<HIS_IMP_MEST_PAY> Update(HIS_IMP_MEST_PAY data)
        {
            ApiResultObject<HIS_IMP_MEST_PAY> result = new ApiResultObject<HIS_IMP_MEST_PAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_IMP_MEST_PAY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisImpMestPayUpdate(param).Update(data);
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
        public ApiResultObject<HIS_IMP_MEST_PAY> ChangeLock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PAY> result = new ApiResultObject<HIS_IMP_MEST_PAY>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PAY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestPayLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_PAY> Lock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PAY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PAY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestPayLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_IMP_MEST_PAY> Unlock(long id)
        {
            ApiResultObject<HIS_IMP_MEST_PAY> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_IMP_MEST_PAY resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisImpMestPayLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisImpMestPayTruncate(param).Truncate(id);
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
