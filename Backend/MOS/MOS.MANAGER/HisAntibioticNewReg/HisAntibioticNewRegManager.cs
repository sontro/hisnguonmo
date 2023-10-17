using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticNewReg
{
    public partial class HisAntibioticNewRegManager : BusinessBase
    {
        public HisAntibioticNewRegManager()
            : base()
        {

        }
        
        public HisAntibioticNewRegManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIBIOTIC_NEW_REG>> Get(HisAntibioticNewRegFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIBIOTIC_NEW_REG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIBIOTIC_NEW_REG> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticNewRegGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> Create(HIS_ANTIBIOTIC_NEW_REG data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_NEW_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_NEW_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticNewRegCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> Update(HIS_ANTIBIOTIC_NEW_REG data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_NEW_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_NEW_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticNewRegUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_NEW_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_NEW_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticNewRegLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> Lock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_NEW_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticNewRegLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_NEW_REG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_NEW_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticNewRegLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntibioticNewRegTruncate(param).Truncate(id);
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
