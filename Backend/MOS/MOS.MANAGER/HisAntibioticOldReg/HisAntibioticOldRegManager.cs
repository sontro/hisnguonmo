using Inventec.Core;
using Inventec.Common.Logging;
using MOS.EFMODEL.DataModels;
using MOS.MANAGER.Base;
using System;
using System.Collections.Generic;

namespace MOS.MANAGER.HisAntibioticOldReg
{
    public partial class HisAntibioticOldRegManager : BusinessBase
    {
        public HisAntibioticOldRegManager()
            : base()
        {

        }
        
        public HisAntibioticOldRegManager(CommonParam param)
            : base(param)
        {

        }
		
		[Logger]
        public ApiResultObject<List<HIS_ANTIBIOTIC_OLD_REG>> Get(HisAntibioticOldRegFilterQuery filter)
        {
            ApiResultObject<List<HIS_ANTIBIOTIC_OLD_REG>> result = null;
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(filter);
                List<HIS_ANTIBIOTIC_OLD_REG> resultData = null;
                if (valid)
                {
                    resultData = new HisAntibioticOldRegGet(param).Get(filter);
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
        public ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> Create(HIS_ANTIBIOTIC_OLD_REG data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_OLD_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_OLD_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticOldRegCreate(param).Create(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> Update(HIS_ANTIBIOTIC_OLD_REG data)
        {
            ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_OLD_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                valid = valid && IsNotNull(data);
                HIS_ANTIBIOTIC_OLD_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
					isSuccess = new HisAntibioticOldRegUpdate(param).Update(data);
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
        public ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> ChangeLock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> result = new ApiResultObject<HIS_ANTIBIOTIC_OLD_REG>(null);
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_OLD_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticOldRegLock(param).ChangeLock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> Lock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_OLD_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticOldRegLock(param).Lock(id, ref resultData);
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
        public ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> Unlock(long id)
        {
            ApiResultObject<HIS_ANTIBIOTIC_OLD_REG> result = null;
            
            try
            {
                bool valid = true;
                valid = valid && IsNotNull(param);
                HIS_ANTIBIOTIC_OLD_REG resultData = null;
				bool isSuccess = false;
                if (valid)
                {
                    isSuccess = new HisAntibioticOldRegLock(param).Unlock(id, ref resultData);
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
                    resultData = new HisAntibioticOldRegTruncate(param).Truncate(id);
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
